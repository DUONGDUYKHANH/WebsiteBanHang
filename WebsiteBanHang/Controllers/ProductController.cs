using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Context;

namespace WebsiteBanHang.Controllers
{
    public class ProductController : Controller
    {
        BanHangEntities objBanHangEntities = new BanHangEntities();
        // GET: Product
        public ActionResult Detail(int Id)
        {
            var objProduct = objBanHangEntities.Product_2119110319.Where(n => n.Id == Id).FirstOrDefault();
            return View(objProduct);
        }
        public ActionResult Allproduct(int? page)
        {
            //lấy all sản phẩm trong bảng product
            var lstAllProduct = objBanHangEntities.Product_2119110319.ToList();


            //số lượng item cua 1 trang
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            //sắp xếp theo id sản phẩm,sp mới đưa lên đầu
            lstAllProduct = lstAllProduct.OrderByDescending(n => n.Id).ToList();
            return View(lstAllProduct.ToPagedList(pageNumber, pageSize));
        }
    }

}