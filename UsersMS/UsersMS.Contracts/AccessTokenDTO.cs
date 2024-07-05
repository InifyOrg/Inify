using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersMS.Contracts
{
    public class AccessTokenDTO
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Token { get; set; }
    }
}
