using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletsMS.Contract
{
    public class AddWalletDTO
    {
        public string Address { get; set; }
        public WalletTypeDTO WalletType { get; set; }
        public long UserId { get; set; }
    }
}
