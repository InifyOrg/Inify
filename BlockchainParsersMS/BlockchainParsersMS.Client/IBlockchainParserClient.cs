using BlockchainParsersMS.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainParsersMS.Client
{
    public interface IBlockchainParserClient
    {
        public Task<ParsingOutputDTO> parseManyByUserId(long userId);
        public Task<ParsingOutputDTO> parseOneByAddress(WalletDTO walletInfo);
    }
}
