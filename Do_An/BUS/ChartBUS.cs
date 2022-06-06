﻿using DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class ChartBUS
    {
        QL_LinhKienDBDataContext db = new QL_LinhKienDBDataContext();
        private static ChartBUS instances;
        public static ChartBUS Instances
        {
            get
            {
                if (instances == null)
                    instances = new ChartBUS();
                return instances;
            }

            set
            {
                instances = value;
            }
        }
        //đơn hàng phiếu nhập trong tháng hiện tại
        public DataTable loadOrderAndImportInMonthNow()
        {
            DataTable tb = new DataTable();
            tb.Columns.Add("ten");
            tb.Columns.Add("soluong");
            DataRow dr = tb.NewRow();
            dr[0] = "Đơn hàng";
            dr[1] = db.HOADONs.Count(x => x.NGAYLAP.Month == DateTime.Now.Month && x.NGAYLAP.Year == DateTime.Now.Year);
            tb.Rows.Add(dr);
            dr = tb.NewRow();
            dr[0] = "Phiếu nhập";
            dr[1] = db.NHAPKHOs.Count(x => x.NGAYNHAP.Month == DateTime.Now.Month && x.NGAYNHAP.Year == DateTime.Now.Year);
            tb.Rows.Add(dr);

            return tb;
        }

        //top sản phẩm bán chạy nhất
        public DataTable loadTopSelling()
        {
            return Support.ToDataTable(db.topSelling().OrderByDescending(x => x.soluong).ThenBy(x => x.TENLINHKIEN).ToList());
        }
        //load doanh thu năm hiện tại
        public DataTable loadStatisticalYear()
        {
            DataTable tb = new DataTable();
            tb.Columns.Add("ten");
            tb.Columns.Add("soluong");
            DataRow dr = tb.NewRow();
            dr[0] = "Tiền thu";
            dr[1] = db.HOADONs.Where(x => x.NGAYLAP.Year == DateTime.Now.Year).Sum(x => x.tongtien);
            tb.Rows.Add(dr);
            dr = tb.NewRow();
            dr[0] = "Tiền chi";
            dr[1] = db.NHAPKHOs.Where(x => x.NGAYNHAP.Year == DateTime.Now.Year).Sum(x => x.tongtien);
            tb.Rows.Add(dr);

            return tb;
        }
        //sản phẩm sắp hết hàng <=5
        public DataTable loadProductNotStock()
        {
            DataTable tb = new DataTable();
            tb.Columns.Add("ten");
            tb.Columns.Add("soluong");
            foreach(var item in db.LINHKIENs.Where(x=>x.SOLUONGCON<5))
            {
                DataRow dr = tb.NewRow();
                dr[0] = item.TENLINHKIEN;
                dr[1] = item.SOLUONGCON;
                tb.Rows.Add(dr);
            }
            return tb;
        }
    }
}
