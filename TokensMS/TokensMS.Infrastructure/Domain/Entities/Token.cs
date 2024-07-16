using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokensMS.Infrastructure.Domain.Entities
{
    public class Token
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Symbol { get; set; }
        [Required]
        public string Slug { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public int Decimals { get; set; }
        [Required]
        public Platform Platform { get; set; }
        [Required]
        public WalletType WalletType { get; set; }

    }
}
