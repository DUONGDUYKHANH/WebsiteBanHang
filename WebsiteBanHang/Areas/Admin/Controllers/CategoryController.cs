using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Context;
using WebsiteBanHang.Models;

namespace WebsiteBanHang.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        BanHangEntities objBanHangEntities = new BanHangEntities();
        // GET: Admin/Category
        public ActionResult Index(string currentFiler, string SearchString, int? page)
        {
            var lstCategory = new List<Category_2119110319>();
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
                lstCategory = objBanHangEntities.Category_2119110319.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstCategory = objBanHangEntities.Category_2119110319.ToList();
            }
            ViewBag.CurrentFiler = SearchString;
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            lstCategory = lstCategory.OrderByDescending(n => n.Id).ToList();
            return View(lstCategory.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Category_2119110319 objCategory)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    if (objCategory.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objCategory.ImageUpload.FileName);
                        string extension = Path.GetExtension(objCategory.ImageUpload.FileName);
                        fileName = fileName + extension;
                        objCategory.Avatar = fileName;
                        objCategory.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images"), fileName));
                    }
                    objCategory.CreatedOnUtc = DateTime.Now;
                    objBanHangEntities.Category_2119110319.Add(objCategory);
                    objBanHangEntities.SaveChanges();


                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View(objCategory);
        }
        public ActionResult Details(int id)
        {
            var objCategory = objBanHangEntities.Category_2119110319.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategory);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objCategory = objBanHangEntities.Category_2119110319.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategory);
        }
        [HttpPost]
        public ActionResult Delete(Product_2119110319 objPro)
        {
            var objCategory = objBanHangEntities.Category_2119110319.Where(n => n.Id == objPro.Id).FirstOrDefault();

            objBanHangEntities.Category_2119110319.Remove(objCategory);
            objBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objCategory = objBanHangEntities.Category_2119110319.Where(n => n.Id == id).FirstOrDefault();
            Session["imgCate"] = objCategory.Avatar;
            return View(objCategory);
        }
        [HttpPost]
        public ActionResult Edit(int id, Category_2119110319 objCategory)
        {
            if (objCategory.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objCategory.ImageUpload.FileName);
                string extension = Path.GetExtension(objCategory.ImageUpload.FileName);
                fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                objCategory.Avatar = fileName;
                objCategory.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
            }
            else
            {
                objCategory.Avatar = Session["imgCate"].ToString();
            }
            objBanHangEntities.Entry(objCategory).State = EntityState.Modified;
            objBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}