using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersMS.Infrastructure.Domain.Entities
{
    public class AccessToken
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public User User{ get; set; }
        [Required]
        public string Token { get; set; }
    }
}
