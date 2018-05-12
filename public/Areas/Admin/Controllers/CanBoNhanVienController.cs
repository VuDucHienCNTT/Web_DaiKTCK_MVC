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
    public class CanBoNhanVienController : Controller
    {
        // GET: Admin/CanBoNhanVien
        [HttpGet]
        public ActionResult Index(string tukhoa, int? page)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                int itemPageSize = 20;
                int pageNumber = (page ?? 1);
                ViewBag.IdPhongBan = new SelectList(db.PHONGBANs.ToList(), "IdPb", "TenPhong");
                ViewBag.IdTaiKhoan = new SelectList(db.TAIKHOANs.ToList(), "IdTK", "HoTen");
                if (!String.IsNullOrEmpty(tukhoa))
                {
                    ViewBag.TuKhoa = tukhoa;
                    var lstResult = db.CANBOes.Where(n => n.HoTen.Contains(tukhoa) || n.ChucVu.Contains(tukhoa) || n.BangCap.Contains(tukhoa) || n.QueQuan.Contains(tukhoa)).OrderByDescending(n => n.HoTen).ToPagedList(pageNumber, itemPageSize);
                    return View("Index", lstResult);
                }
                else
                {
                    var tatcacanbo = db.CANBOes.ToList();
                    ViewBag.Count = tatcacanbo.Count;
                    var dscanbo = db.CANBOes.OrderBy(n => n.IdCB).ToPagedList(pageNumber, itemPageSize);
                    return View(dscanbo);
                }

            }
        }
        // POST: Admin/CanBoNhanVien/Them
        [ValidateInput(false)]
        public ActionResult Them(CANBO cb)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                cb.NgayCapNhat = DateTime.Parse(DateTime.Now.ToString());
                ViewBag.IdPhongBan = new SelectList(db.PHONGBANs.ToList(), "IdPb", "TenPhongBan");
                ViewBag.IdTaiKhoan = new SelectList(db.TAIKHOANs.ToList(), "IdTK", "HoTen");
                db.CANBOes.Add(cb);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        // POST: Admin/CanBoNhanVien/Sua
        [ValidateInput(false)]
        public ActionResult Sua(CANBO cb)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                CANBO canbo = db.CANBOes.SingleOrDefault(n => n.IdCB == cb.IdCB);
                if (canbo != null)
                {
                    canbo.HoTen = cb.HoTen;
                    canbo.ChucVu = cb.ChucVu;
                    canbo.BangCap = cb.BangCap;
                    canbo.QueQuan = cb.QueQuan;
                    canbo.Email = cb.Email;
                    if (cb.NgaySinh == null)
                    {
                        canbo.NgaySinh = canbo.NgaySinh;
                    }
                    else
                    {
                        canbo.NgaySinh = cb.NgaySinh;
                    }
                    canbo.NgayCapNhat = DateTime.Parse(DateTime.Now.ToString());
                    canbo.SoDT = cb.SoDT;
                    canbo.AnhMoTa = cb.AnhMoTa;
                    canbo.ChiTiet = cb.ChiTiet;
                    canbo.IdTaiKhoan = cb.IdTaiKhoan;
                    canbo.IdPhongBan = cb.IdPhongBan;
                    ViewBag.IdPhongBan = new SelectList(db.PHONGBANs.ToList(), "IdPB", "TenPhong", canbo.IdPhongBan);
                    ViewBag.IdTaiKhoan = new SelectList(db.TAIKHOANs.ToList(), "IdTK", "HoTen");
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }

        // POST: Admin/CanBoNhanVien/Xoa
        public ActionResult Xoa(int id)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                CANBO canbo = db.CANBOes.SingleOrDefault(n => n.IdCB == id);
                db.CANBOes.Remove(canbo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        //GET: Admin/CanBoNhanVien/ChiTiet 
        public ActionResult ChiTiet(int id = 0)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {

                CANBO cb = db.CANBOes.SingleOrDefault(n => n.IdCB == id);
                ViewBag.IdPhongBan = new SelectList(db.PHONGBANs.ToList(), "IdPB", "TenPhong");
                ViewBag.IdTaiKhoan = new SelectList(db.TAIKHOANs.ToList(), "IdTK", "HoTen");
                if (cb == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                return View(cb);
            }
        }
    }
}