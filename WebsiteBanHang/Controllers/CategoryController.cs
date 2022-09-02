using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Context;
using WebsiteBanHang.Models;

namespace WebsiteBanHang.Controllers
{
    public class CategoryController : Controller
    {
        BanHangEntities objBanHangEntities = new BanHangEntities();
        // GET: Category
        public ActionResult Index()
        {
            var lstCategory = objBanHangEntities.Category_2119110319.ToList();
            return View(lstCategory);
        }
        public ActionResult ProductCategory(int Id)
        {
            HomeModel objHomeModel = new HomeModel();
            objHomeModel.ListCategory = objBanHangEntities.Category_2119110319.ToList();
            objHomeModel.ListProduct = objBanHangEntities.Product_2119110319.Where(n => n.CategoryId == Id).ToList();
            return View(objHomeModel);
        }
    }

}