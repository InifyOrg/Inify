using Microsoft.Extensions.Configuration;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.JsonRpc.Client;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TokensMS.Infrastructure.Web3DTOs;

namespace TokensMS.Infrastructure.Services
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

        public async Task<int> GetDecimalsByAddress(string address)
        {
            GetDecimalsDTO getDecimalsFunction = new GetDecimalsDTO();

            IContractQueryHandler<GetDecimalsDTO> handler = _web3.Eth.GetContractQueryHandler<GetDecimalsDTO>();

            BigInteger decimals = await handler.QueryAsync<BigInteger>(address, getDecimalsFunction);

            return ((int)decimals);
        }
    }
}
