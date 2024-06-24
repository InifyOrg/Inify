using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletsMS.Contract;

namespace WalletsMS.Infrastructure.Domain.Entities
{
    public class Wallet
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public WalletType WalletType { get; set; }
        [Required]
        public long UserId { get; set; }

    }
}
