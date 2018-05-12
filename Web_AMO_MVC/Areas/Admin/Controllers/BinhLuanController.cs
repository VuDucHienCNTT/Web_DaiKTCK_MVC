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
    public class BinhLuanController : Controller
    {
        // GET: Admin/BinhLuan
        public ActionResult Index(int? page)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                int itemPageSize = 30;
                int pageNumber = (page ?? 1);
                var tatcabl = db.BINHLUANs.ToList();
                ViewBag.Count = tatcabl.Count;
                var dsbinhluan = db.BINHLUANs.OrderByDescending(n => n.NgayBinhLuan).ToPagedList(pageNumber, itemPageSize);
                ViewBag.IdTaiKhoan = new SelectList(db.TAIKHOANs.ToList(), "IdTK", "HoTen");
                ViewBag.IdBaiViet = new SelectList(db.BAIVIETs.ToList(), "IdBV", "TieuDe");
                return View(dsbinhluan);
            }
        }
        // POST: Admin/BinhLuan/Sua
        [HttpPost]
        public ActionResult Sua(BINHLUAN bl)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                BINHLUAN binhluan = db.BINHLUANs.SingleOrDefault(n => n.IdBL == bl.IdBL);
                if (binhluan != null)
                {
                    binhluan.TrangThai = bl.TrangThai;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }
        //POST: Admin/BinhLuan/Xoa
        public ActionResult Xoa(int id)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                BINHLUAN bl = db.BINHLUANs.SingleOrDefault(n => n.IdBL == id);
                if (bl != null)
                {
                    db.BINHLUANs.Remove(bl);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }
    }
}