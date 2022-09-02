using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Context;
using static WebsiteBanHang.ListtoDataTableConverter;

namespace WebsiteBanHang.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        BanHangEntities objBanHangEntities = new BanHangEntities();
        // GET: Admin/Product
        public ActionResult Index(string currentFiler, string SearchString, int? page)
        {
            var lstProduct = new List<Product_2119110319>();
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
                lstProduct = objBanHangEntities.Product_2119110319.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstProduct = objBanHangEntities.Product_2119110319.ToList();
            }
            ViewBag.CurrentFiler = SearchString;
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            lstProduct = lstProduct.OrderByDescending(n => n.Id).ToList();
            return View(lstProduct.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {

            this.LoadData();
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Product_2119110319 objProduct)
        {
            this.LoadData();
            if (ModelState.IsValid) 
            { 
            try
            {
                if (objProduct.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
                    string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
                    fileName = fileName + extension;
                    objProduct.Avatar = fileName;
                    objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images"), fileName));
                }
                objProduct.CreatedOnUtc = DateTime.Now;
                objBanHangEntities.Product_2119110319.Add(objProduct);
                objBanHangEntities.SaveChanges();


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
             return View(objProduct);
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var objProduct = objBanHangEntities.Product_2119110319.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objProduct = objBanHangEntities.Product_2119110319.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpPost]
        public ActionResult Delete(Product_2119110319 objPro)
        {
            var objProduct = objBanHangEntities.Product_2119110319.Where(n => n.Id == objPro.Id).FirstOrDefault();

            objBanHangEntities.Product_2119110319.Remove(objProduct);
            objBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objProduct = objBanHangEntities.Product_2119110319.Where(n => n.Id == id).FirstOrDefault();
            Session["imgpro"] = objProduct.Avatar;
            this.LoadData();
            return View(objProduct);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(int id, Product_2119110319 objProduct)
        {
            this.LoadData();
            if (objProduct.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
                string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
                fileName = fileName + extension;
                objProduct.Avatar = fileName;
                objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
            }
            else
            {
                objProduct.Avatar = Session["imgpro"].ToString();
            }
            objBanHangEntities.Entry(objProduct).State = EntityState.Modified;
            objBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        void LoadData()
        {
            Common objCommon = new Common();
            var lstCat = objBanHangEntities.Category_2119110319.ToList();
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dtCategory = converter.ToDataTable(lstCat);
            ViewBag.ListCategory = objCommon.ToSelectList(dtCategory, "Id", "Name");

            var lstBrand = objBanHangEntities.Brand_2119110319.ToList();
            DataTable dtBrand = converter.ToDataTable(lstBrand);
            ViewBag.ListBrand = objCommon.ToSelectList(dtBrand, "Id", "Name");

            List<ProductType> lstProductType = new List<ProductType>();
            ProductType objProductType = new ProductType();
            objProductType.Id = 01;
            objProductType.Name = "Giảm giá sốc";
            lstProductType.Add(objProductType);


            objProductType = new ProductType();
            objProductType.Id = 02;
            objProductType.Name = "Đề xuất";
            lstProductType.Add(objProductType);

            DataTable dtProductType = converter.ToDataTable(lstProductType);
            ViewBag.ProductType = objCommon.ToSelectList(dtProductType, "Id", "Name");
        }
    }
}