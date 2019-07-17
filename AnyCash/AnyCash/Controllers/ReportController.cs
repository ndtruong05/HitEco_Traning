using AnyCash.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AnyCash.Controllers
{
    public class ReportController : Controller
    {
        public ActionResult Index(string LoaiHinh = "", string fromDate = "", string toDate = "")
        {
            ViewBag.TienDauNgay = 10000000;
            ViewBag.Thu = 0;
            ViewBag.Chi = 0;
            ViewBag.TongCong = 0;
            ViewBag.TienConLai = 10000000;
            return View();
        }

        public ActionResult Revenue()
        {
            return View();
        }

        public PartialViewResult _Chi_tiet_giao_dich(string fromDate = "", string toDate = "", string LoaiHinh = "", string NgGD = "")
        {
            List<Chi_tiet_giao_dich> obj = new List<Chi_tiet_giao_dich>();
            DateTime ngGD = DateTime.ParseExact("07-07-2019", "dd-MM-yyyy",
                                       System.Globalization.CultureInfo.InvariantCulture);
            obj.Add(new Chi_tiet_giao_dich()
            {
                LoaiHinh = "Bát họ",
                MaHD = "BH-1",
                NgGD = "AL",
                KhachHang = "HuyNguyen",
                NgayGD = ngGD,
                DienGiai = "na",
                Thu = 1000,
                Chi = 1000,
                GhiChu = "na"
            });


            List<Chi_tiet_giao_dich> BanGhi = new List<Chi_tiet_giao_dich>();

                DateTime from_date = DateTime.ParseExact(fromDate, "dd-MM-yyyy",
                                           System.Globalization.CultureInfo.InvariantCulture);


                DateTime to_date = DateTime.ParseExact(toDate, "dd-MM-yyyy",
                                           System.Globalization.CultureInfo.InvariantCulture);
            

            var result = from item in obj
                         where item.NgayGD >= from_date && item.NgayGD <= to_date
                         where item.LoaiHinh == LoaiHinh
                         where item.NgGD == NgGD
                         select item;

            foreach(var record in result)
            {
                BanGhi.Add(record);
            }


            //if(fromDate != "")
            //{
            //    DateTime from_date = DateTime.ParseExact(fromDate, "dd-MM-yyyy",
            //                           System.Globalization.CultureInfo.InvariantCulture);
            //    List<Chi_tiet_giao_dich> frd = obj.Where(x => x.NgayGD >= from_date).ToList();
            //    foreach (var item in frd)
            //    {
            //        result.Add(item);
            //    }
            //}

            //else if(toDate != "")
            //{
            //    DateTime to_date = DateTime.ParseExact(toDate, "dd-MM-yyyy",
            //                           System.Globalization.CultureInfo.InvariantCulture);
            //    List<Chi_tiet_giao_dich> td = obj.Where(x => x.NgayGD <= to_date).ToList();
            //    foreach (var item in td)
            //    {
            //        result.Add(item);
            //    }
            //}

            //else if(LoaiHinh != "")
            //{
            //    List<Chi_tiet_giao_dich> lh = obj.Where(x => x.LoaiHinh == LoaiHinh).ToList();
            //    foreach(var item in lh){
            //        result.Add(item);
            //    }
            //}

            //else if (NgGD != "")
            //{
            //    List<Chi_tiet_giao_dich> nv = obj.Where(x => x.NgGD == NgGD).ToList();
            //    foreach (var item in nv)
            //    {
            //        result.Add(item);
            //    }
            //}

            return PartialView(BanGhi);
        }
    }
}