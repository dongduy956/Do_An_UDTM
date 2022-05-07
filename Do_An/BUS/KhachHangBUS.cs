using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using System.Data;
using DevExpress.XtraGrid;

namespace BUS
{
    public class KhachHangBUS
    {
        QL_LinhKienDBDataContext db = new QL_LinhKienDBDataContext();
        private static KhachHangBUS instances;



        public static KhachHangBUS Instances
        {
            get
            {
                if (instances == null)
                    instances = new KhachHangBUS();
                return instances;
            }

            set
            {
                instances = value;
            }
        }
        public void getDataGV(GridControl gv)
        {

            gv.DataSource = (from kh in db.KHACHHANGs
                             select new
                             {
                                 kh.Makh,
                                 kh.TENKH,
                                 kh.GIOITINH,
                                 kh.SDT,
                                 kh.DIACHI,
                                 kh.LOAIKH.TENLOAIKH
                             });
        }
    }
}
