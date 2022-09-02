using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebsiteBanHang.Models;

namespace WebsiteBanHang.Context
{
    [MetadataType(typeof(UserMasterData))]
    public partial class User_2119110319
    {
        [NotMapped]
        public System.Web.HttpPostedFileBase ImageUpload { get; set; }
    }

    [MetadataType(typeof(ProductMasterData))]
    public partial class Product_2119110319
    {
        [NotMapped]
        public System.Web.HttpPostedFileBase ImageUpload { get; set; }
    }
    [MetadataType(typeof(BrandMasterData))]
    public partial class Brand_2119110319
    {
        [NotMapped]
        public System.Web.HttpPostedFileBase ImageUpload { get; set; }
    }
    [MetadataType(typeof(CategoryMasterData))]
    public partial class Category_2119110319
    {
        [NotMapped]
        public System.Web.HttpPostedFileBase ImageUpload { get; set; }
    }
}