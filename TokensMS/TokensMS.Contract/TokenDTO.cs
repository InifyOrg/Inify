using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokensMS.Contract
{
    public class TokenDTO
    {
        //не конечная дто, делать по ней бд нельзя, просто наброски чтобы понимать на чем строить бд
        public long Id {  get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string? Address { get; set; }
        public int Decimals { get; set; }
        public string? PlatformSymbol { get; set; }
        public string WalletType { get; set; }
    }
}
