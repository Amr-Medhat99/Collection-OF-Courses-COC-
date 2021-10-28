using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace coc_graduation_project_.Enums
{
    public enum Gender
    {
        Male,
        Female
    }
    public class EnumValue
    {
        public string Name { get; set; }
    }
    public static class EnumExtensions
    {
        public static List<EnumValue> GetValues<T>()
        {
            List<EnumValue> values = new List<EnumValue>();
            foreach (var itemType in Enum.GetValues(typeof(T)))
            {
                //For each value of this enumeration, add a new EnumValue instance
                values.Add(new EnumValue()
                {
                    Name = Enum.GetName(typeof(T), itemType)
                });
            }
            return values;
        }
    }
}
