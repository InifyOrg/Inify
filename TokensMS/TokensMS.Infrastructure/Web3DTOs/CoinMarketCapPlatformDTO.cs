using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokensMS.Infrastructure.Web3DTOs
{
    public class CoinMarketCapPlatformDTO
    {
        public string name { get; set; }
        public string symbol { get; set; }
        [AdaptMember("Slug")]
        public string slug { get; set; }
        [AdaptMember("Address")]
        public string token_address { get; set; }
        
    }
}
