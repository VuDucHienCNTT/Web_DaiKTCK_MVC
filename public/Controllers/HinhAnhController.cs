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
    public class HinhAnhController : Controller
    {
        Web_KTCKEntities db = new Web_KTCKEntities();
        // GET: HinhAnh
        public ActionResult Index()
        {
            var album = db.ALBUMs;
            return View(album);
        }

        // GET: HinhAnh/ListHinhAnh
        public ActionResult ListHinhAnh(int? page, int id = 0)
        {
            int itemPageSize = 36;
            int pageNumber = (page ?? 1);
            var tatcahinhanh = db.HINHANHs.Where(n => n.IdAlbum == id).ToList();
            ViewBag.Count = tatcahinhanh.Count;
            var dshinhanh = db.HINHANHs.Where(n => n.IdAlbum == id).OrderByDescending(n => n.NgayDang).ToPagedList(pageNumber, itemPageSize);
            return View(dshinhanh);
        }
    }
}