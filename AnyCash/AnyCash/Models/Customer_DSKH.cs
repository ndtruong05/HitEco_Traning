using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnyCash.Models
{
    public class Customer_DSKH
    {
        public Customer_DSKH()
        {
        }

        private Guid _khachHangID;
        public Guid KhachHangID
        {
            get { return _khachHangID; }
            set { _khachHangID = value; }
        }

        public string MaKH { get; set; }
        public string KhachHang { get; set; }

        public string DiaChi { get; set; }

        public string DienThoai { get; set; }

        public string CMND { get; set; }

        public string CuaHang { get; set; }

        public string NgayTao { get; set; }

        public string TinhTrang { get; set; }
    }
}