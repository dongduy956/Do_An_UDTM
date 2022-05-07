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
    public class NhanVienBUS
    {
        QL_LinhKienDBDataContext db = new QL_LinhKienDBDataContext();
        private static NhanVienBUS instances;



        public static NhanVienBUS Instances
        {
            get
            {
                if (instances == null)
                    instances = new NhanVienBUS();
                return instances;
            }

            set
            {
                instances = value;
            }
        }
        public void getDataGV(GridControl gv)
        {

            gv.DataSource = (from nv in db.NHANVIENs
                             select new
                             {
                                nv.MANV,
                                nv.TENNV,
                                nv.taikhoan,
                                nv.SDT,
                                nv.NGAYVL,
                                nv.LUONG,
                                nv.GIOITINH,
                                nv.QUYEN.maquyen                                
                             });
        }
    }
}
