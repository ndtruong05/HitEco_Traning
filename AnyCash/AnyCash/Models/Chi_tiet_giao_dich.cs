using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnyCash.Models
{
    public class Chi_tiet_giao_dich
    {
        public string LoaiHinh { get; set; }
        public string MaHD { get; set; }
        public string NgGD { get; set; }
        public string KhachHang { get; set; }
        public DateTime NgayGD { get; set; }
        public string DienGiai { get; set; }
        public double Thu { get; set; }
        public double Chi { get; set; }
        public string GhiChu { get; set; }
    }
}