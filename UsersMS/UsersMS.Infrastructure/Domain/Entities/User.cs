using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using UsersMS.Contracts;

namespace UsersMS.Infrastructure.Domain.Entities
{
    public class User
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime RegisteredAt { get; set; }

        public User UpdateFromEditUserDto(EditUserDTO userToEditDTO)
        {
            List<PropertyInfo> properties = userToEditDTO.GetProperties();

            if (properties.Count > 0)
            {
                Type typeOfuser = this.GetType();
                Type typeOfuserDTO = userToEditDTO.GetType();

                foreach (PropertyInfo property in properties)
                {
                    object valueToCopy = typeOfuserDTO.GetProperty(property.Name).GetValue(userToEditDTO, null);

                    PropertyInfo? propertyToUpdate = typeOfuser.GetProperty(property.Name);

                    if (propertyToUpdate != null)
                    {
                        propertyToUpdate.SetValue(this, valueToCopy, null);

                    }
                }
            }

            return this;
        }

    }
}
