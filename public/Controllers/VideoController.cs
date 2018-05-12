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
    public class VideoController : Controller
    {
        Web_KTCKEntities db = new Web_KTCKEntities();
        // GET: Video
        public ActionResult Index()
        {
            var album = db.ALBUMs;
            return View(album);
        }
        // GET: Video/ListVideo
        public ActionResult ListVideo(int? page, int id = 0)
        {
            int itemPageSize = 10;
            int pageNumber = (page ?? 1);
            var tatcavideo = db.VIDEOs.Where(n => n.IdAlbum == id).ToList();
            ViewBag.Count = tatcavideo.Count;
            var dsvideo = db.VIDEOs.Where(n => n.IdAlbum == id).OrderByDescending(n => n.NgayDang).ToPagedList(pageNumber, itemPageSize); ;
            return View(dsvideo);
        }
        // GET: Video/ChiTiet
        public ActionResult ChiTiet(int id = 0)
        {
            VIDEO video = db.VIDEOs.SingleOrDefault(n => n.IdVideo == id);
            if (video == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(video);
        }

        public PartialViewResult VideoTrongAlbum(int id)
        {
            var idalbum = db.VIDEOs.Find(id).IdAlbum;
            var videocungalbum = db.VIDEOs.Where(n => n.IdVideo != id && n.IdAlbum == idalbum).ToList();
            return PartialView(videocungalbum);
        }
    }
}