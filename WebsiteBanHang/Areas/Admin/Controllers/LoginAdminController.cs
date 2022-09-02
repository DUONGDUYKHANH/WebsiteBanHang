using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Context;

namespace WebsiteBanHang.Areas.Admin.Controllers
{
    public class LoginAdminController : Controller
    {
        BanHangEntities objBanHangEntities = new BanHangEntities();
        // GET: Admin/LoginAdmin
        public ActionResult Index()
        {
            return View();
        }

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
        [HttpGet]
        public ActionResult LoginAdmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginAdmin(Users_2119110319 _user)
        {
            if (ModelState.IsValid)
            {

                string error = "";
                var f_password = GetMD5(_user.Password);
                var data = objBanHangEntities.Users_2119110319.Where(s => s.Email.Equals(_user.Email) && s.Password.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["FullName"] = data.FirstOrDefault().FirstName + " " + data.FirstOrDefault().LastName;
                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["idUser"] = data.FirstOrDefault().Id;
                    Session["isAdmin"] = data.FirstOrDefault().IsAdmin;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    error = "Tài khoản Admin không đúng!";
                }
                ViewBag.StrError = "<div class='text-danger'>" + error + "</div>";
                return View();
            }
            return View();
        }
        //Logout
        public ActionResult LogoutAdmin()
        {
            Session.Clear();//remove session
            return RedirectToAction("LoginAdmin", "LoginAdmin");
        }
    }
}