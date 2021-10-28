using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coc_graduation_project_.Models
{
    public class Favorite
    {
        public int FavoriteID { get; set; }
        public Student student { get; set; }
        public int studentID { get; set; }
        public ICollection<FavoriteSubCategory> FavoriteSubCategory { get; set; }
    }
}
