using BlockchainParsersMS.Contract;
using Microsoft.Extensions.Configuration;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokensMs.Client;
using TokensMS.Contract;

namespace BlockchainParsersMS.Infrastructure.Services
{
    public class BlockchainParserService : IBlockchainParserService
    {
        private readonly IWeb3Service _web3Service;
        private readonly ITokensMsClient _tokensMsClient;

        public BlockchainParserService(IWeb3Service web3Service, ITokensMsClient tokensMsClient)
        {
            _web3Service = web3Service;
            _tokensMsClient = tokensMsClient;
        }


        public async Task<List<ParsedTokenDTO>> parseOneByAddress(WalletInfoDTO walletInfo)
        {
            List<ParsedTokenDTO> res = new List<ParsedTokenDTO>();

            ParsedTokenDTO parsedBaseToken = await _web3Service.parseBaseErcToken(walletInfo);
            res.Add(parsedBaseToken);

            List<TokenDTO> tokens = await _tokensMsClient.GetAllTokensByWalletType(walletInfo.Type);

            res.AddRange(await _web3Service.parseBalancesWithMulticall(new List<string>{ walletInfo.Address }, tokens));

            return res;
        }


    }
}
