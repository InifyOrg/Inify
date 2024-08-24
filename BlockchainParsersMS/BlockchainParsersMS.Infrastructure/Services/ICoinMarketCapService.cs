using BlockchainParsersMS.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainParsersMS.Infrastructure.Services
{
    public interface ICoinMarketCapService
    {
        Task<List<ParsedTokenDTO>> ParsePricesOfParsedTokens(List<ParsedTokenDTO> parsedTokensWithoutPrice);
    }
}
