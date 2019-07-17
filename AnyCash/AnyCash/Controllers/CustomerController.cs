using AnyCash.Models;
using CORE.Models;
using CORE.Tables;
using CORE.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AnyCash.Controllers
{
    public class CustomerController : BaseController
    {
        public ActionResult Index()
        {
            List<CASH_SHOPS> list = new List<CASH_SHOPS>();
            try
            {
                int userId = ((USER_INFOR)Session[AppSession.AppSessionKeys.USER_INFO]).USER_ID;
                int count;
                list = CASH_SHOP_Service.GetByUser(userId, "", "", 1, short.MaxValue, out count);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View(list);
        }

        public PartialViewResult _List(int shop = 0, string keyText = "", string stt = "", int pageNumber = 1, int pageSize = 10)
        {
            ViewBag.pageNumber = pageNumber;
            ViewBag.pageSize = pageSize;
            int count = 0;
            List<CASH_CUSTOMERS2> list = new List<CASH_CUSTOMERS2>();
            try
            {
                int userId = ((USER_INFOR)Session[AppSession.AppSessionKeys.USER_INFO]).USER_ID;
                list = CASH_CUSTOMER_Service.GetByUser(userId, shop, keyText, stt, pageNumber, pageSize, out count);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            ViewBag.maxNumber = Math.Ceiling((double)count / pageSize);

            return PartialView(list);
        }

        public ActionResult Create(string id = "")
        {
            CASH_CUSTOMERS shop = CASH_CUSTOMER_Service.GetByKey(id);
            if (shop == null)
                shop = new CASH_CUSTOMERS();

            List<CASH_SHOPS> list = new List<CASH_SHOPS>();
            try
            {
                int userId = ((USER_INFOR)Session[AppSession.AppSessionKeys.USER_INFO]).USER_ID;
                int count;
                list = CASH_SHOP_Service.GetByUser(userId, "", "", 1, short.MaxValue, out count);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            ViewBag.ShopList = list;

            return View(shop);
        }

        public ActionResult FileUpload(string id = "")
        {
            CASH_CUSTOMERS shop = CASH_CUSTOMER_Service.GetByKey(id);
            if (shop == null)
                shop = new CASH_CUSTOMERS();

            List<CASH_SHOPS> list = new List<CASH_SHOPS>();
            try
            {
                int userId = ((USER_INFOR)Session[AppSession.AppSessionKeys.USER_INFO]).USER_ID;
                int count;
                list = CASH_SHOP_Service.GetByUser(userId, "", "", 1, short.MaxValue, out count);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            ViewBag.ShopList = list;

            return View(shop);
        }

        public PartialViewResult _DanhSachKH(string tenKH = "", string maKH = "", string tinhtrang = "Hoạt động")
        {
            List<Customer_DSKH> obj = new List<Customer_DSKH>();
            obj.Add(new Customer_DSKH
            {
                MaKH = "KH0001",
                KhachHang = "Vay lãi Lò Đúc",
                DiaChi = "Số 02 Ngõ 131 Lò Đúc",
                DienThoai = "02413650378",
                CMND = "",
                CuaHang = "Vay Lãi Lò Đúc",
                NgayTao = "05/07/2019",
                TinhTrang = "Hoạt động"
            });
            obj.Add(new Customer_DSKH
            {
                MaKH = "KH0002",
                KhachHang = "Bất Động Sản",
                DiaChi = "Tỉnh Đồng Nai",
                DienThoai = "0939098699",
                CMND = "",
                CuaHang = "Bất Động Sản Đồng Nai",
                NgayTao = "04/07/2019",
                TinhTrang = "Hoạt động"
            });
            obj.Add(new Customer_DSKH
            {
                MaKH = "KH0003",
                KhachHang = "Bóc Bác Họ",
                DiaChi = "Số 05 - Ngõ 169 - Mai Dịch",
                DienThoai = "0243561718",
                CMND = "",
                CuaHang = "Bốc Bác Họ Cầu Giấy",
                NgayTao = "05/07/2019",
                TinhTrang = "Đã khóa"
            });
            List<Customer_DSKH> result = new List<Customer_DSKH>();
            if ((tinhtrang.Equals("Xem tất cả")) || (tinhtrang.Equals("Hoạt động")) || (tinhtrang.Equals("Đã khóa")))
            {
                if (tinhtrang.Equals("Xem tất cả"))
                {
                    var result1 = obj.Where(x => (x.TinhTrang.Equals("Hoạt động"))).ToList();
                    var result2 = obj.Where(x => (x.TinhTrang.Equals("Đã khóa"))).ToList();
                    //result = result1.Concat(result2).ToList();

                    List<Customer_DSKH> allCustomer = new List<Customer_DSKH>(result1.Count + result2.Count);

                    result1.ForEach(p => allCustomer.Add(p));
                    result2.ForEach(p => allCustomer.Add(p));
                    result = allCustomer;
                }
                else
                {
                    result = obj.Where(x => (x.TinhTrang == tinhtrang)).ToList();
                }
            }
            else if (tenKH != null)
            {
                if (tenKH.Equals("Tất cả khách hàng"))
                {
                    List<Customer_DSKH> allCustomer = new List<Customer_DSKH>(obj.Count);
                    for (int i = 0; i < obj.Count; i++)
                    {
                        var result1 = obj.Where(x => (x.KhachHang.Equals(obj[i].KhachHang))).ToList();

                        result1.ForEach(p => allCustomer.Add(p));

                    }
                    result = allCustomer;
                }
                else
                {
                    result = obj.Where(x => (x.KhachHang == tenKH)).ToList();
                }

            }
            else
            {
                result = obj.Where(x => (x.MaKH == maKH)).ToList();
            }
            return PartialView(result);
        }
    }
}