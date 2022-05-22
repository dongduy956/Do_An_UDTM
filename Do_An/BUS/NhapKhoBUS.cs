using DAO;
using DevExpress.XtraGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class NhapKhoBUS
    {
        QL_LinhKienDBDataContext db = new QL_LinhKienDBDataContext();
        private static NhapKhoBUS instances;
        public static NhapKhoBUS Instances
        {
            get
            {
                if (instances == null)
                    instances = new NhapKhoBUS();
                return instances;
            }

            set
            {
                instances = value;
            }
        }
        public void getDataGV(GridControl gv, bool isPay = true)
        {
            db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues,
                    db.NHAPKHOs);
            List<NHAPKHO> lst;
            if (isPay)
                lst = (from nk in db.NHAPKHOs select nk).ToList();
            else
                lst = (from nk in db.NHAPKHOs where nk.ispay == false select nk).ToList();
            gv.DataSource = Support.ToDataTable<NHAPKHO>(lst);
        }
        public int insert(int manv)
        {
            try
            {
                db.NHAPKHOs.InsertOnSubmit(new NHAPKHO()
                {
                    ispay = false,
                    MANV = manv,
                    NGAYNHAP = DateTime.Now,
                    tongtien = 0
                });
                db.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {

                return -1;
            }

        }
        public int delete(int mapn)
        {
            try
            {
                db.NHAPKHOs.DeleteOnSubmit(db.NHAPKHOs.FirstOrDefault(x => x.MAPN == mapn));
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                return -1;
            }
            return 1;
        }
        public int update(int mapn, bool ispay)
        {
            try
            {
                var nk = db.NHAPKHOs.FirstOrDefault(x => x.MAPN == mapn);
                nk.ispay = ispay;
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                return -1;
            }
            return 1;
        }
        public NHAPKHO layHDVuaTao()
        {
            return db.NHAPKHOs.OrderByDescending(x => x.MAPN).FirstOrDefault();
        }
        public NHAPKHO findOrderCode(int code)
        {
            return db.NHAPKHOs.FirstOrDefault(x => x.MAPN == code);
        }
    }
}
