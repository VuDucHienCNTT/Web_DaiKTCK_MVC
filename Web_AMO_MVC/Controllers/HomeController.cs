using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_AMO_MVC.Models;
using PagedList;
using PagedList.Mvc;
using Web_AMO_MVC.ReportTinTuc;
using DevExpress.Web.Mvc;

namespace Web_AMO_MVC.Controllers
{
    public class HomeController : Controller
    {
        Web_KTCKEntities db = new Web_KTCKEntities();
        readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // GET: Home
        public ActionResult Index()
        {
            var theloai = db.THELOAIBAIVIETs.OrderBy(n => n.IdTheLoai);
            return View(theloai);
        }

        [HttpPost]
        public ActionResult XuatTinTuc()
        {
            logger.Info("Xuất tin tức thành công!");
            TempData["MaTT"] = Request.Form["txtMaTT"].ToString();         
            return View();
        }

        public ActionResult DocumentViewerPartial()
        {
            TempData.Keep("MaTT");
            reportTinTuccs report = new reportTinTuccs(int.Parse(TempData["MaTT"].ToString()));         
            return PartialView("_DocumentViewerPartial", report);
        }

        public ActionResult DocumentViewerPartialExport()
        {
            TempData.Keep("MaTT");
            reportTinTuccs report = new reportTinTuccs(int.Parse(TempData["MaTT"].ToString()));
            return DocumentViewerExtension.ExportTo(report, Request);
        }

        public PartialViewResult Slide()
        {
            var slidebv = db.BAIVIETs.Where(n => n.TrangThai == "Đã duyệt" && n.Slide == "Hiển thị").Take(3).OrderByDescending(n => n.NgayDang).ToList();
            return PartialView(slidebv);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemBinhLuan(BINHLUAN bl)
        {
            try
            {
                bl.NgayBinhLuan = DateTime.Parse(DateTime.Now.ToString());
                bl.TrangThai = "Hiển thị";
                bl.NoiDung = bl.NoiDung;
                bl.UrlBaiViet = bl.UrlBaiViet;
                db.BINHLUANs.Add(bl);
                db.SaveChanges();
                return Redirect(Request.UrlReferrer.ToString());
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public PartialViewResult HienThiBinhLuan(int id = 0)
        {
            var dsbinhluan = db.BINHLUANs.Where(n => n.IdBaiViet == id && n.TrangThai == "Hiển thị").ToList();
            return PartialView(dsbinhluan);
        }

        public PartialViewResult TinMoiNhat()
        {
            var dsbaiviet = db.BAIVIETs.Where(n => n.TrangThai == "Đã duyệt").ToList();
            return PartialView(dsbaiviet);
        }
        public PartialViewResult ThongBao()
        {
            List<THONGBAO> dsthongbao = db.THONGBAOs.Where(n => n.TrangThai == "Có hiệu lực").OrderByDescending(n=>n.NgayDang).ToList();
            return PartialView(dsthongbao);

        }
        public ActionResult TheLoai(int? page, int id = 0)
        {
            int itemPageSize = 12;
            int pageNumber = (page ?? 1);
            var dstlbaiviet1 = db.BAIVIETs.Where(n => n.IdTheLoai == id && n.TrangThai == "Đã duyệt").ToList();
            ViewBag.Count = dstlbaiviet1.Count;
            var dstlbaiviet = db.BAIVIETs.Where(n => n.IdTheLoai == id && n.TrangThai == "Đã duyệt").OrderByDescending(n => n.NgayDang).ToPagedList(pageNumber, itemPageSize);
            return View(dstlbaiviet);
        }

        public PartialViewResult BaiVietCungTheLoai(int id = 0)
        {
            var idtl = db.BAIVIETs.Find(id).IdTheLoai;
            var dsbaicungtheloai = db.BAIVIETs.Where(n => n.IdBV != id && n.IdTheLoai == idtl && n.TrangThai == "Đã duyệt").OrderByDescending(n => n.NgayDang).Take(10).ToList();
            return PartialView(dsbaicungtheloai);
        }

        public ActionResult ChiTiet(int id = 0)
        {
            BAIVIET baiviet = db.BAIVIETs.SingleOrDefault(n => n.IdBV == id);
            baiviet.LuotXem = baiviet.LuotXem + 1;
            db.SaveChanges();
            if (baiviet == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(baiviet);
        }
    }
}