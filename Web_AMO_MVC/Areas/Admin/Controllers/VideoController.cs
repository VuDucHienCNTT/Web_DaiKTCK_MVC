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
    public class VideoController : Controller
    {
        // GET: Admin/Video
        [HttpGet]
        public ActionResult Index(int? page)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                int itemPageSize = 20;
                int pageNumber = (page ?? 1);
                var tatcavideo = db.VIDEOs.ToList();
                ViewBag.Count = tatcavideo.Count;
                var dsvideo = db.VIDEOs.OrderByDescending(n => n.IdVideo).ToPagedList(pageNumber, itemPageSize);
                ViewBag.IdAlbum = new SelectList(db.ALBUMs.ToList(), "IdAlbum", "TenAlbum");
                ViewBag.IdTaiKhoan = new SelectList(db.TAIKHOANs.ToList(), "IdTK", "HoTen");
                return View(dsvideo);
            }
        }
        // POST: Admin/Video
        [HttpPost]
        public ActionResult Index(string tukhoa, int? page)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                int itemPageSize = 20;
                int pageNumber = (page ?? 1);
                var lstResult = db.VIDEOs.Where(n => n.TenVideo.Contains(tukhoa)).OrderByDescending(n => n.NgayDang).ToPagedList(pageNumber, itemPageSize);
                ViewBag.IdAlbum = new SelectList(db.ALBUMs.ToList(), "IdAlbum", "TenAlbum");
                ViewBag.IdTaiKhoan = new SelectList(db.TAIKHOANs.ToList(), "IdTK", "HoTen");
                return View("Index", lstResult);
            }
        }
        // POST: Admin/Video/Them
        public ActionResult Them(VIDEO vdo)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                if (vdo.NgayDang == null)
                {
                    vdo.NgayDang = DateTime.Parse(DateTime.Now.ToString());
                }
                else
                {
                    vdo.NgayDang = vdo.NgayDang;
                }
                db.VIDEOs.Add(vdo);
                ViewBag.IdAlbum = new SelectList(db.ALBUMs.ToList(), "IdAlbum", "TenAlbum");
                ViewBag.IdTaiKhoan = new SelectList(db.TAIKHOANs.ToList(), "IdTK", "HoTen");
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        // POST: Admin/Video/Sua
        public ActionResult Sua(VIDEO vdo)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                VIDEO video = db.VIDEOs.SingleOrDefault(n => n.IdVideo == vdo.IdVideo);
                if (video != null)
                {
                    video.TenVideo = vdo.TenVideo;
                    video.LinkVideo = vdo.LinkVideo;
                    if (vdo.NgayDang == null)
                    {
                        video.NgayDang = video.NgayDang;
                    }
                    else
                    {
                        video.NgayDang = vdo.NgayDang;
                    }
                    video.IdAlbum = vdo.IdAlbum;
                    ViewBag.IdAlbum = new SelectList(db.ALBUMs.ToList(), "IdAlbum", "TenAlbum", video.IdAlbum);
                    ViewBag.IdTaiKhoan = new SelectList(db.TAIKHOANs.ToList(), "IdTK", "HoTen");
                    video.IdTaiKhoan = vdo.IdTaiKhoan;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }
        // POST: Admin/Video/Xoa
        public ActionResult Xoa(int id)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                VIDEO vdo = db.VIDEOs.SingleOrDefault(n => n.IdVideo == id);
                if (vdo != null)
                {
                    db.VIDEOs.Remove(vdo);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }
    }
}