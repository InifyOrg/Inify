using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokensMS.Infrastructure.Web3DTOs
{
    public class CoinMarketCapTokenDTO
    {
        public string name { get; set; }
        public string symbol { get; set; }
        public string slug { get; set; }
        public CoinMarketCapPlatformDTO platform { get; set; }
    }
}
