using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainParsersMS.Contract
{
    public class ParsedTokenDTO
    {
        public long Id { get; set; }
        public string WalletAddress { get; set; }
        public string WalletType { get; set; }
        public string Platform { get; set; }
        public string? TokenAddress { get; set; }
        public int Amount { get; set; }
        public int Price { get; set; }
        public int UsdValue { get; set; }
    }
}
