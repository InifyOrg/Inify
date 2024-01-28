using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersMS.Contracts
{
    public class UserDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime RegisteredAt { get; set; }

    }
}
