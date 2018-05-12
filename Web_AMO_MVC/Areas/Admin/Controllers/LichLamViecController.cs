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
    public class LichLamViecController : Controller
    {
        // GET: Admin/LichLamViec
        [HttpGet]
        public ActionResult Index(string tukhoa, int? page)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                int itemPageSize = 20;
                int pageNumber = (page ?? 1);
                ViewBag.IdTaiKhoan = new SelectList(db.TAIKHOANs.ToList(), "IdTK", "HoTen");

                if (!String.IsNullOrEmpty(tukhoa))
                {
                    ViewBag.TuKhoa = tukhoa;
                    var lstResult = db.LICHLAMVIECs.Where(n => n.TieuDe.Contains(tukhoa)).OrderByDescending(n => n.NgayDang).ToPagedList(pageNumber, itemPageSize);
                    return View("Index", lstResult);
                }
                else
                {
                    var tatcalct = db.LICHLAMVIECs.ToList();
                    ViewBag.Count = tatcalct.Count;
                    var dsLichlamviec = db.LICHLAMVIECs.OrderByDescending(n => n.NgayDang).ToPagedList(pageNumber, itemPageSize);
                    return View(dsLichlamviec);
                }

            }
        }

        //POST: Admin/LichLamViec/Them
        [HttpPost]
        public ActionResult Them(LICHLAMVIEC llv)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                if (llv.NgayDang == null)
                {
                    llv.NgayDang = DateTime.Parse(DateTime.Now.ToString());
                }
                else
                {
                    llv.NgayDang = llv.NgayDang;
                }
                if (llv.NhacLich == null)
                {
                    llv.NhacLich = "Mọi người chú ý thực hiện đầy đủ!";
                }
                else
                {
                    llv.NhacLich = llv.NhacLich;
                }
                llv.LuotXem = 0;
                ViewBag.IdTaiKhoan = new SelectList(db.TAIKHOANs.ToList(), "IdTK", "HoTen");
                db.LICHLAMVIECs.Add(llv);
                db.SaveChanges();
                return RedirectToAction("Index", "LichLamViec");
            }
        }

        //POST: Admin/LichLamViec/Sua
        [HttpPost]
        public ActionResult Sua(LICHLAMVIEC llv)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                LICHLAMVIEC lichlamviec = db.LICHLAMVIECs.SingleOrDefault(n => n.IdLLV == llv.IdLLV);
                if (lichlamviec != null)
                {
                    lichlamviec.TieuDe = llv.TieuDe;
                    lichlamviec.LinkNoiDung = llv.LinkNoiDung;
                    if (llv.NgayDang == null)
                    {
                        lichlamviec.NgayDang = lichlamviec.NgayDang;
                    }
                    else
                    {
                        lichlamviec.NgayDang = llv.NgayDang;
                    }
                    lichlamviec.NhacLich = llv.NhacLich;
                    lichlamviec.IdTaiKhoan = llv.IdTaiKhoan;
                    ViewBag.IdTaiKhoan = new SelectList(db.TAIKHOANs.ToList(), "IdTK", "HoTen", lichlamviec.IdTaiKhoan);
                    db.SaveChanges();
                }
                return RedirectToAction("Index", "LichLamViec");
            }
        }

        //POST: Admin/LichLamViec/Xoa
        public ActionResult Xoa(int id)
        {
            using (Web_KTCKEntities db = new Web_KTCKEntities())
            {
                LICHLAMVIEC llv = db.LICHLAMVIECs.SingleOrDefault(n => n.IdLLV == id);
                if (llv != null)
                {
                    db.LICHLAMVIECs.Remove(llv);
                    db.SaveChanges();
                }
                return RedirectToAction("Index", "LichLamViec");
            }
        }

    }
}