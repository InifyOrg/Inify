using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainParsersMS.Contract
{
    public class WalletParsedInfoDTO
    {
        public string Address { get; set; }
        public string Type { get; set; }
        public decimal Balance { get; set; }
        public BestTokenDTO BestToken { get; set; }
    }
}
