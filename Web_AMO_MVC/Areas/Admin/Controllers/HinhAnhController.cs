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
    public class HinhAnhController : Controller
    {

        public ActionResult Index(string tukhoa, int? page)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                int itemPageSize = 20;
                int pageNumber = (page ?? 1);
                ViewBag.IdAlbum = new SelectList(db.ALBUMs.ToList(), "IdAlbum", "TenAlbum");
                ViewBag.IdTaiKhoan = new SelectList(db.TAIKHOANs.ToList(), "IdTK", "HoTen");
                if (!String.IsNullOrEmpty(tukhoa))
                {

                    var lstResult = db.HINHANHs.Where(n => n.TenHinhAnh.Contains(tukhoa)).OrderByDescending(n => n.NgayDang).ToPagedList(pageNumber, itemPageSize);
                    ViewBag.Tukhoa = tukhoa;
                    return View("Index", lstResult);
                }
                else
                {
                    var dshinhanh1 = db.HINHANHs.ToList();
                    ViewBag.Count = dshinhanh1.Count();
                    var dshinhanh = db.HINHANHs.OrderByDescending(n => n.IdHinhAnh).ToPagedList(pageNumber, itemPageSize);
                    return View(dshinhanh);
                }
            }
        }
        // POST: Admin/HinhAnh/Them
        public ActionResult Them(HINHANH ha)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                if (ha.NgayDang == null)
                {
                    ha.NgayDang = DateTime.Parse(DateTime.Now.ToString());
                }
                else
                {
                    ha.NgayDang = ha.NgayDang;
                }
                ViewBag.IdAlbum = new SelectList(db.ALBUMs.ToList(), "IdAlbum", "TenAlbum");
                ViewBag.IdTaiKhoan = new SelectList(db.TAIKHOANs.ToList(), "IdTK", "HoTen");
                db.HINHANHs.Add(ha);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        // POST: Admin/HinhAnh/Sua
        public ActionResult Sua(HINHANH ha)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                HINHANH hinhanh = db.HINHANHs.SingleOrDefault(n => n.IdHinhAnh == ha.IdHinhAnh);
                if (hinhanh != null)
                {
                    hinhanh.TenHinhAnh = ha.TenHinhAnh;
                    if (ha.NgayDang == null)
                    {
                        hinhanh.NgayDang = hinhanh.NgayDang;
                    }
                    else
                    {
                        hinhanh.NgayDang = ha.NgayDang;
                    }

                    hinhanh.HinhAnh1 = ha.HinhAnh1;
                    hinhanh.IdAlbum = ha.IdAlbum;
                    ViewBag.IdAlbum = new SelectList(db.ALBUMs.ToList(), "IdAlbum", "TenAlbum");
                    ViewBag.IdTaiKhoan = new SelectList(db.TAIKHOANs.ToList(), "IdTK", "HoTen");
                    hinhanh.IdTaiKhoan = ha.IdTaiKhoan;
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
        // POST: Admin/HinhAnh/Xoa
        public ActionResult Xoa(int id)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                HINHANH ha = db.HINHANHs.SingleOrDefault(n => n.IdHinhAnh == id);
                if (ha != null)
                {
                    db.HINHANHs.Remove(ha);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

    }
}