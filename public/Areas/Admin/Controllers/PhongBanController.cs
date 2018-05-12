using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_AMO_MVC.Models;
namespace Web_AMO_MVC.Areas.Admin.Controllers
{
    public class PhongBanController : Controller
    {
        // GET: Admin/PhongBan
        [HttpGet]
        public ActionResult Index()
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                List<PHONGBAN> dsPhongban = db.PHONGBANs.ToList();
                return View(dsPhongban);
            }
        }
        // POST: Admin/PhongBan
        [HttpPost]
        public ActionResult Index(string tukhoa)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                List<PHONGBAN> lstResult = db.PHONGBANs.Where(n => n.TenPhong.Contains(tukhoa)).ToList();
                return View("Index", lstResult);
            }
        }

        // POST: Admin/PhongBan/Them
        [HttpPost]
        public ActionResult Them(PHONGBAN pb)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                //Kiểm tra tên phòng có trùng k?
                var checkTenPhong = db.PHONGBANs.SingleOrDefault(x => x.TenPhong.Trim() == pb.TenPhong.Trim());
                // không trùng thì thêm mới
                if (checkTenPhong == null)
                {
                    db.PHONGBANs.Add(pb);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }
        // POST: Admin/PhongBan/Sua
        [HttpPost]
        public ActionResult Sua(PHONGBAN pb)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                PHONGBAN phongban = db.PHONGBANs.SingleOrDefault(n => n.IdPB == pb.IdPB);
                if (phongban != null)
                {
                    phongban.TenPhong = pb.TenPhong;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }
        // POST: Admin/PhongBan/Xoa
        public ActionResult Xoa(int id)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                try
                {
                    PHONGBAN pb = db.PHONGBANs.SingleOrDefault(n => n.IdPB == id);
                    db.PHONGBANs.Remove(pb);
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