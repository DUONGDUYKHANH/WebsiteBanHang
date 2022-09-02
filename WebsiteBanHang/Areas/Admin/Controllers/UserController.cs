using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Context;

namespace WebsiteBanHang.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        // GET: Admin/User
        BanHangEntities objBanHangEntities = new BanHangEntities();

        public ActionResult Index(string currentFilter, string SearchString, int? page)
        { 
            var lstUser = new List<Users_2119110319>();
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                //lấy danh sách sản phẩm theo từ khóa tìm kiếm
                lstUser = objBanHangEntities.Users_2119110319.Where(n =>n.Email.Contains(SearchString)).ToList();
            }
            else
            {
                //lấy all sản phẩm trong bảng product
                lstUser = objBanHangEntities.Users_2119110319.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            //số lượng item cua 1 trang
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            //sắp xếp theo id sản phẩm,sp mới đưa lên đầu
            lstUser = lstUser.OrderByDescending(n => n.Id).ToList();
            return View(lstUser.ToPagedList(pageNumber, pageSize));
        }
        //Thêm tài khoản
        [HttpGet]
      
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;
            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

        //Sửa tài khoản
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var objUser = objBanHangEntities.Users_2119110319.Where(n => n.Id == Id).FirstOrDefault();
            return View(objUser);
        }
        [HttpPost]
        public ActionResult Edit(int Id, Users_2119110319 objUser)
        {
            if (ModelState.IsValid)
            {
                objUser.Password = GetMD5(objUser.Password);
                objBanHangEntities.Configuration.ValidateOnSaveEnabled = false;
                objBanHangEntities.Entry(objUser).State = EntityState.Modified;
                objBanHangEntities.SaveChanges();
                return RedirectToAction("Index", "User");
            }
            return View("Index"); //objUser      
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var objUser = objBanHangEntities.Users_2119110319.Where(n => n.Id == id).FirstOrDefault();
            return View(objUser);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objUser = objBanHangEntities.Users_2119110319.Where(n => n.Id == id).FirstOrDefault();
            return View(objUser);
        }
        [HttpPost]
        public ActionResult Delete(Users_2119110319 objUse)
        {
            var objUser = objBanHangEntities.Users_2119110319.Where(n => n.Id == objUse.Id).FirstOrDefault();
            objBanHangEntities.Users_2119110319.Remove(objUser);
            objBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}