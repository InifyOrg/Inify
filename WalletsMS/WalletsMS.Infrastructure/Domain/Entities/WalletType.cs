using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletsMS.Infrastructure.Domain.Entities
{
    public class WalletType
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Title { get; set; }

    }
}
