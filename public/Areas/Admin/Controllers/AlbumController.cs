using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_AMO_MVC.Models;

namespace Web_AMO_MVC.Areas.Admin.Controllers
{
    public class AlbumController : Controller
    {

        // GET: Admin/Album
        public ActionResult Index(string tukhoa)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                if (!String.IsNullOrEmpty(tukhoa))
                {
                    List<ALBUM> lstResult = db.ALBUMs.Where(n => n.TenAlbum.Contains(tukhoa)).ToList();
                    return View("Index", lstResult);
                }
                else
                {
                    List<ALBUM> dsalbum = db.ALBUMs.ToList();
                    return View(dsalbum);
                }
            }
        }

        // POST: Admin/Album/Them
        public ActionResult Them(ALBUM al)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                var checkTenAlbum = db.ALBUMs.SingleOrDefault(n => n.TenAlbum == al.TenAlbum);
                if (checkTenAlbum == null)
                {
                    db.ALBUMs.Add(al);
                    db.SaveChanges();
                }
                else
                {

                }
                return RedirectToAction("Index");
            }

        }
        // POST: Admin/Album
        public ActionResult Sua(ALBUM al)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                ALBUM album = db.ALBUMs.SingleOrDefault(n => n.IdAlbum == al.IdAlbum);
                if (album != null)
                {
                    album.TenAlbum = al.TenAlbum;
                    album.AnhDaiDien = al.AnhDaiDien;
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
        // POST: Admin/Album
        public ActionResult Xoa(int id)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                try
                {
                    ALBUM al = db.ALBUMs.SingleOrDefault(n => n.IdAlbum == id);
                    db.ALBUMs.Remove(al);
                    db.SaveChanges();
                }
                catch
                {
                }
            }
            return RedirectToAction("Index");
        }
    }
}