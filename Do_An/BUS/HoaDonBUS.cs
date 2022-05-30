using DAO;
using DevExpress.XtraGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class HoaDonBUS
    {
        QL_LinhKienDBDataContext db = new QL_LinhKienDBDataContext();
        private static HoaDonBUS instances;
        public static HoaDonBUS Instances
        {
            get
            {
                if (instances == null)
                    instances = new HoaDonBUS();
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
                    db.HOADONs);
            List<HOADON> lst;
            if (isPay)

                lst = (from hd in db.HOADONs select hd).ToList();
            else
                lst = (from hd in db.HOADONs where hd.ispay == false select hd).ToList();
            gv.DataSource = Support.ToDataTable<HOADON>(lst);
            
        }
        public int insert(int manv,int makh,double giamGia)
        {
            try
            {
                db.HOADONs.InsertOnSubmit(new HOADON() {
                    ispay=false,
                    giamgia=giamGia,
                    MAKH=makh,
                    MANV=manv,
                    NGAYLAP=DateTime.Now,
                    tongtien=0                  
                });
                db.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {

                return -1;
            }

        }
        public int delete(int mahd)
        {
            try
            {
                db.HOADONs.DeleteOnSubmit(db.HOADONs.FirstOrDefault(x => x.MAHD==mahd));
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                return -1;
            }
            return 1;
        }
        public int update(int mahd,bool ispay)
        {
            try
            {
                var hd = db.HOADONs.FirstOrDefault(x => x.MAHD == mahd);
                hd.ispay = ispay;                
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                return -1;
            }
            return 1;
        }
        public HOADON layHDVuaTao()
        {
            return db.HOADONs.OrderByDescending(x => x.MAHD).FirstOrDefault();
        }
        public HOADON findOrderCode(int code)
        {
            return db.HOADONs.FirstOrDefault(x => x.MAHD == code);
        }
        public bool checkIsStaffOrder(int manv)
        {
            return db.HOADONs.FirstOrDefault(x => x.MANV == manv) != null;
        }
        public bool IsCustomer(int makh)
        {
            return db.HOADONs.FirstOrDefault(x => x.MAKH == makh) != null;
        }
    }
}
