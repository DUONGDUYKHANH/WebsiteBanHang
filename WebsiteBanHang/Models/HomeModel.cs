using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteBanHang.Areas.Admin.Controllers;
using WebsiteBanHang.Context;

namespace WebsiteBanHang.Models
{
    public class HomeModel
    {
        public List<Product_2119110319> ListProduct { get; set; }
        public List<Category_2119110319> ListCategory { get; set; }
    }
    public class AllModel
    {
        public List<Product> ListProduct { get; set; }
        public List<Category_2119110319> ListCategory { get; set; }
        public List<Brand_2119110319> ListBrand { get; set; }
    }

}