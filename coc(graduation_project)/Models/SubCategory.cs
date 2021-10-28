using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coc_graduation_project_.Models
{
    public class SubCategory
    {
        public int SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public string SubCategoryLogo { get; set; }
        public virtual MainCategory MainCategory{ get; set; }
        public int MainCategoryID { get; set; }
        public ICollection<FavoriteSubCategory> FavoriteSubCategory { get; set; }
    }
}
