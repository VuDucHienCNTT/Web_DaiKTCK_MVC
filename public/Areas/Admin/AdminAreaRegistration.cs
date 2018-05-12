using System.Web.Mvc;

namespace Web_AMO_MVC.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
            "dangnhap",
            "Admin/dang-nhap",
            new { controller = "TaiKhoan", action = "DangNhap" },
            new string[] { "Web_AMO_MVC.Areas.Admin.Controllers" }
            );

            context.MapRoute(
            "dangky",
            "Admin/dang-ky",
            new { controller = "TaiKhoan", action = "DangKy" },
            new string[] { "Web_AMO_MVC.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] {"Web_AMO_MVC.Areas.Admin.Controllers"}
            );

        }
    }
}