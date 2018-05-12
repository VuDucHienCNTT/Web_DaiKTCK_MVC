using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_AMO_MVC.Models;
namespace Web_AMO_MVC.Areas.Admin.Controllers
{
    public class TheLoaiBaiVietController : Controller
    {
        // GET: Admin/TheLoaiBaiViet
        [HttpGet]
        public ActionResult Index()
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                List<THELOAIBAIVIET> dstheloaibv = db.THELOAIBAIVIETs.ToList();
                return View(dstheloaibv);
            }
        }

        //POST: Admin/TheLoaiBaiViet/
        [HttpPost]
        public ActionResult Index(string tukhoa)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                List<THELOAIBAIVIET> lstResult = db.THELOAIBAIVIETs.Where(n => n.TenTheLoai.Contains(tukhoa)).ToList();
                return View("Index", lstResult);
            }
        }

        //POST: Admin/TheLoaiBaiViet/Them
        public ActionResult Them(THELOAIBAIVIET tlbv)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                var checkTheLoai = db.THELOAIBAIVIETs.SingleOrDefault(n => n.TenTheLoai.Trim() == tlbv.TenTheLoai.Trim());
                if (checkTheLoai == null)
                {
                    db.THELOAIBAIVIETs.Add(tlbv);
                    db.SaveChanges();

                }
                return RedirectToAction("Index");
            }
        }
        //POST: Admin/TheLoaiBaiViet/Sua
        public ActionResult Sua(THELOAIBAIVIET tlbv)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                THELOAIBAIVIET theloaibv = db.THELOAIBAIVIETs.SingleOrDefault(n => n.IdTheLoai == tlbv.IdTheLoai);
                if (theloaibv != null)
                {
                    theloaibv.TenTheLoai = tlbv.TenTheLoai;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }

        //POST: Admin/TheLoaiBaiViet/Xoa
        public ActionResult Xoa(int id)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                try
                {
                    THELOAIBAIVIET theloaibv = db.THELOAIBAIVIETs.SingleOrDefault(n => n.IdTheLoai == id);
                    db.THELOAIBAIVIETs.Remove(theloaibv);
                    db.SaveChanges();

                }
                catch
                {
                }
                return RedirectToAction("Index");
            }

        }
    }
}