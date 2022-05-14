using DAO;
using DevExpress.XtraGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class ChiTietHDBUS
    {
        QL_LinhKienDBDataContext db = new QL_LinhKienDBDataContext();
        private static ChiTietHDBUS instances;
        public static ChiTietHDBUS Instances
        {
            get
            {
                if (instances == null)
                    instances = new ChiTietHDBUS();
                return instances;
            }

            set
            {
                instances = value;
            }
        }

        public void getDataGV(GridControl gv, int mahd)
        {
            db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues,
                   db.CHITIETHDs);
            var lst = (from cthd in db.CHITIETHDs where cthd.MAHD == mahd select cthd).ToList();
            gv.DataSource = Support.ToDataTable<CHITIETHD>(lst);
        }
        public int insert(int mahd, int malk, int soluong)
        {
            var cthd = db.CHITIETHDs.FirstOrDefault(x => x.MAHD == mahd && x.MALINHKIEN == malk);
            if (cthd != null)
            {
                update(mahd, malk, (soluong+ (int)cthd.SOLUONG));
                return 2;
            }
            else
            {
              
                    db.CHITIETHDs.InsertOnSubmit(new CHITIETHD()
                    {
                        MAHD = mahd,
                        MALINHKIEN = malk,
                        SOLUONG = soluong,
                        DONGIA = LinhKienBUS.Instances.timTheoMa(malk).DONGIA,
                        THANHTIEN = 0
                    });
                    db.SubmitChanges();
                    return 1;
               
            }
        }
        public int update(int mahd, int malk, int soluong)
        {
            if (soluong == 0)
            {
                delete(mahd, malk);
                return 2;
            }
            else
            {
              
                    var cthd = db.CHITIETHDs.FirstOrDefault(x => x.MAHD == mahd && x.MALINHKIEN == malk);
                    if (cthd == null)
                        return -1;
                    cthd.SOLUONG = soluong;
                    cthd.DONGIA = LinhKienBUS.Instances.timTheoMa(malk).DONGIA;
                    db.SubmitChanges();
                    return 1;
               
            }
        }
        public int delete(int mahd, int malk)
        {
         
                var cthd = db.CHITIETHDs.FirstOrDefault(x => x.MAHD == mahd && x.MALINHKIEN == malk);
                if (cthd == null)
                    return -1;
                db.CHITIETHDs.DeleteOnSubmit(cthd);
                db.SubmitChanges();
                return 1;
         
        }
    }
}
