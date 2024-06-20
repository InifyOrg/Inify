using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletsMS.Contract
{
    public class WalletDTO
    {
        public long Id {  get; set; }
        public string Address { get; set; }
        public WalletTypeDTO walletType { get; set; }
        public long UserId { get; set; }
    }
}
