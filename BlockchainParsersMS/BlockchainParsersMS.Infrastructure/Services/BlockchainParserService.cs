using BlockchainParsersMS.Contract;
using Mapster;
using Microsoft.Extensions.Configuration;
using Nethereum.Web3;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokensMs.Client;
using TokensMS.Contract;
using WalletsMS.Client;

namespace BlockchainParsersMS.Infrastructure.Services
{
    public class BlockchainParserService : IBlockchainParserService
    {
        private readonly IWeb3Service _web3Service;
        private readonly ITokensMsClient _tokensMsClient;
        private readonly IWalletsMsClient _walletsMsClient; 
        private readonly ICoinMarketCapService _coinMarketCapService;
        public BlockchainParserService(IWeb3Service web3Service, ITokensMsClient tokensMsClient, IWalletsMsClient walletsMsClient, ICoinMarketCapService coinMarketCapService)
        {
            _web3Service = web3Service;
            _tokensMsClient = tokensMsClient;
            _walletsMsClient = walletsMsClient;
            _coinMarketCapService = coinMarketCapService;
        }

        public decimal GetTotalBalance(List<ParsedTokenDTO> parsedTokens)
        {
            decimal totalBalance = 0;
            foreach (ParsedTokenDTO token in parsedTokens)
            {
                totalBalance += token.UsdValue;
            }
            return totalBalance;
        }

        public BestTokenDTO GetTotalBestSymbol(List<ParsedTokenDTO> parsedTokens)
        {
            List<BestTokenDTO> bestTokens = new List<BestTokenDTO>();

            foreach (ParsedTokenDTO token in parsedTokens)
            {
                if(bestTokens.FirstOrDefault(bt => bt.Symbol == token.Symbol) == null) 
                    bestTokens.Add(new BestTokenDTO() { Symbol = token.Symbol, Amount = token.Amount, UsdValue = token.UsdValue });
                else
                {
                    int index = bestTokens.FindIndex(bt => bt.Symbol == token.Symbol);
                    bestTokens[index].Amount += token.Amount;
                    bestTokens[index].UsdValue += token.UsdValue;
                }
            }

            return bestTokens.OrderByDescending(bt => bt.UsdValue).First();
        }

        public async Task<ParsingOutputDTO> ParseManyByUserId(long userId)
        {
            ParsingOutputDTO output = new ParsingOutputDTO() 
            { 
                Wallets = new List<WalletParsedInfoDTO>()
            };

            TypeAdapterConfig<WalletsMS.Contract.WalletDTO, WalletDTO>.NewConfig().Map(dest => dest.Type, src => src.WalletType.Title);

            List<WalletDTO> wallets = (await _walletsMsClient.GetAllWalletsByUserId(userId)).Adapt<List<WalletDTO>>();
            List<ParsedTokenDTO> tokensForStatistics = new List<ParsedTokenDTO>();

            IEnumerable<Task> tasks = wallets.Select(async wallet => {
                WalletParsedInfoDTO walletParsed = new WalletParsedInfoDTO
                {
                    Wallet = wallet,
                    Tokens = await ParseOneByAddress(wallet),
                };

                walletParsed.BestToken = GetTotalBestSymbol(walletParsed.Tokens);
                walletParsed.Balance = GetTotalBalance(walletParsed.Tokens);

                output.Wallets.Add(walletParsed);
                tokensForStatistics.AddRange(walletParsed.Tokens);
            });
            await Task.WhenAll(tasks);

            output.TotalBalance = GetTotalBalance(tokensForStatistics);
            output.TotalBestTokenSymbol = GetTotalBestSymbol(tokensForStatistics).Symbol;

            return output;
        }

        public async Task<List<ParsedTokenDTO>> ParseOneByAddress(WalletDTO walletInfo)
        {
            List<ParsedTokenDTO> parsedTokensWithoutPrice = new List<ParsedTokenDTO>();

            ParsedTokenDTO parsedBaseToken = await _web3Service.ParseBaseErcToken(walletInfo);
            parsedTokensWithoutPrice.Add(parsedBaseToken);

            List<TokenDTO> tokens = await _tokensMsClient.GetAllTokensByWalletType(walletInfo.Type);

            parsedTokensWithoutPrice.AddRange(await _web3Service.ParseBalancesWithMulticall(walletInfo.Address, tokens));

            List<ParsedTokenDTO> parsedTokensWithPrice = await _coinMarketCapService.ParsePricesOfParsedTokens(parsedTokensWithoutPrice);

            return parsedTokensWithPrice;
        }


    }
}
