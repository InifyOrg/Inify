using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokensMS.Infrastructure.Domain.Entities
{
    public class Platform
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Slug { get; set; }

    }
}
