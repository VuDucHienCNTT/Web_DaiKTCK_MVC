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
    public class CongVanTaiLieuController : Controller
    {
        // GET: Admin/CongVanTaiLieu
        [HttpGet]
        public ActionResult Index(string tukhoa, int? page)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                int itemPageSize = 20;
                int pageNumber = (page ?? 1);
                ViewBag.IdTaiKhoan = new SelectList(db.TAIKHOANs.ToList(), "IdTK", "HoTen");
                if (!String.IsNullOrEmpty(tukhoa))
                {
                    ViewBag.TuKhoa = tukhoa;
                    var lstResult = db.TAILIEUx.Where(n => n.TieuDe.Contains(tukhoa)).OrderByDescending(n => n.NgayDang).ToPagedList(pageNumber, itemPageSize);
                    return View("Index", lstResult);
                }
                else
                {
                    var tatcacvtl = db.TAILIEUx.ToList();
                    ViewBag.Count = tatcacvtl.Count;
                    var dstailieu = db.TAILIEUx.OrderByDescending(n => n.NgayDang).ToPagedList(pageNumber, itemPageSize);
                    return View(dstailieu);
                }

            }
        }


        //POST: Admin/CongVanTaiLieu/Them
        public ActionResult Them(TAILIEU tl)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                if (tl.NgayDang == null)
                {
                    tl.NgayDang = DateTime.Parse(DateTime.Now.ToString());
                }
                else
                {
                    tl.NgayDang = tl.NgayDang;
                }
                db.TAILIEUx.Add(tl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        //POST: Admin/CongVanTaiLieu/Sua
        public ActionResult Sua(TAILIEU tl)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                TAILIEU tailieu = db.TAILIEUx.SingleOrDefault(n => n.IdTL == tl.IdTL);
                if (tailieu != null)
                {
                    tailieu.TieuDe = tl.TieuDe;
                    tailieu.LinkNoiDung = tl.LinkNoiDung;
                    if (tl.NgayDang == null)
                    {
                        tailieu.NgayDang = tailieu.NgayDang;
                    }
                    else
                    {
                        tailieu.NgayDang = tl.NgayDang;
                    }
                    tailieu.IdTaiKhoan = tl.IdTaiKhoan;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }

        //POST: Admin/CongVanTaiLieu/Xoa
        public ActionResult Xoa(int id)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                TAILIEU tl = db.TAILIEUx.SingleOrDefault(n => n.IdTL == id);
                if (tl != null)
                {
                    db.TAILIEUx.Remove(tl);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }
    }
}