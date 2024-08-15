using BlockchainParsersMS.Contract;
using BlockchainParsersMS.Infrastructure.Web3DTOs;
using Microsoft.Extensions.Configuration;
using Nethereum.Contracts;
using Nethereum.Contracts.Standards.ERC20.ContractDefinition;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TokensMS.Contract;

namespace BlockchainParsersMS.Infrastructure.Services
{
    public class Web3Service : IWeb3Service
    {
        private readonly Web3 _web3;
        private readonly ICoinGeckoService _coinGeckoService;
        public Web3Service(ICoinGeckoService coinGeckoService)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            _web3 = new Web3(configuration.GetSection("APIs:InfuraEthNode").Value);
            _coinGeckoService = coinGeckoService;
        }

        public async Task<ParsedTokenDTO> parseBaseErcToken(WalletInfoDTO walletInfo)
        {
            ParsedTokenDTO result = new ParsedTokenDTO() { WalletInfo = walletInfo };
            result.Amount = Web3.Convert.FromWei(await _web3.Eth.GetBalance.SendRequestAsync(walletInfo.Address));
            result.Platform = "ethereum";
            result.Name = "Ethereum";
            result.Symbol = "ETH";
            result.Chain = "ERC-20";
            result.Price = await _coinGeckoService.GetPriceByCoinId(result.Name.ToLower());
            result.UsdValue = result.Amount * result.Price;
            return result;
        }

        public async Task<List<ParsedTokenDTO>> parseBalancesWithMulticall(List<string> addresses, List<TokenDTO> tokens)
        {
            List<IMulticallInputOutput> callList = new List<IMulticallInputOutput>();

            foreach (var token in tokens)
            {
                BalanceOfFunction balanceOf = new BalanceOfFunction() { Owner = addresses[0] };
                var call = new MulticallInputOutput<BalanceOfFunction, BalanceOfOutputDTO>(balanceOf, token.Address);
                callList.Add(call);
            }

            await _web3.Eth.GetMultiQueryHandler().MultiCallAsync(callList.ToArray()).ConfigureAwait(false);

            List<ParsedTokenDTO> currentBalances = new List<ParsedTokenDTO>();

            for(int i = 0; i < callList.Count; i++)
            {
                BigInteger balance = ((MulticallInputOutput<BalanceOfFunction, BalanceOfOutputDTO>)callList[i]).Output.Balance;
                if (balance > 0)
                {
                    ParsedTokenDTO balanceResult = new ParsedTokenDTO()
                    {
                        Name = tokens[i].Name,
                        Symbol = tokens[i].Symbol,
                        Platform = tokens[i].Platform.Slug,
                        Chain = "ERC-20",
                        TokenAddress = tokens[i].Address,
                        Amount = Web3.Convert.FromWei(balance, tokens[i].Decimals),
                        Price = await _coinGeckoService.GetPriceByTokenAddress(tokens[i].Address, tokens[i].Platform.Slug),
                    };
                    balanceResult.UsdValue = balanceResult.Amount * balanceResult.Price;

                    currentBalances.Add(balanceResult);
                }
            }

            return currentBalances;
        }

    }
}
