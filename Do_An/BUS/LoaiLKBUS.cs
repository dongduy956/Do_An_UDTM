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
   public class LoaiLKBUS
    {
        QL_LinhKienDBDataContext db = new QL_LinhKienDBDataContext();
        private static LoaiLKBUS instances;
        public static LoaiLKBUS Instances
        {
            get
            {
                if (instances == null)
                    instances = new LoaiLKBUS();
                return instances;
            }

            set
            {
                instances = value;
            }
        }
        public void getDataLkLoaiLK(RepositoryItemLookUpEdit lk)
        {
            lk.DataSource = from llk in db.LOAILINHKIENs select llk;
            lk.DisplayMember = "TENLOAI";
            lk.ValueMember = "MALOAI";
        }

        public void getDataGV(GridControl gv)
        {

            var lst = (from llk in db.LOAILINHKIENs select llk).ToList();

            gv.DataSource = Support.ToDataTable<LOAILINHKIEN>(lst);

        }
        public int insert(string tenLoai)
        {
            try
            {
                db.LOAILINHKIENs.InsertOnSubmit(new LOAILINHKIEN()
                {
                    TENLOAI = tenLoai
                });
                db.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {

                return -1;
            }

        }
        public int update(string tenLoai, int maLoai)
        {
            var llk = db.LOAILINHKIENs.FirstOrDefault(x => x.MALOAI == maLoai);

            try
            {
                if (llk == null)
                    return -1;
                llk.TENLOAI = tenLoai;
                db.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {

                return -1;
            }

        }
        public int delete(int maLoai)
        {
            try
            {
                var llk = db.LOAILINHKIENs.FirstOrDefault(x => x.MALOAI == maLoai);

                if (llk == null)
                    return -1;
                db.LOAILINHKIENs.DeleteOnSubmit(llk);
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
