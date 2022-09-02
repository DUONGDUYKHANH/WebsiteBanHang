using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteBanHang.Models
{
    public class BrandMasterData
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên thương hiệu")]
        [Display(Name = "Tên thương hiệu")]
        public string Name { get; set; }
        [Display(Name = "Hình đại diện")]
        public string Avatar { get; set; }
        [Display(Name = "Danh mục")]
        public string Slug { get; set; }
        [Display(Name = "Hiển thị trên Trang chủ")]
        public Nullable<bool> ShowOnHomePage { get; set; }
        [Display(Name = "Thứ tự hiển thị")]
        public Nullable<int> DisplayOrder { get; set; }
        [Display(Name = "Thời gian tạo")]
        public Nullable<System.DateTime> CreatedOnUtc { get; set; }
        public Nullable<System.DateTime> UpdatedOnUtc { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public object ImageUpload { get; internal set; }
    }
}