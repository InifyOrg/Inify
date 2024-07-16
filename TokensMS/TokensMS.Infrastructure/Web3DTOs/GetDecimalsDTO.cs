using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokensMS.Infrastructure.Web3DTOs
{
    [Function("decimals", "uint256")]
    class GetDecimalsDTO : FunctionMessage
    {
    }
}
