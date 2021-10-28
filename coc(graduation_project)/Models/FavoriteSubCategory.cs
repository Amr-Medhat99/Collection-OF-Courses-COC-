using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coc_graduation_project_.Models
{
    public class FavoriteSubCategory
    {
        public int SubCategoryID { get; set; }
        //public virtual Course Course { get; set; }
        //public int FavoriteID { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public int FavoriteID { get; set; }
        public virtual Favorite Favorite { get; set; }
    }
}