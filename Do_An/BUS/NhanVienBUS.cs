using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using System.Data;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using static System.Net.Mime.MediaTypeNames;
using DevExpress.Utils;
using System.Data.SqlClient;
using DevExpress.XtraEditors;
using System.Windows.Forms;

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
            var lst = (from nv in db.NHANVIENs select nv).ToList();
            gv.DataSource = Support.ToDataTable<NHANVIEN>(lst);
        }
        public int insert(string tennv, string diachi, string sdt, bool gioiTinh, DateTime ngayVL
            , double luong, string hinhAnh, string taiKhoan, int maQuyen)
        {
            try
            {
                db.NHANVIENs.InsertOnSubmit(new NHANVIEN()
                {
                    TENNV = tennv,
                    DIACHI = diachi,
                    SDT = sdt,
                    GIOITINH = gioiTinh,
                    NGAYVL = ngayVL,
                    LUONG = luong,
                    HINHANH = hinhAnh,
                    taikhoan = taiKhoan,
                    MATKHAU = Support.EndCodeMD5("12345"),
                    maquyen = maQuyen
                });
                db.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {

                return -1;
            }

        }
        public int update(string tennv, string diachi,string sdt,bool gioiTinh,DateTime ngayVL
            ,double luong,string hinhAnh,string taiKhoan,int maQuyen,int manv)
        {
            var nv = db.NHANVIENs.FirstOrDefault(x => x.MANV == manv);

            try
            {
                if (nv == null)
                    return -1;
                nv.TENNV = tennv;
                nv.SDT = sdt;
                nv.GIOITINH = gioiTinh;
                nv.NGAYVL = ngayVL;
                nv.LUONG = luong;
                nv.HINHANH = hinhAnh;
                nv.taikhoan = taiKhoan;
                nv.QUYEN = db.QUYENs.FirstOrDefault(x => x.maquyen == maQuyen);
                nv.maquyen = maQuyen;
                db.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {

                return -1;
            }

        }
        public int delete(int manv)
        {
            try
            {
                var nv = db.NHANVIENs.FirstOrDefault(x => x.MANV == manv);
                if (nv == null)
                    return -1;
                db.NHANVIENs.DeleteOnSubmit(nv);
                db.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {

                return -1;
            }

        }
        public int resetPass(int manv)
        {
            try
            {
                var nv = db.NHANVIENs.FirstOrDefault(x => x.MANV == manv);
                if (nv == null)
                    return -1;
                nv.MATKHAU = Support.EndCodeMD5("12345");
                db.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {

                return -1;
            }

        }
        public NHANVIEN login(string userName,string passWord,ref int errorCode)
        {
            passWord = Support.EndCodeMD5(passWord.Trim());
            NHANVIEN nv = null;
            try
            {
                nv = db.NHANVIENs.FirstOrDefault(x => x.taikhoan.Trim().Equals(userName.Trim()) && x.MATKHAU.Equals(passWord));

            }
            catch (SqlException ex)
            {
                errorCode = ex.ErrorCode;
            }
          
            return nv;
        }
        public int changePass(int manv,string oldPass,string newPass)
        {
            try
            {
                oldPass = Support.EndCodeMD5(oldPass);
                var nv = db.NHANVIENs.FirstOrDefault(x => x.MANV == manv&&x.MATKHAU.Equals(oldPass));
                if (nv == null)
                    return -1;
                nv.MATKHAU = Support.EndCodeMD5(newPass);
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
