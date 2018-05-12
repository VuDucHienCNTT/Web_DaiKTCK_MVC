using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using Web_AMO_MVC.Models;
using System.Linq;

namespace Web_AMO_MVC.ReportTinTuc
{
    public partial class reportTinTuccs : DevExpress.XtraReports.UI.XtraReport
    {
        //string MaTinTuc;
        //public reportTinTuccs(string MaTT)
        //{
        //    InitializeComponent();
        //    MaTinTuc = MaTT;
        //    LoadTT(int.Parse(MaTT));
        //}
        public reportTinTuccs(int id)
        {
            InitializeComponent();
            Web_KTCKEntities db = new Web_KTCKEntities();
            var baiviet = db.BAIVIETs.Where(n => n.IdBV == id).ToList();
            DetailReport.DataSource = baiviet;
            xrLabel1.DataBindings.Add("Text", DetailReport.DataSource, "TieuDe");
            xrLabel2.DataBindings.Add("Text", DetailReport.DataSource, "TomTat");
            xrRichText1.DataBindings.Add("Html", DetailReport.DataSource, "NoiDung");
        }

        private void reportTinTuccs_AfterPrint(object sender, EventArgs e)
        {
            PrintingSystem.Document.Name = "Tintuc-" + DateTime.Now.ToString("dd-MM-yyyy-hh:mm");
        }
        //private void LoadTT(int MaTT)
        //{
        //    //SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["Web_KTCKEntities"].ConnectionString);
        //    SqlConnection conn = new SqlConnection();
        //    conn.ConnectionString = @"Data Source=DESKTOP-O3LN6DI;Initial Catalog=Web_KTCK;Integrated Security=True;";
        //    conn.Open();
        //    SqlCommand cmd = new SqlCommand("RP_BaiViet", conn);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@IDBaiViet", MaTT);
        //    SqlDataAdapter da = new SqlDataAdapter();
        //    da.SelectCommand = cmd;
        //    System.Data.DataSet dt = new DataSet();
        //    da.Fill(dt);
        //    conn.Close();

        //    DetailReport.DataSource = dt;
        //    xrLabel1.DataBindings.Add("Text", DetailReport.DataSource, "TieuDe");
        //}
    }
}
