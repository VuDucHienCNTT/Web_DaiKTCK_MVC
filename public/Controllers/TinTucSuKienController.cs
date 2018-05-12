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
    public class TinTucSuKienController : Controller
    {
        Web_KTCKEntities db = new Web_KTCKEntities();
        // GET: TinTucSuKien
        public ActionResult Index(int? page)
        {
            int itemPageSize = 12;
            int pageNumber = (page ?? 1);
            var tatcabv = db.BAIVIETs.Where(n => n.TrangThai == "Đã duyệt").ToList();
            ViewBag.Count = tatcabv.Count;
            var dsbaiviet = db.BAIVIETs.Where(n => n.TrangThai == "Đã duyệt").OrderByDescending(n => n.NgayDang).ToPagedList(pageNumber, itemPageSize);
            return View(dsbaiviet);
        }
    }
}