using AnyCash.Models;
using CORE.Models;
using CORE.Tables;
using CORE.Views;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace AnyCash.Controllers
{
    public class InstallmentController : BaseController
    {
        public ActionResult Index()
        {
            int height = (int)(Request.Browser.ScreenPixelsHeight * 0.85);
            return View(height);
        }

        public PartialViewResult _List(string keyText = "", string fromDate = "", string toDate = "", string type = "", int pageNumber = 1, int pageSize = 10)
        {
            ViewBag.pageNumber = pageNumber;
            ViewBag.pageSize = pageSize;
            int count = 0;
            List<INSTALLMENT_INFOR> list = new List<INSTALLMENT_INFOR>();
            try
            {
                int userId = ((USER_INFOR)Session[AppSession.AppSessionKeys.USER_INFO]).USER_ID;
                int shopId = 0;
                if (Session[AppSession.AppSessionKeys.SHOP_CURRENT] != null)
                    shopId = ((SHOP_INFOR)Session[AppSession.AppSessionKeys.SHOP_CURRENT]).SHOP_ID;
                list = CASH_INSTALLMENT_Service.GetByUser(userId, shopId, keyText, fromDate, toDate, type, pageNumber, pageSize, out count);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            ViewBag.maxNumber = Math.Ceiling((double)count / pageSize);

            return PartialView(list);
        }

        public ActionResult Create(int id = 0)
        {
            CASH_INSTALLMENTS shop = CASH_INSTALLMENT_Service.GetByKey(id);
            if (shop == null)
                shop = new CASH_INSTALLMENTS();

            List<SYS_USERS> staffList = new List<SYS_USERS>();
            List<CASH_CUSTOMERS> customerList = new List<CASH_CUSTOMERS>();
            try
            {
                int userId = ((USER_INFOR)Session[AppSession.AppSessionKeys.USER_INFO]).USER_ID;
                staffList = SYS_USER_Service.GetStaff(userId);

                int shopId = ((SHOP_INFOR)Session[AppSession.AppSessionKeys.SHOP_CURRENT]).SHOP_ID;
                customerList = CASH_CUSTOMER_Service.GetByShop(shopId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            ViewBag.StaffList = staffList;
            ViewBag.CustomerList = customerList;

            return View(shop);
        }

        public PartialViewResult _LichDongTien(int id)
        {
            List<CASH_INSTALLMENT_HISTORIES> list = new List<CASH_INSTALLMENT_HISTORIES>();
            try
            {
                list = CASH_INSTALLMENT_Service.GetByInstallmentId(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return PartialView(list);
        }

        public PartialViewResult _DanhSach(string maHD = "", string fromDate = "", string toDate = "")
        {
            List<Installment_DSHD> obj = new List<Installment_DSHD>();
            obj.Add(new Installment_DSHD
            {
                MaHD = "A",
                KhachHang = "Xa XA",
                TienGiaoKhach = 100000,
                Tile = "1/9",
                ThoiGian = 50,
                Ky = 10,
                NgayBoc = "13/07/2019",
                NgayHet = "4/7/2019",
                TienDaDong = 10000,
                NoCu = 10000,
                Tien1Ngay = 1000,
                ConPhaiDong = 90000,
                TinhTrang = "Đến ngày đóng họ",
                NgayPhaiDong = "31/7"
            });
            obj.Add(new Installment_DSHD
            {
                MaHD = "B",
                KhachHang = "Ba  BA",
                TienGiaoKhach = 500000,
                Tile = "1/9",
                ThoiGian = 50,
                Ky = 10,
                NgayBoc = "02/07/2019",
                NgayHet = "5/7/2019",
                TienDaDong = 10000,
                NoCu = 10000,
                Tien1Ngay = 1000,
                ConPhaiDong = 90000,
                TinhTrang = "Đến ngày đóng họ",
                NgayPhaiDong = "31/7"
            });
            obj.Add(new Installment_DSHD
            {
                MaHD = "C",
                KhachHang = "Ca CA",
                TienGiaoKhach = 300000,
                Tile = "1/9",
                ThoiGian = 50,
                Ky = 10,
                NgayBoc = "03/07/2019",
                NgayHet = "6/7/2019",
                TienDaDong = 10000,
                NoCu = 10000,
                Tien1Ngay = 1000,
                ConPhaiDong = 90000,
                TinhTrang = "Đến ngày đóng họ",
                NgayPhaiDong = "31/7"
            });
            List<Installment_DSHD> result = new List<Installment_DSHD>();
            if (fromDate != "" || maHD != "" || toDate != "")
            {
                if (maHD != "")
                {
                    List<Installment_DSHD> list = obj.Where(x => x.MaHD == maHD).ToList();
                    foreach (var item in list)
                    {
                        result.Add(item);
                    }
                }
                if (fromDate != "" && toDate != "")
                {
                    DateTime FromDate = Convert.ToDateTime(DateTime.ParseExact(fromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                    DateTime ToDate = Convert.ToDateTime(DateTime.ParseExact(toDate, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                    foreach (var item in obj)
                    {
                        DateTime ngay = Convert.ToDateTime(DateTime.ParseExact(item.NgayBoc, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                        if (ngay >= FromDate && ngay <= ToDate)
                        {
                            result.Add(item);
                        }
                    }
                }
                else if (fromDate != "")
                {
                    DateTime FromDate = Convert.ToDateTime(DateTime.ParseExact(fromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                    foreach (var item in obj)
                    {
                        DateTime ngay = Convert.ToDateTime(DateTime.ParseExact(item.NgayBoc, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                        if (ngay >= FromDate)
                        {
                            result.Add(item);
                        }
                    }
                }
                else if (toDate != "")
                {
                    DateTime ToDate = Convert.ToDateTime(DateTime.ParseExact(toDate, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                    foreach (var item in obj)
                    {
                        DateTime ngay = Convert.ToDateTime(DateTime.ParseExact(item.NgayBoc, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                        if (ngay <= ToDate)
                        {
                            result.Add(item);
                        }
                    }
                }
            }
            else
            {
                result = obj.ToList();
            }
            double TongTien = 0;
            double TongTienDaDong = 0;
            double TongNoCu = 0;
            double TongTien1Ngay = 0;
            double TongTienConPhaiDong = 0;

            foreach (var item in result)
            {
                TongTien += item.TienGiaoKhach;
                TongTienDaDong += item.TienDaDong;
                TongNoCu += item.NoCu;
                TongTien1Ngay += item.Tien1Ngay;
                TongTienConPhaiDong += item.ConPhaiDong;
            }

            ViewBag.TongTien = TongTien;
            ViewBag.TongTienDaDong = TongTienDaDong;
            ViewBag.TongNoCu = TongNoCu;
            ViewBag.TongTien1Ngay = TongTien1Ngay;
            ViewBag.TongTienConPhaiDong = TongTienConPhaiDong;
            return PartialView(result);
        }
        public PartialViewResult _ChiTietHopDong(string MaHD = "", string tenForm = "")
        {
            List<Installment_DSHD> obj = new List<Installment_DSHD>();
            obj.Add(new Installment_DSHD
            {
                MaHD = "A",
                BatHo = 100000,
                KhachHang = "Xa XA",
                SoDienThoai = "09176638264",
                TienGiaoKhach = 100000,
                Tile = "1/9",
                ThoiGian = 50,
                Ky = 10,
                NgayBoc = "01/07/2019",
                NgayHet = "4/7/2019",
                TienDaDong = 10000,
                NoCu = 10000,
                Tien1Ngay = 1000,
                ConPhaiDong = 90000,
                TinhTrang = "Đến ngày đóng họ",
                NgayPhaiDong = "31/7"
            });
            obj.Add(new Installment_DSHD
            {
                MaHD = "B",
                BatHo = 500000,
                KhachHang = "Ba  BA",
                SoDienThoai = "09176638264",
                TienGiaoKhach = 500000,
                Tile = "1/9",
                ThoiGian = 50,
                Ky = 10,
                NgayBoc = "02/07/2019",
                NgayHet = "5/7/2019",
                TienDaDong = 10000,
                NoCu = 10000,
                Tien1Ngay = 1000,
                ConPhaiDong = 90000,
                TinhTrang = "Đến ngày đóng họ",
                NgayPhaiDong = "31/7"
            });
            obj.Add(new Installment_DSHD
            {
                MaHD = "C",
                BatHo = 300000,
                KhachHang = "Ca CA",
                SoDienThoai = "09176638264",
                TienGiaoKhach = 300000,
                Tile = "1/9",
                ThoiGian = 50,
                Ky = 10,
                NgayBoc = "03/07/2019",
                NgayHet = "6/7/2019",
                TienDaDong = 10000,
                NoCu = 10000,
                Tien1Ngay = 1000,
                ConPhaiDong = 90000,
                TinhTrang = "Đến ngày đóng họ",
                NgayPhaiDong = "31/7"
            });
            ViewBag.tenForm = tenForm;
            ViewBag.NoCuKH = 100000;
            foreach (var item in obj)
            {
                if (item.MaHD == MaHD)
                {
                    ViewBag.HopDong = item;
                    ViewBag.TongTienPhaiDong = item.TienGiaoKhach;
                    ViewBag.TongLai = item.BatHo - item.TienGiaoKhach;
                }
            }
            return PartialView();
        }
        public PartialViewResult _DanhSachDongTien(string maHD = "")
        {
            List<LichDongTien_DSND> list1 = new List<LichDongTien_DSND>();
            list1.Add(new LichDongTien_DSND
            {
                SoThuTu = 1,
                MaHD = "A",
                NgayBatDau = "1/7/2019",
                NgayKetThuc = "2/7/2019",
                TienHo = 4000,
                NgayGiaoDich = "",
                TienKhachTra = 0,
                TinhTrang = "Chưa trả"
            });
            list1.Add(new LichDongTien_DSND
            {
                SoThuTu = 2,
                MaHD = "A",
                NgayBatDau = "3/7/2019",
                NgayKetThuc = "4/7/2019",
                TienHo = 4000,
                NgayGiaoDich = "",
                TienKhachTra = 0,
                TinhTrang = "Chưa trả"
            });
            list1.Add(new LichDongTien_DSND
            {
                SoThuTu = 3,
                MaHD = "A",
                NgayBatDau = "5/7/2019",
                NgayKetThuc = "6/7/2019",
                TienHo = 4000,
                NgayGiaoDich = "",
                TienKhachTra = 0,
                TinhTrang = "Chưa trả"
            });
            return PartialView(list1);
        }
    }
}