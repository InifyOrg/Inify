using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainParsersMS.Infrastructure.Web3DTOs
{
    [Function("balanceOf", "uint256")]
    class BalanceOfDTO : FunctionMessage
    {
        [Parameter("address", 1)]
        public string UserAddress { get; set; }

        public BalanceOfDTO() { }
        public BalanceOfDTO(string address) { this.UserAddress = address; }
    }
}
