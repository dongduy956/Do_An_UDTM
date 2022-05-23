using DAO;
using DevExpress.XtraGrid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class InventoryBUS
    {
        QL_LinhKienDBDataContext db = new QL_LinhKienDBDataContext();
        private static InventoryBUS instances;
        public static InventoryBUS Instances
        {
            get
            {
                if (instances == null)
                    instances = new InventoryBUS();
                return instances;
            }

            set
            {
                instances = value;
            }
        }
        public int luongNhapVao(DateTime dateTimeFrom, DateTime dateTimeTo)
        {
            var lstImport = db.NHAPKHOs.Where(x => x.ispay == true && x.NGAYNHAP.CompareTo(dateTimeFrom) >= 0 && x.NGAYNHAP.CompareTo(dateTimeTo) <= 0).ToList();
            int sum = 0;
            foreach (var import in lstImport)
                sum += db.CHITIETNKs.Where(x => x.MAPN == import.MAPN).Sum(x => x.SOLUONG)??0;
            return sum;
        }

        public int luongBanRa(DateTime dateTimeFrom, DateTime dateTimeTo)
        {
            var lstOrder = db.HOADONs.Where(x => x.ispay == true && x.NGAYLAP.CompareTo(dateTimeFrom) >= 0 && x.NGAYLAP.CompareTo(dateTimeTo) <= 0).ToList();
            int sum = 0;
            foreach (var order in lstOrder)
            {
                sum += db.CHITIETHDs.Where(x => x.MAHD == order.MAHD).Sum(x => x.SOLUONG)??0;
            }
            return sum;
        }
        public DataTable loadDetailInventory(GridControl gc, DateTime dateTimeFrom, DateTime dateTimeTo)
        {
            DataTable tb = new DataTable();
            List<ItemInventory> lstItemInventory = new List<ItemInventory>();
            tb.Columns.Add("ngay");
            tb.Columns.Add("linhkien");
            tb.Columns.Add("nhap");
            tb.Columns.Add("ban");
            var lstOrder = db.HOADONs.Where(x => x.ispay == true && x.NGAYLAP.CompareTo(dateTimeFrom) >= 0 && x.NGAYLAP.CompareTo(dateTimeTo) <= 0).ToList();
            var lstImport = db.NHAPKHOs.Where(x => x.ispay == true && x.NGAYNHAP.CompareTo(dateTimeFrom) >= 0 && x.NGAYNHAP.CompareTo(dateTimeTo) <= 0).ToList();
            for (DateTime date = dateTimeFrom; date.CompareTo(dateTimeTo) <= 0; date = date.AddDays(1))
            {

                var lstOrderTemp = lstOrder.Where(x => DateTime.Parse(x.NGAYLAP.ToShortDateString()).CompareTo(DateTime.Parse(date.ToShortDateString())) == 0).ToList();
                var lstImportTemp = lstImport.Where(x => DateTime.Parse(x.NGAYNHAP.ToShortDateString()).CompareTo(DateTime.Parse(date.ToShortDateString())) == 0).ToList();
                if (lstOrderTemp.Count != 0 || lstImportTemp.Count != 0)
                {
                    loadProductOfDay(lstItemInventory, date, lstImportTemp, lstOrderTemp);
                    loadTotalImportAndOrder(lstItemInventory,lstImportTemp,lstOrderTemp);
                }
               
            }
            //thêm datarow vào datatable
            foreach(var item in lstItemInventory)
            {
                DataRow dr = tb.NewRow();
                dr[0] = item._Date;
                dr[1] = LinhKienBUS.Instances.timTheoMa(item.Malk).TENLINHKIEN;
                dr[2] = item.SoLuongNhap;
                dr[3] = item.SoLuongBan;
                tb.Rows.Add(dr);
            }
            gc.DataSource = tb;
            return tb;
        }

        private void loadTotalImportAndOrder(List<ItemInventory> lstItemInventory, List<NHAPKHO> lstImportTemp, List<HOADON> lstOrderTemp)
        {
            //tính tổng số lượng nhập của 1 linh kiện trong 1 ngày
            foreach (var import in lstImportTemp)
            {
                for (int i = 0; i < lstItemInventory.Count; i++)
                {

                    int soLuongNhap = db.CHITIETNKs.Where(x => x.MAPN == import.MAPN && x.MALINHKIEN == lstItemInventory[i].Malk).Sum(x => x.SOLUONG) ?? 0;
                    lstItemInventory[i].SoLuongNhap += soLuongNhap;
                }
            }
            //tính tổng số lượng bán của 1 linh kiện trong 1 ngày

            foreach (var order in lstOrderTemp)
            {
                for (int i = 0; i < lstItemInventory.Count; i++)
                {
                    int soLuongBan = db.CHITIETHDs.Where(x => x.MAHD == order.MAHD && x.MALINHKIEN == lstItemInventory[i].Malk).Sum(x => x.SOLUONG) ?? 0;
                    lstItemInventory[i].SoLuongBan += soLuongBan;
                }
            }
        }

        private void loadProductOfDay(List<ItemInventory> lstItemInventory,DateTime date, List<NHAPKHO> lstImportTemp, List<HOADON> lstOrderTemp)
        {
          
            //thêm tất cả linh kiện trong danh sách nhập kho theo ngày
            foreach (var import in lstImportTemp)
                foreach (var importDetail in db.CHITIETNKs.Where(x => x.MAPN == import.MAPN))
                    if (lstItemInventory.FirstOrDefault(x => x.Malk == importDetail.MALINHKIEN) == null)
                        lstItemInventory.Add(new ItemInventory() { _Date = date.ToShortDateString(), Malk = importDetail.MALINHKIEN, SoLuongBan = 0, SoLuongNhap = 0 });
            //thêm tất cả linh kiện trong danh sách hoá đơn theo ngày
            foreach (var order in lstOrderTemp)
                foreach (var orderDetail in db.CHITIETHDs.Where(x => x.MAHD == order.MAHD))
                    if (lstItemInventory.FirstOrDefault(x => x.Malk == orderDetail.MALINHKIEN) == null)
                        lstItemInventory.Add(new ItemInventory() { _Date = date.ToShortDateString(), Malk = orderDetail.MALINHKIEN, SoLuongBan = 0, SoLuongNhap = 0 });
        }
    }
}
