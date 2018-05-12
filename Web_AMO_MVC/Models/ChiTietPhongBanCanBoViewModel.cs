using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_AMO_MVC.Models
{
    public class ChiTietPhongBanCanBoViewModel
    {
        public List<PHONGBAN> dsphongban { get; set; }
        public List<CANBO> dscanbo { get; set; }

        public ChiTietPhongBanCanBoViewModel()
        {

        }
        public ChiTietPhongBanCanBoViewModel(List<PHONGBAN> danhsachphongban, List<CANBO> danhsachcanbo)
        {
            this.dsphongban = danhsachphongban;
            this.dscanbo = danhsachcanbo;
        }

    }
}