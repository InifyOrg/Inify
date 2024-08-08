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
    public class BlockchainParserService : IBlockchainParserService
    {
        private readonly IWeb3Service _web3Service;

        public BlockchainParserService(IWeb3Service web3Service)
        {
            _web3Service = web3Service;
        }

        public async Task<List<ParsedTokenDTO>> parseOneByAddress(string address)
        {
            ParsedTokenDTO parsedBaseToken = await _web3Service.parseBaseErcToken(address);
            throw new NotImplementedException();
        }
    }
}
