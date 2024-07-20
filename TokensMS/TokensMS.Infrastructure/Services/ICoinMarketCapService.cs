using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokensMS.Infrastructure.Services
{
    public interface ICoinMarketCapService
    {
        Task<string> GetTokensJson();
    }
}
