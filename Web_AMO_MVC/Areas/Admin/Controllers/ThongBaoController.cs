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
    public class ThongBaoController : Controller
    {
        // GET: Admin/ThongBao
        [HttpGet]
        public ActionResult Index(string sortOrder, string currentFilter, string tukhoa, int? page)
        {
            // Sort filter Mã
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                int itemPageSize = 20;
                int pageNumber = (page ?? 1);
                ViewBag.CurrentSort = sortOrder;
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
                ViewBag.IdTaiKhoan = new SelectList(db.TAIKHOANs.ToList(), "IdTK", "HoTen");

                if (!String.IsNullOrEmpty(tukhoa))
                {
                    page = 1;
                }
                else
                {
                    tukhoa = currentFilter;
                }

                ViewBag.TuKhoa = tukhoa;
                var tatcatb = db.THONGBAOs.ToList();

                if (!String.IsNullOrEmpty(tukhoa))
                {
                    ViewBag.TuKhoa = tukhoa;
                    tatcatb = tatcatb.Where(n => n.TieuDe.Contains(tukhoa) || n.NoiDung.Contains(tukhoa)).ToList();
                }
                switch (sortOrder)
                {
                    case "id_desc":
                        tatcatb = tatcatb.OrderBy(s => s.IdTB).ToList();
                        break;
                    default:
                        tatcatb = tatcatb.OrderByDescending(s => s.IdTB).ToList();
                        break;
                }
                ViewBag.Count = tatcatb.Count;
                var dsthongbao = tatcatb.ToPagedList(pageNumber, itemPageSize);
                return View(dsthongbao);
            }
        }

        //POST: Admin/ThongBao/Them
        public ActionResult Them(THONGBAO tb)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                if (tb.NgayDang == null)
                {
                    tb.NgayDang = DateTime.Parse(DateTime.Now.ToString());
                }
                else
                {
                    tb.NgayDang = tb.NgayDang;
                }
                ViewBag.IdTaiKhoan = new SelectList(db.TAIKHOANs.ToList(), "IdTK", "HoTen");
                db.THONGBAOs.Add(tb);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        //POST: Admin/ThongBao/Sua
        public ActionResult Sua(THONGBAO tb)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                THONGBAO thongbao = db.THONGBAOs.SingleOrDefault(n => n.IdTB == tb.IdTB);
                if (thongbao != null)
                {
                    thongbao.TieuDe = tb.TieuDe;
                    thongbao.NoiDung = tb.NoiDung;
                    if (tb.NgayDang == null)
                    {
                        thongbao.NgayDang = thongbao.NgayDang;
                    }
                    else
                    {
                        thongbao.NgayDang = tb.NgayDang;
                    }
                    thongbao.TrangThai = tb.TrangThai;
                    thongbao.IdTaiKhoan = tb.IdTaiKhoan;
                    ViewBag.IdTaiKhoan = new SelectList(db.TAIKHOANs.ToList(), "IdTK", "HoTen");
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }

        //POST: Admin/ThongBao/Xoa
        public ActionResult Xoa(int id)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                THONGBAO tb = db.THONGBAOs.SingleOrDefault(n => n.IdTB == id);
                if (tb != null)
                {
                    db.THONGBAOs.Remove(tb);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }
    }
}