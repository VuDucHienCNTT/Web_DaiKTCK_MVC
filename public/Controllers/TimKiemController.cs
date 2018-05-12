using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_AMO_MVC.Models;

namespace Web_AMO_MVC.Controllers
{
    public class TimKiemController : Controller
    {
        Web_KTCKEntities db = new Web_KTCKEntities();
        // GET: TimKiem
        [HttpGet]
        public ActionResult Index(string tukhoa)
        {
            ViewBag.Tukhoa = tukhoa;
            if (tukhoa == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View();
        }

        // GET: TimKiem/BaiViet
        [HttpGet]
        [ValidateInput(false)]
        public ActionResult BaiViet(string tukhoa)
        {
            ViewBag.Tukhoa = tukhoa;
            List<BAIVIET> lstKetqua = db.BAIVIETs.Where(n => (n.TieuDe.Contains(tukhoa) && n.TrangThai == "Đã duyệt") || (n.TomTat.Contains(tukhoa) && n.TrangThai == "Đã duyệt")).ToList();
            return View(lstKetqua);
        }
        // GET: TimKiem/CanBo
        [HttpGet]
        [ValidateInput(false)]
        public ActionResult CanBo(string tukhoa)
        {
            ViewBag.Tukhoa = tukhoa;
            List<CANBO> lstKetqua = db.CANBOes.Where(n => n.HoTen.Contains(tukhoa) || n.BangCap.Contains(tukhoa) || n.ChucVu.Contains(tukhoa) || n.QueQuan.Contains(tukhoa)).ToList();
            return View(lstKetqua);
        }
        // GET: TimKiem/CongVan
        [HttpGet]
        [ValidateInput(false)]
        public ActionResult CongVan(string tukhoa)
        {
            ViewBag.Tukhoa = tukhoa;
            List<TAILIEU> lstKetqua = db.TAILIEUx.Where(n => n.TieuDe.Contains(tukhoa)).ToList();
            return View(lstKetqua);
        }
        // GET: TimKiem/LichCongTac
        [HttpGet]
        [ValidateInput(false)]
        public ActionResult LichCongTac(string tukhoa)
        {
            ViewBag.Tukhoa = tukhoa;
            List<LICHLAMVIEC> lstKetqua = db.LICHLAMVIECs.Where(n => n.TieuDe.Contains(tukhoa)).ToList();
            return View(lstKetqua);
        }
    }
}