using BlockchainParsersMS.Contract;
using Microsoft.Extensions.Configuration;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainParsersMS.Infrastructure.Services
{
    public class Web3Service : IWeb3Service
    {
        private readonly Web3 _web3;

        public Web3Service()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            _web3 = new Web3(configuration.GetSection("APIs:InfuraEthNode").Value);
        }

        public async Task<ParsedTokenDTO> parseBaseErcToken(WalletInfoDTO walletInfo)
        {
            ParsedTokenDTO result = new ParsedTokenDTO() { WalletInfo = walletInfo };
            result.Amount = Web3.Convert.FromWei(await _web3.Eth.GetBalance.SendRequestAsync(walletInfo.Address));
            result.Platform = "ethereum";
            result.Name = "Ethereum";
            result.Symbol = "ETH";
            result.Chain = "ERC-20";
            result.Price = 1;
            result.UsdValue = result.Amount * result.Price;
            return result;
        }

    }
}
