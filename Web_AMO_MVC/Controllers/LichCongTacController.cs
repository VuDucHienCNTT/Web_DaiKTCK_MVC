using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_AMO_MVC.Models;
using PagedList.Mvc;
using PagedList;

namespace Web_AMO_MVC.Controllers
{
    public class LichCongTacController : Controller
    {
        Web_KTCKEntities db = new Web_KTCKEntities();
        // GET: LichCongTac
        public ActionResult Index(int? page)
        {
            int itemPageSize = 20;
            int pageNumber = (page ?? 1);
            var tatcalct = db.LICHLAMVIECs.ToList();
            ViewBag.Count = tatcalct.Count;
            var dslichcongtac = db.LICHLAMVIECs.OrderByDescending(n => n.NgayDang).ToPagedList(pageNumber, itemPageSize);
            return View(dslichcongtac);
        }

        // POST: LichCongTac/Them
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Them(LICHLAMVIEC llv)
        {
            if (llv.NgayDang == null)
            {
                llv.NgayDang = DateTime.Parse(DateTime.Now.ToString());
            }
            else
            {
                llv.NgayDang = llv.NgayDang;
            }
            llv.LuotXem = 0;
            llv.NhacLich = "Mọi người chú ý thực hiện đầy đủ!";
            db.LICHLAMVIECs.Add(llv);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: LichCongTac/ChiTiet
        public ActionResult ChiTiet(int id = 0)
        {
            LICHLAMVIEC lichcongtac = db.LICHLAMVIECs.SingleOrDefault(n => n.IdLLV == id);
            lichcongtac.LuotXem = lichcongtac.LuotXem + 1;
            db.SaveChanges();
            if (lichcongtac == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(lichcongtac);
        }
    }
}