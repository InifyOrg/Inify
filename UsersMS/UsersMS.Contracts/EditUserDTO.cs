using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UsersMS.Contracts
{
    public class EditUserDTO
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }


        public bool IsUpdated()
        {
            return GetProperties().Count > 0;
        }

        public List<PropertyInfo> GetProperties()
        {
            List<PropertyInfo> valueProperties = this.GetType().GetProperties().Where(p => p.Name != "Id").ToList();
            List<PropertyInfo> propertiesForUpdate = new List<PropertyInfo>();

            if (valueProperties.Count > 0)
            {
                foreach (PropertyInfo property in valueProperties)
                {
                    if (property.GetValue(this, null) != null)
                    {
                        propertiesForUpdate.Add(property);
                    }
                }
            }

            return propertiesForUpdate;
        }

    }
}
