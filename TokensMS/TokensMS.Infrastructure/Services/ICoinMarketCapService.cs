using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokensMS.Contract;

namespace TokensMS.Infrastructure.Services
{
    public interface ICoinMarketCapService
    {
        Task<List<TokenDTO>> UpdateDatabase();
    }
}
