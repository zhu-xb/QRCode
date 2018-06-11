using Gma.QrCodeNet;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using Gma.QrCodeNet.Encoding.Windows.WPF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace QRCode.Controllers
{
    public class HomeController : Controller
    {
        static Random r = new Random();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult BuildQRCode(PXInfo info)
        {
            ResponseResult ret = new ResponseResult();
            try
            {
                QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
                QrCode qrCode = qrEncoder.Encode(info.ToString());
                ret.success = qrEncoder.TryEncode(info.ToString(), out qrCode);
                if (ret.success)
                {
                    string filename = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + r.Next(0, 10000).ToString("D5") + ".png";
                    GraphicsRenderer render = new GraphicsRenderer(new FixedModuleSize(5, QuietZoneModules.Two), Brushes.Black, Brushes.White);
                    using (FileStream stream = new FileStream(Server.MapPath("~/qrimages/") + filename, FileMode.Create))
                    {
                        render.WriteToStream(qrCode.Matrix, ImageFormat.Png, stream);
                    }

                    ret.imagename = filename;
                }
                else
                {
                    ret.errorMsg = "二维码生成失败";
                }
            }
            catch (Exception ex)
            {
                ret.success = false;
                ret.errorMsg = ex.Message;
            }
            return Json(ret);
        }
    }

    public class ResponseResult
    {
        public bool success { get; set; }
        public string errorMsg { get; set; }
        public string imagename { get; set; }
    }

    public class PXInfo
    {
        public string name1 { get; set; }
        public string name2 { get; set; }
        public string name3 { get; set; }
        public string name4 { get; set; }
        public string name5 { get; set; }
        public string name6 { get; set; }
        public string name7 { get; set; }
        public string name8 { get; set; }

        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3},{4},{5},{6},{7}"
                , name1 ?? "", name2 ?? "", name3 ?? ""
                , (name4 == null ? DateTime.Now.ToString("yyyyMMdd") : Convert.ToDateTime(name4).ToString("yyyyMMdd"))
                , (name5 == null ? DateTime.Now.ToString("yyyyMMdd") : Convert.ToDateTime(name5).ToString("yyyyMMdd"))
                , name6 ?? "", name7 ?? "", name8 ?? "");
        }
    }
}