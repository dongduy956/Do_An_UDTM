using DAO;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
   public class LoaiKHBUS
    {
        QL_LinhKienDBDataContext db = new QL_LinhKienDBDataContext();
        private static LoaiKHBUS instances;
        public static LoaiKHBUS Instances
        {
            get
            {
                if (instances == null)
                    instances = new LoaiKHBUS();
                return instances;
            }

            set
            {
                instances = value;
            }
        }
        public void getDataLkLoaiKH(RepositoryItemLookUpEdit lk)
        {
            lk.DataSource = from lkh in db.LOAIKHs select lkh;
            lk.DisplayMember = "TENLOAIKH";
            lk.ValueMember = "MALOAIKH";
        }

        public void getDataGV(GridControl gv)
        {

            var lst = (from lkh in db.LOAIKHs select lkh).ToList();

            gv.DataSource = Support.ToDataTable<LOAIKH>(lst);

        }
        public int insert(string tenLoaiKH,double giamGia)
        {
            try
            {
                db.LOAIKHs.InsertOnSubmit(new LOAIKH()
                {
                    TENLOAIKH=tenLoaiKH,
                    GIAMGIA=giamGia
                });
                db.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {

                return -1;
            }

        }
        public int update(string tenLoaiKH, double giamGia, int maLKH)
        {
            var lkh = db.LOAIKHs.FirstOrDefault(x => x.MALOAIKH == maLKH);

            try
            {
                if (lkh == null)
                    return -1;
                lkh.TENLOAIKH = tenLoaiKH;
                lkh.GIAMGIA = giamGia;
                db.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {

                return -1;
            }

        }
        public int delete(int maLoaiKH)
        {
            try
            {
                var lkh = db.LOAIKHs.FirstOrDefault(x => x.MALOAIKH == maLoaiKH);
                if (lkh == null)
                    return -1;
                db.LOAIKHs.DeleteOnSubmit(lkh);
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
