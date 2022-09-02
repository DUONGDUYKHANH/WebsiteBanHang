using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Models;
using WebsiteBanHang.Context;

namespace WebsiteBanHang.Controllers
{
    public class PaymenController : Controller
    {
        BanHangEntities objBanHangEntities = new BanHangEntities();
        // GET: Paymen
        public ActionResult Index()
        {
            if (Session["idUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var lstCart = (List<CartModel>)Session["cart"];
                Order_2119110319 objOrder = new Order_2119110319();
                objOrder.Name = "DonHang-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                objOrder.UserId = int.Parse(Session["idUser"].ToString());
                objOrder.CreatedOnUtc = DateTime.Now;
                objOrder.Status = 1;
                objBanHangEntities.Order_2119110319.Add(objOrder);
                objBanHangEntities.SaveChanges();

                int intOrderId = objOrder.Id;

                List<OrderDetail_2119110319> lstOrderDetail = new List<OrderDetail_2119110319>();

                foreach (var item in lstCart)
                {
                    OrderDetail_2119110319 obj = new OrderDetail_2119110319();
                    obj.Quantity = item.Quantity;
                    obj.OrderId = intOrderId;
                    obj.ProductId = item.Product_2119110319.Id;
                    lstOrderDetail.Add(obj);
                }
                objBanHangEntities.OrderDetail_2119110319.AddRange(lstOrderDetail);
                objBanHangEntities.SaveChanges();

            }
            return View();
        }
    }
}