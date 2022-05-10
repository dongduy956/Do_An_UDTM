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
   public class QuyenBUS
    {
        QL_LinhKienDBDataContext db = new QL_LinhKienDBDataContext();
        private static QuyenBUS instances;
        public static QuyenBUS Instances
        {
            get
            {
                if (instances == null)
                    instances = new QuyenBUS();
                return instances;
            }

            set
            {
                instances = value;
            }
        }

        public void getDataLkQuyen(RepositoryItemLookUpEdit lk)
        {
            lk.DataSource = from q in db.QUYENs select q;
            lk.DisplayMember = "tenquyen";
            lk.ValueMember = "maquyen";
        }

        public void getDataGV(GridControl gv)
        {

            var lst = (from q in db.QUYENs select q).ToList();

            gv.DataSource = Support.ToDataTable<QUYEN>(lst);

        }
        public int insert(string tenQuyen)
        {
            try
            {
                db.QUYENs.InsertOnSubmit(new QUYEN()
                {
                   tenquyen=tenQuyen
                });
                db.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {

                return -1;
            }

        }
        public int update(string tenQuyen,int maQuyen)
        {
            var q = db.QUYENs.FirstOrDefault(x => x.maquyen == maQuyen);

            try
            {
                if (q == null)
                    return -1;
                q.tenquyen = tenQuyen;                
                db.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {

                return -1;
            }

        }
        public int delete(int maQuyen)
        {
            try
            {
                var q = db.QUYENs.FirstOrDefault(x => x.maquyen == maQuyen);
                if (q == null)
                    return -1;
                db.QUYENs.DeleteOnSubmit(q);
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
