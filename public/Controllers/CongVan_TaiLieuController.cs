using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_AMO_MVC.Models;
using PagedList;
using PagedList.Mvc;
namespace Web_AMO_MVC.Controllers
{
    public class CongVan_TaiLieuController : Controller
    {
        Web_KTCKEntities db = new Web_KTCKEntities();
        // GET: CongVan_TaiLieu
        public ActionResult Index(int? page)
        {
            int itemPageSize = 24;
            int pageNumber = (page ?? 1);
            var tatcacvtl = db.TAILIEUx.ToList();
            ViewBag.Count = tatcacvtl.Count;
            var dstailieu = db.TAILIEUx.OrderByDescending(n => n.NgayDang).ToPagedList(pageNumber, itemPageSize);
            return View(dstailieu);
        }

        // GET: CongVan_TaiLieu/ChiTiet
        public ActionResult ChiTiet(int id = 0)
        {
            TAILIEU dstailieu = db.TAILIEUx.SingleOrDefault(n => n.IdTL == id);
            dstailieu.LuotXem = dstailieu.LuotXem + 1;
            db.SaveChanges();
            if (dstailieu == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(dstailieu);
        }

        // POST: CongVan_TaiLieu/Them
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Them(TAILIEU tl)
        {
            if (tl.NgayDang == null)
            {
                tl.NgayDang = DateTime.Parse(DateTime.Now.ToString());
            }
            else
            {
                tl.NgayDang = tl.NgayDang;
            }
            tl.LuotXem = 0;
            db.TAILIEUx.Add(tl);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}