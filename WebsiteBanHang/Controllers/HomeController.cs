using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Context;
using WebsiteBanHang.Models;

namespace WebsiteBanHang.Controllers
{
    public class HomeController : Controller
    {
        BanHangEntities objBanHangEntities = new BanHangEntities();
        // GET: Admin/Home
        public ActionResult Index()
        {
            HomeModel objHomeModel = new HomeModel();

            objHomeModel.ListCategory = objBanHangEntities.Category_2119110319.ToList();

            objHomeModel.ListProduct = objBanHangEntities.Product_2119110319.ToList();
            return View(objHomeModel);
        }
        [HttpGet]
        public ActionResult Register()
        {

            return View();
        }
        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Users_2119110319 _user)
        {
            if (ModelState.IsValid)
            {

                var check = objBanHangEntities.Users_2119110319
                    .FirstOrDefault(s => s.Email == _user.Email);
                if (check == null)
                {
                    _user.Password = GetMD5(_user.Password);
                    objBanHangEntities.Configuration.ValidateOnSaveEnabled = false;
                    objBanHangEntities.Users_2119110319.Add(_user);
                    objBanHangEntities.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }
            }
            return View(_user);
        }
      
        public ActionResult Search(string currentFilter, string SearchString, int? page)
        {
            var lstProduct = new List<Product_2119110319>();
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
                lstProduct = objBanHangEntities.Product_2119110319.Where(n =>n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                //lấy all sản phẩm trong bảng product
                lstProduct = objBanHangEntities.Product_2119110319.Where(n => n.Id != 0).ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            //số lượng item cua 1 trang
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            //sắp xếp theo id sản phẩm,sp mới đưa lên đầu
            lstProduct = lstProduct.OrderByDescending(n => n.Id).ToList();
            return View(lstProduct.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {
                if (email.Length == 0)
                {
                    ViewBag.error1 = "*Vui lòng nhập email";
                }
                if (password.Length == 0)
                {
                    ViewBag.error2 = "*Vui lòng nhập password";
                }
                else
                {
                    var f_password = GetMD5(password);
                    var data = objBanHangEntities.Users_2119110319.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password)).ToList();
                    if (data.Count() > 0)
                    {
                        //add session
                        Session["FullName"] = data.FirstOrDefault().FirstName + " " + data.FirstOrDefault().LastName;
                        Session["Email"] = data.FirstOrDefault().Email;
                        Session["idUser"] = data.FirstOrDefault().Id;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.error = "Login failed";
                        return RedirectToAction("Login");
                    }
                    return View();
                }
                
            }
            return View();
        }
        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }



        //create a string MD5
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
    }
}