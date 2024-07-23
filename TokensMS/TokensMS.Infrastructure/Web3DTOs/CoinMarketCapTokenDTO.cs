using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokensMS.Infrastructure.Web3DTOs
{
    public class CoinMarketCapTokenDTO
    {
        [AdaptMember("Name")]
        public string name { get; set; }
        [AdaptMember("Symbol")]
        public string symbol { get; set; }
        [AdaptMember("Slug")]
        public string slug { get; set; }
        [AdaptMember("Platform")]
        public CoinMarketCapPlatformDTO platform { get; set; }
    }
}
