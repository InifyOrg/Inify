using BlockchainParsersMS.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainParsersMS.Infrastructure.Services
{
    public class BlockchainParserService : IBlockchainParserService
    {
        public Task<List<ParsedTokenDTO>> parseOneByAddress(string address)
        {
            throw new NotImplementedException();
        }
    }
}
