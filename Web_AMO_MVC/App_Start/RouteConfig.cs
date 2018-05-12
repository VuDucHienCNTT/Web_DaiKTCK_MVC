using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web_AMO_MVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                    name: "theloai",
                    url: "the-loai/{id}/{theloai}",
                    defaults: new { controller = "Home", action = "TheLoai" },
                    namespaces: new string[] { "Web_AMO_MVC.Controllers" }
              );

            routes.MapRoute(
            name: "gioithieu",
            url: "gioi-thieu",
            defaults: new { controller = "GioiThieu", action = "Index" },
            namespaces: new string[] { "Web_AMO_MVC.Controllers" }
            );

            routes.MapRoute(
            name: "canbonv",
            url: "can-bo-nhan-vien",
            defaults: new { controller = "CanBo_NhanVien", action = "Index" },
            namespaces: new string[] { "Web_AMO_MVC.Controllers" }
            );

            routes.MapRoute(
      name: "chitietcanbonv",
      url: "chi-tiet/can-bo-nhan-vien/{id}/{tencanbo}",
      defaults: new { controller = "CanBo_NhanVien", action = "ChiTiet" },
      namespaces: new string[] { "Web_AMO_MVC.Controllers" }
      );

            routes.MapRoute(
         name: "congvantl",
         url: "cong-van-tai-lieu",
         defaults: new { controller = "CongVan_TaiLieu", action = "Index" },
         namespaces: new string[] { "Web_AMO_MVC.Controllers" }
         );

            routes.MapRoute(
            name: "chitietcongvantl",
            url: "chi-tiet/cong-van-tai-lieu/{id}/{tieude}",
            defaults: new { controller = "CongVan_TaiLieu", action = "ChiTiet" },
            namespaces: new string[] { "Web_AMO_MVC.Controllers" }
            );

            routes.MapRoute(
            name: "lichcongtac",
            url: "lich-cong-tac",
            defaults: new { controller = "LichCongTac", action = "Index" },
            namespaces: new string[] { "Web_AMO_MVC.Controllers" }
           );

            routes.MapRoute(
      name: "chitietlichcongtac",
      url: "chi-tiet/lich-cong-tac/{id}/{tieude}",
      defaults: new { controller = "LichCongTac", action = "ChiTiet" },
      namespaces: new string[] { "Web_AMO_MVC.Controllers" }
     );

            routes.MapRoute(
          name: "tintucsukien",
          url: "tin-tuc-su-kien",
          defaults: new { controller = "TinTucSuKien", action = "Index" },
          namespaces: new string[] { "Web_AMO_MVC.Controllers" }
         );

            routes.MapRoute(
          name: "hinhanh",
          url: "hinh-anh",
          defaults: new { controller = "HinhAnh", action = "Index" },
          namespaces: new string[] { "Web_AMO_MVC.Controllers" }
         );

            routes.MapRoute(
     name: "dshinhanh",
     url: "danh-sach-hinh-anh/{id}/{tenalbum}",
     defaults: new { controller = "HinhAnh", action = "ListHinhAnh" },
     namespaces: new string[] { "Web_AMO_MVC.Controllers" }
    );

            routes.MapRoute(
            name: "video",
            url: "video",
            defaults: new { controller = "Video", action = "Index" },
            namespaces: new string[] { "Web_AMO_MVC.Controllers" }
            );

            routes.MapRoute(
            name: "dsvideo",
            url: "danh-sach-video/{id}/{tenalbum}",
            defaults: new { controller = "Video", action = "ListVideo" },
            namespaces: new string[] { "Web_AMO_MVC.Controllers" }
           );

            routes.MapRoute(
            name: "chitietvideo",
            url: "chi-tiet/video/{id}/{tenvideo}",
            defaults: new { controller = "Video", action = "ChiTiet" },
            namespaces: new string[] { "Web_AMO_MVC.Controllers" }
           );

            routes.MapRoute(
                    name: "chitiettintuc",
                    url: "chi-tiet/tin-tuc-su-kien/{id}/{tieude}",
                    defaults: new { controller = "Home", action = "ChiTiet" },
                    namespaces: new string[] { "Web_AMO_MVC.Controllers" }

             );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "Web_AMO_MVC.Controllers" }

            );
        }
    }
}
