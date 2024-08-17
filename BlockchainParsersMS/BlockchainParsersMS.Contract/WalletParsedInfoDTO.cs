using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainParsersMS.Contract
{
    public class WalletParsedInfoDTO
    {
        public WalletDTO Wallet { get; set; }
        public decimal Balance { get; set; }
        public BestTokenDTO BestToken { get; set; }
        public List<ParsedTokenDTO> Tokens { get; set; }
    }
}
