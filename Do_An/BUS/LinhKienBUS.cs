using DAO;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class LinhKienBUS
    {
        QL_LinhKienDBDataContext db = new QL_LinhKienDBDataContext();
        private static LinhKienBUS instances;
        public static LinhKienBUS Instances
        {
            get
            {
                if (instances == null)
                    instances = new LinhKienBUS();
                return instances;
            }

            set
            {
                instances = value;
            }
        }
        public void getDataGV(GridControl gv)
        {
            db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, db.LINHKIENs);
            var lst = (from lk in db.LINHKIENs select lk).ToList();
            gv.DataSource = Support.ToDataTable<LINHKIEN>(lst);

        }
        public int insert(string tenLK, int maLoai, string hangSX, double donGia, string hinhAnh, int soLuongCon)
        {
            try
            {
                db.LINHKIENs.InsertOnSubmit(new LINHKIEN()
                {
                    TENLINHKIEN = tenLK,
                    MALOAI = maLoai,
                    HANGSX = hangSX,
                    DONGIA = donGia,
                    HINHANH = hinhAnh,
                    SOLUONGCON = soLuongCon
                });
                db.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {

                return -1;
            }

        }
        public int update(string tenLK, int maLoai, string hangSX, double donGia, string hinhAnh, int soLuongCon, int maLK)
        {
            var lk = db.LINHKIENs.FirstOrDefault(x => x.MALINHKIEN == maLK);

            try
            {
                if (lk == null)
                    return -1;
                lk.TENLINHKIEN = tenLK;
                lk.LOAILINHKIEN = db.LOAILINHKIENs.Single(x => x.MALOAI == maLoai);
                lk.MALOAI = maLoai;
                lk.HANGSX = hangSX;
                lk.DONGIA = donGia;
                lk.HINHANH = hinhAnh;
                lk.SOLUONGCON = soLuongCon;
                db.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {

                return -1;
            }

        }
        public int delete(int maLK)
        {
            try
            {
                var lk = db.LINHKIENs.FirstOrDefault(x => x.MALINHKIEN == maLK);
                if (lk == null)
                    return -1;
                db.LINHKIENs.DeleteOnSubmit(lk);
                db.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {

                return -1;
            }

        }
        public void getDataLkLK(RepositoryItemLookUpEdit lk)
        {
            db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, db.LINHKIENs);
            lk.DataSource = from lkk in db.LINHKIENs select lkk;
            lk.DisplayMember = "TENLINHKIEN";
            lk.ValueMember = "MALINHKIEN";

        }

        public LINHKIEN timTheoMa(int malk)
        {
            return db.LINHKIENs.FirstOrDefault(x => x.MALINHKIEN == malk);
        }
    }
}
