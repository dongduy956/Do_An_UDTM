using DAO;
using DevExpress.XtraGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
   public class ChiTietNKBUS
    {
        QL_LinhKienDBDataContext db = new QL_LinhKienDBDataContext();
        private static ChiTietNKBUS instances;
        public static ChiTietNKBUS Instances
        {
            get
            {
                if (instances == null)
                    instances = new ChiTietNKBUS();
                return instances;
            }

            set
            {
                instances = value;
            }
        }

        public void getDataGV(GridControl gv, int mapn)
        {
            db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues,
                   db.CHITIETNKs);
            var lst = (from ctnk in db.CHITIETNKs where ctnk.MAPN == mapn select ctnk).ToList();
            gv.DataSource = Support.ToDataTable<CHITIETNK>(lst);
        }
        public List<CHITIETNK> getDataGV(int mapn)
        {
            db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues,
                   db.CHITIETNKs);
            return (from ctnk in db.CHITIETNKs where ctnk.MAPN == mapn select ctnk).ToList();

        }
        public int insert(int mapn, int malk, int soluong,double donGia)
        {
            var ctnk = db.CHITIETNKs.FirstOrDefault(x => x.MAPN == mapn && x.MALINHKIEN == malk);
            if (ctnk != null)
            {
                update(mapn, malk, (soluong + (int)ctnk.SOLUONG),donGia);
                return 2;
            }
            else
            {

                db.CHITIETNKs.InsertOnSubmit(new CHITIETNK()
                {
                    MAPN = mapn,
                    MALINHKIEN = malk,
                    SOLUONG = soluong,
                    DONGIA = donGia,
                    THANHTIEN = 0
                });
                db.SubmitChanges();
                return 1;

            }
        }
        public int update(int mapn, int malk, int soluong,double donGia)
        {
            if (soluong == 0)
            {
                delete(mapn, malk);
                return 2;
            }
            else
            {

                var ctnk = db.CHITIETNKs.FirstOrDefault(x => x.MAPN == mapn && x.MALINHKIEN == malk);
                if (ctnk == null)
                    return -1;
                ctnk.SOLUONG = soluong;
                ctnk.DONGIA =donGia;
                db.SubmitChanges();
                return 1;

            }
        }
        public int delete(int mapn, int malk)
        {

            var ctnk = db.CHITIETNKs.FirstOrDefault(x => x.MAPN == mapn && x.MALINHKIEN == malk);
            if (ctnk == null)
                return -1;
            db.CHITIETNKs.DeleteOnSubmit(ctnk);
            db.SubmitChanges();
            return 1;

        }
        public bool IsProduct(int malinhkien)
        {
            return db.CHITIETNKs.FirstOrDefault(x => x.MALINHKIEN == malinhkien) != null;
        }
    }
}
