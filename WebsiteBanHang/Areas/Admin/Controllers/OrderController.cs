using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Context;

namespace WebsiteBanHang.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {

        BanHangEntities objBanHangEntities = new BanHangEntities();
        // GET: Admin/Order
        public ActionResult Index(string SearchString, string currentFiler, int? page)
        {
            var lstOrder = new List<Order_2119110319>();
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFiler;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                // lay san pham theo tu khoa tim kiem
                lstOrder = objBanHangEntities.Order_2119110319.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                ///lay all san pham trong categỏy
                lstOrder = objBanHangEntities.Order_2119110319.ToList();
            }
            ViewBag.CurrentFiler = SearchString;
            //so luong item cua 1 trang = 4
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            //sap xep theo id san pham,sp moi dua len dau
            return View(lstOrder.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            this.loadData();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Order_2119110319 objOrder)
        {
            this.loadData();
            if (ModelState.IsValid)
            {
                try
                {
                    objBanHangEntities.Configuration.ValidateOnSaveEnabled = false;
                    objBanHangEntities.Order_2119110319.Add(objOrder);
                    objBanHangEntities.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(objOrder);
        }

        public ActionResult Details(int id)
        {
            var objOrder = objBanHangEntities.Order_2119110319.Where(n => n.Id == id).FirstOrDefault();
            return View(objOrder);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objOrder = objBanHangEntities.Order_2119110319.Where(n => n.Id == id).FirstOrDefault();
            return View(objOrder);
        }
        [HttpPost]
        public ActionResult Delete(Order_2119110319 objOr)
        {
            var objOrder = objBanHangEntities.Order_2119110319.Where(n => n.Id == objOr.Id).FirstOrDefault();
            objBanHangEntities.Order_2119110319.Remove(objOrder);
            objBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            this.loadData();
            var objOrder = objBanHangEntities.Order_2119110319.Where(n => n.Id == id).FirstOrDefault();
            return View(objOrder);
        }
        [HttpPost]
        public ActionResult Edit(int id, Order_2119110319 objOrder)
        {
            this.loadData();
            objBanHangEntities.Entry(objOrder).State = EntityState.Modified;
            objBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        void loadData()
        {
            Common objCommon = new Common();
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            List<Order_2119110319> lstOrder = new List<Order_2119110319>();
            Order_2119110319 objOrder = new Order_2119110319();
            objOrder.Status = 01;
            objOrder.Name = "Đang giao";
            lstOrder.Add(objOrder);

            objOrder = new Order_2119110319();
            objOrder.Status = 02;
            objOrder.Name = "Đã giao";
            lstOrder.Add(objOrder);

            objOrder = new Order_2119110319();
            objOrder.Status = 03;
            objOrder.Name = "Hủy hàng";
            lstOrder.Add(objOrder);

            objOrder = new Order_2119110319();
            objOrder.Status = 04;
            objOrder.Name = "Trả hàng";
            lstOrder.Add(objOrder);

            DataTable dtOrder = converter.ToDataTable(lstOrder);
            //convert sang select list dang value, text
            ViewBag.Order = objCommon.ToSelectList(dtOrder, "Id", "Name");
        }
    }
}