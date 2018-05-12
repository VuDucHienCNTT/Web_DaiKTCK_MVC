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
    public class CanBo_NhanVienController : Controller
    {
        Web_KTCKEntities db = new Web_KTCKEntities();
        // GET: CanBoNhanVien
        public ActionResult Index(int? page)
        {
            int itemPageSize = 12;
            int pageNumber = (page ?? 1);
            var tatcacbnv = db.CANBOes.ToList();
            ViewBag.Count = tatcacbnv.Count;
            var dscanbo = db.CANBOes.OrderBy(n=>n.IdCB).ToPagedList(pageNumber, itemPageSize);
            return View(dscanbo);
        }
        // GET: CanBoNhanVien/ChiTiet
        public ActionResult ChiTiet(int id = 0)
        {
            var canbo = db.CANBOes.SingleOrDefault(n => n.IdCB == id);
            return View(canbo);
        }
    }
}