using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using System.Data;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;

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
        
            var lst = (from kh in db.KHACHHANGs select kh).ToList();
          
            gv.DataSource = Support.ToDataTable<KHACHHANG>(lst);

        }
        public int insert(string tenkh,bool gioiTinh, string diaChi, string sdt,int maLoaiKH)
        {
            try
            {
                db.KHACHHANGs.InsertOnSubmit(new KHACHHANG()
                {
                    TENKH = tenkh,
                    DIACHI = diaChi,
                    GIOITINH = gioiTinh,
                    SDT = sdt,
                    maloaikh =maLoaiKH 
                });
                db.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {

                return -1;
            }           
            
        }
        public int update(string tenkh, bool gioiTinh, string diaChi, string sdt, int maloaikh,int maKH)
        {
            var kh = db.KHACHHANGs.FirstOrDefault(x => x.Makh==maKH);
           
            try
            {
                if (kh == null)
                    return -1;
                kh.TENKH = tenkh;
                kh.GIOITINH = gioiTinh;
                kh.DIACHI = diaChi;
                kh.SDT = sdt;
                kh.LOAIKH = db.LOAIKHs.Single(x => x.MALOAIKH == maloaikh);
                kh.maloaikh = maloaikh;
                db.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {

                return -1;
            }

        }
        public int delete (int maKH)
        {
            try
            {
                var kh = db.KHACHHANGs.FirstOrDefault(x => x.Makh == maKH);
                if (kh == null)
                    return -1;
                db.KHACHHANGs.DeleteOnSubmit(kh);
                db.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {

                return -1;
            }

        }
    }
}
