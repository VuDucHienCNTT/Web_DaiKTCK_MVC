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
    public class BaiVietController : Controller
    {
        // GET: Admin/BaiViet
        [HttpGet]
        public ActionResult Index(string tukhoa, int? page)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                int itemPageSize = 15;
                int pageNumber = (page ?? 1);
                ViewBag.IdTheLoai = new SelectList(db.THELOAIBAIVIETs.ToList(), "IdTheLoai", "TenTheLoai");
                ViewBag.IdTaiKhoan = new SelectList(db.TAIKHOANs.ToList(), "IdTK", "HoTen");
                if (!String.IsNullOrEmpty(tukhoa))
                {

                    var lstResult = db.BAIVIETs.Where(n => n.TieuDe.Contains(tukhoa) || n.TomTat.Contains(tukhoa)).OrderByDescending(n => n.IdBV).ToPagedList(pageNumber, itemPageSize);
                    ViewBag.Tukhoa = tukhoa;
                    return View("Index", lstResult);
                }
                else
                {
                    var dsbaiviet = db.BAIVIETs.ToList();
                    ViewBag.Count = dsbaiviet.Count();
                    var dstatcabaiviet = db.BAIVIETs.OrderByDescending(n => n.IdBV).ToPagedList(pageNumber, itemPageSize);
                    return View(dstatcabaiviet);
                }
            }
        }

        [HttpGet]
        public ActionResult Daduyet(int? page)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                int itemPageSize = 15;
                int pageNumber = (page ?? 1);
                var dsbaiviet = db.BAIVIETs.Where(n => n.TrangThai == "Đã duyệt").ToList();
                ViewBag.Count = dsbaiviet.Count();
                var dsbaivietdaduyet = db.BAIVIETs.Where(n => n.TrangThai == "Đã duyệt").OrderByDescending(n => n.IdBV).ToPagedList(pageNumber, itemPageSize);
                ViewBag.IdTheLoai = new SelectList(db.THELOAIBAIVIETs.ToList(), "IdTheLoai", "TenTheLoai");
                ViewBag.IdTaiKhoan = new SelectList(db.TAIKHOANs.ToList(), "IdTK", "HoTen");
                return View(dsbaivietdaduyet);
            }
        }
        [HttpGet]
        public ActionResult Chuaduyet(int? page)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                int itemPageSize = 15;
                int pageNumber = (page ?? 1);
                var dsbaiviet = db.BAIVIETs.Where(n => n.TrangThai == "Chưa duyệt").ToList();
                ViewBag.Count = dsbaiviet.Count();
                var dsbaivietchuaduyet = db.BAIVIETs.Where(n => n.TrangThai == "Chưa duyệt").OrderByDescending(n => n.IdBV).ToPagedList(pageNumber, itemPageSize);
                ViewBag.IdTheLoai = new SelectList(db.THELOAIBAIVIETs.ToList(), "IdTheLoai", "TenTheLoai");
                ViewBag.IdTaiKhoan = new SelectList(db.TAIKHOANs.ToList(), "IdTK", "HoTen");
                return View(dsbaivietchuaduyet);
            }
        }

        [HttpGet]
        public ActionResult BvLamSlide(int? page)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                int itemPageSize = 15;
                int pageNumber = (page ?? 1);
                var dsbaiviet = db.BAIVIETs.Where(n => n.TrangThai == "Đã duyệt" && n.Slide == "Hiển thị").ToList();
                ViewBag.Count = dsbaiviet.Count();
                var dsbaivietlamslide = db.BAIVIETs.Where(n => n.TrangThai == "Đã duyệt" && n.Slide == "Hiển thị").OrderByDescending(n => n.IdBV).ToPagedList(pageNumber, itemPageSize);
                ViewBag.IdTheLoai = new SelectList(db.THELOAIBAIVIETs.ToList(), "IdTheLoai", "TenTheLoai");
                ViewBag.IdTaiKhoan = new SelectList(db.TAIKHOANs.ToList(), "IdTK", "HoTen");
                return View(dsbaivietlamslide);
            }
        }



        // POST: Admin/BaiViet/Them
        [ValidateInput(false)]
        public ActionResult Them(BAIVIET bv)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                try
                {
                    if (bv.NgayDang == null)
                    {
                        bv.NgayDang = DateTime.Parse(DateTime.Now.ToString());
                    }
                    else
                    {
                        bv.NgayDang = bv.NgayDang;
                    }
                    bv.NgayCapNhat = null;
                    bv.LuotXem = 0;
                    ViewBag.IdTheLoai = new SelectList(db.THELOAIBAIVIETs.ToList(), "IdTheLoai", "TenTheLoai");
                    ViewBag.IdTaiKhoan = new SelectList(db.TAIKHOANs.ToList(), "IdTK", "HoTen");
                    db.BAIVIETs.Add(bv);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return RedirectToAction("Index");
                }
            }
        }

        // GET: Admin/BaiViet/Sua
        public ActionResult Sua(int id)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                BAIVIET baiviet = db.BAIVIETs.SingleOrDefault(n => n.IdBV == id);
                ViewBag.IdTheLoai = new SelectList(db.THELOAIBAIVIETs.ToList(), "IdTheLoai", "TenTheLoai", baiviet.IdTheLoai);
                ViewBag.IdTaiKhoan = new SelectList(db.TAIKHOANs.ToList(), "IdTK", "HoTen");
                return View(baiviet);
            }

        }
        // POST: Admin/BaiViet/Sua
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Sua(BAIVIET bv)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                BAIVIET baiviet = db.BAIVIETs.SingleOrDefault(n => n.IdBV == bv.IdBV);
                if (baiviet != null)
                {
                    baiviet.TieuDe = bv.TieuDe;
                    baiviet.TomTat = bv.TomTat;
                    if (bv.NoiDung == null)
                    {
                        baiviet.NoiDung = baiviet.NoiDung;
                    }
                    else
                    {
                        baiviet.NoiDung = bv.NoiDung;
                    }
                    if (bv.NgayDang == null)
                    {
                        baiviet.NgayDang = baiviet.NgayDang;
                    }
                    else
                    {
                        baiviet.NgayDang = bv.NgayDang;
                    }
                    if (bv.NgayCapNhat == null)
                    {
                        baiviet.NgayCapNhat = DateTime.Parse(DateTime.Now.ToString());
                    }
                    else
                    {
                        baiviet.NgayCapNhat = bv.NgayCapNhat;
                    }
                    baiviet.TacGia = bv.TacGia;
                    baiviet.IdTheLoai = bv.IdTheLoai;
                    baiviet.Slide = bv.Slide;
                    baiviet.TrangThai = bv.TrangThai;
                    baiviet.IdTaiKhoan = bv.IdTaiKhoan;
                    baiviet.AnhBV = bv.AnhBV;
                    ViewBag.IdTaiKhoan = new SelectList(db.TAIKHOANs.ToList(), "IdTK", "HoTen", baiviet.IdTaiKhoan);
                    ViewBag.IdTaiKhoan = new SelectList(db.TAIKHOANs.ToList(), "IdTK", "HoTen");

                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }
        // POST: Admin/BaiViet/Xoa
        public ActionResult Xoa(int id)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                BAIVIET bv = db.BAIVIETs.SingleOrDefault(n => n.IdBV == id);
                if (bv != null)
                {
                    db.BAIVIETs.Remove(bv);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

        }
        // GET: Admin/BaiViet/ChiTiet
        public ActionResult ChiTiet(int id = 0)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                BAIVIET bv = db.BAIVIETs.SingleOrDefault(n => n.IdBV == id);
                if (bv == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                ViewBag.IdTheLoai = new SelectList(db.THELOAIBAIVIETs.ToList(), "IdTheLoai", "TenTheLoai");
                ViewBag.IdTaiKhoan = new SelectList(db.TAIKHOANs.ToList(), "IdTK", "HoTen");
                return View(bv);
            }
        }
    }
}