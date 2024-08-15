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
        public string BestTokenAmount { get; set; }
        public string BestTokenSymbol { get; set; }
    }
}
