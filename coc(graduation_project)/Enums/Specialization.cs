using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coc_graduation_project_.Enums
{
    public enum Specialization
    {
        WebDevelopment,
        AndriodDevelopment,
        IT,
        EmbededSystem,
        Grphics
    }
    public class EnumValueSpecialization
    {
        public string Name { get; set; }
    }
    public static class EnumExtensionsSpecialization
    {
        public static List<EnumValueSpecialization> GetValues<T>()
        {
            List<EnumValueSpecialization> values = new List<EnumValueSpecialization>();
            foreach (var itemType in Enum.GetValues(typeof(T)))
            {
                //For each value of this enumeration, add a new EnumValue instance
                values.Add(new EnumValueSpecialization()
                {
                    Name = Enum.GetName(typeof(T), itemType)
                });
            }
            return values;
        }
    }
}
