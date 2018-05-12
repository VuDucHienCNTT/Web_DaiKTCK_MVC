using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_AMO_MVC.Models;
using PagedList;
using PagedList.Mvc;
namespace Web_AMO_MVC.Areas.Admin.Controllers
{
    public class TaiKhoanController : Controller
    {
        // GET: Admin/TaiKhoan
        public ActionResult Index(string tukhoa, int? page)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                int itemPageSize = 20;
                int pageNumber = (page ?? 1);
                if (!String.IsNullOrEmpty(tukhoa))
                {
                    ViewBag.TuKhoa = tukhoa;
                    var lstResult = db.TAIKHOANs.Where(n => n.HoTen.Contains(tukhoa) || n.Email.Contains(tukhoa)).OrderByDescending(n => n.TrangThai).ToPagedList(pageNumber, itemPageSize);
                    return View("Index", lstResult);
                }
                else
                {
                    var tatcataikhoan = db.TAIKHOANs.ToList();
                    ViewBag.Count = tatcataikhoan.Count;
                    var dstaikhoan = db.TAIKHOANs.OrderBy(n => n.Quyen).ThenBy(n => n.TrangThai).ToPagedList(pageNumber, itemPageSize);
                    return View(dstaikhoan);

                }
            }
        }
        // POST: Admin/TaiKhoan/Sua
        public ActionResult Sua(TAIKHOAN tk)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                TAIKHOAN taikhoan = db.TAIKHOANs.SingleOrDefault(n => n.IdTK == tk.IdTK);
                taikhoan.HoTen = tk.HoTen;
                taikhoan.TrangThai = tk.TrangThai;
                taikhoan.Quyen = tk.Quyen;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        // GET: Admin/TaiKhoan/DangKy
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        // POST: Admin/TaiKhoan/DangKy
        [HttpPost]
        public ActionResult DangKy(TAIKHOAN tk)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                var checkMail = db.TAIKHOANs.SingleOrDefault(n => n.Email.Trim() == tk.Email.Trim());
                var checkTen = db.TAIKHOANs.SingleOrDefault(n => n.HoTen.Trim() == tk.HoTen.Trim());
                if (checkTen == null)
                {
                    if (checkMail == null)
                    {
                        tk.Password = MaHoaMd5.EncryptMD5(tk.Email + tk.Password);
                        tk.RetypePassword = MaHoaMd5.EncryptMD5(tk.Email + tk.RetypePassword);
                        tk.TrangThai = "Hoạt động";
                        tk.Quyen = "User";
                        db.TAIKHOANs.Add(tk);
                        db.SaveChanges();
                        ViewBag.tbDangKy = "Người dùng: " + tk.HoTen + " đăng ký thành công!";
                        return View();
                    }
                    else
                    {
                        ViewBag.tbTonTai = "Email: " + tk.Email + " đã tồn tại!";
                        return View();
                    }
                }
                else
                {
                    ViewBag.tbTonTai = "Tên người dùng: " + tk.HoTen + " đã tồn tại!";
                    return View();
                }
            }
        }

        // GET: Admin/TaiKhoan/DangNhap
        [HttpGet]
        public ActionResult DangNhap()
        {
            if (Request["returnUrl"] != null)
                Session["returnUrl"] = Request["returnUrl"];
            return View();
        }
        // POST: Admin/TaiKhoan/DangNhap
        [HttpPost]
        public ActionResult DangNhap(TAIKHOAN tk)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                tk.Password = MaHoaMd5.EncryptMD5(tk.Email + tk.Password);
                var checkTKAdmin = db.TAIKHOANs.SingleOrDefault(n => n.Email.Trim().Equals(tk.Email) && n.Password.Trim().Equals(tk.Password) && n.Quyen.Equals("Admin") && n.TrangThai.Equals("Hoạt động"));
                if (checkTKAdmin != null)
                {
                    Session["IdTK"] = checkTKAdmin.IdTK.ToString();
                    Session["HoTen"] = checkTKAdmin.HoTen.ToString();
                    Session["Email"] = checkTKAdmin.Email.ToString();
                    Session["Quyen"] = "Admin";
                    Session["AnhDaiDien"] = checkTKAdmin.AnhDaiDien.ToString();
                    return Redirect("/Admin/Home/Index");
                }
                var checkTKUser = db.TAIKHOANs.SingleOrDefault(n => n.Email.Trim().Equals(tk.Email) && n.Password.Trim().Equals(tk.Password) && n.Quyen.Equals("User") && n.TrangThai.Equals("Hoạt động"));
                if (checkTKUser != null)
                {
                    Session["IdTK"] = checkTKUser.IdTK.ToString();
                    Session["HoTen"] = checkTKUser.HoTen.ToString();
                    Session["Quyen"] = "User";
                    if (Session["returnUrl"] == null)
                        return Redirect("/Home/Index");
                    else
                        return Redirect(Session["returnUrl"].ToString());
                }
                else
                {
                    ViewBag.tbDangNhap = "Email, mật khẩu sai hoặc tài khoản đã bị khóa";
                    return View();
                }

            }
        }

        // Admin/TaiKhoan/DangXuat
        public void DangXuat()
        {
            Session.Clear();
            Session["IdTK"] = null;
            Session["HoTen"] = null;
            Session["Email"] = null;
            Session["AnhDaiDien"] = null;
            Response.Redirect("~/Home/Index");
        }
    }
}