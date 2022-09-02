using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteBanHang.Models
{
    public partial class ProductMasterData
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
        [Display(Name="Tên sản phẩm")]
        public string Name { get; set; }
        [Display(Name = "Hình đại diện")]
        public string Avatar { get; set; }
        [Display(Name = "Danh mục")]
        public Nullable<int> CategoryId { get; set; }
        [Display(Name = "Mô tả ngắn")]
        public string ShortDes { get; set; }
        [Display(Name = "Mô tả đầy đủ")]
        public string FullDescription { get; set; }
        [Display(Name = "Giá gốc")]
        public Nullable<double> Price { get; set; }
        [Display(Name = "Giá khuyễn mãi")]
        public Nullable<double> PriceDiscount { get; set; }
        [Display(Name = "Loại sản phẩm")]
        public Nullable<int> TypeId { get; set; }
        public string Slug { get; set; }
        [Display(Name = "Thương hiệu")]
        public Nullable<int> BrandId { get; set; }
        [Display(Name = "Thương hiệu")]
        public Nullable<bool> ShowOnHomePage { get; set; }
        public Nullable<int> DisPlayOrder { get; set; }
        public Nullable<System.DateTime> CreatedOnUtc { get; set; }
        public Nullable<System.DateTime> UpdatedOnUtc { get; set; }
        public Nullable<bool> Deleted { get; set; }

    }
}