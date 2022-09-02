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

namespace WebsiteBanHang.Areas.Admin.Controllers
{
    public class BrandController : Controller
    {
        BanHangEntities objBanHangEntities = new BanHangEntities();
        // GET: Admin/Brand
        public ActionResult Index(string currentFiler, string SearchString, int? page)
        {
            var lstBrand = new List<Brand_2119110319>();
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
                lstBrand = objBanHangEntities.Brand_2119110319.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstBrand = objBanHangEntities.Brand_2119110319.ToList();
            }
            ViewBag.CurrentFiler = SearchString;
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            lstBrand = lstBrand.OrderByDescending(n => n.Id).ToList();
            return View(lstBrand.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {


            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Brand_2119110319 objBrand)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    if (objBrand.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objBrand.ImageUpload.FileName);
                        string extension = Path.GetExtension(objBrand.ImageUpload.FileName);
                        fileName = fileName + extension;
                        objBrand.Avatar = fileName;
                        objBrand.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images"), fileName));
                    }
                    objBrand.CreatedOnUtc = DateTime.Now;
                    objBanHangEntities.Brand_2119110319.Add(objBrand);
                    objBanHangEntities.SaveChanges();


                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View(objBrand);
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var objProduct = objBanHangEntities.Brand_2119110319.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objBrand = objBanHangEntities.Brand_2119110319.Where(n => n.Id == id).FirstOrDefault();
            return View(objBrand);
        }
        [HttpPost]
        public ActionResult Delete(Brand_2119110319 objPro)
        {
            var objBrand = objBanHangEntities.Brand_2119110319.Where(n => n.Id == objPro.Id).FirstOrDefault();

            objBanHangEntities.Brand_2119110319.Remove(objBrand);
            objBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objBrand = objBanHangEntities.Brand_2119110319.Where(n => n.Id == id).FirstOrDefault();
            Session["imgBrand"] = objBrand.Avatar;
            return View(objBrand);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(int id, Brand_2119110319 objBrand)
        {
            if (objBrand.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objBrand.ImageUpload.FileName);
                string extension = Path.GetExtension(objBrand.ImageUpload.FileName);
                fileName = fileName + extension;
                objBrand.Avatar = fileName;
                objBrand.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
            }
            else
            {
                objBrand.Avatar = Session["imgBrand"].ToString();
            }
            objBanHangEntities.Entry(objBrand).State = EntityState.Modified;
            objBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}