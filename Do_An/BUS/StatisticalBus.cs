using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
   public class StatisticalBus
    {
        private QL_LinhKienDBDataContext db = new QL_LinhKienDBDataContext();

        private static StatisticalBus instance;

        public static StatisticalBus Instance
        {
            get
            {
                if (instance == null)
                    return new StatisticalBus();
                return instance;
            }

            set
            {
                instance = value;
            }
        }

        /*
         0: ngày
         1: tháng
         2: năm
             */

        public double? tinhTienChi(DateTime datetime, int date = 0)
        {
            if (date == 1)
            {
                double? sumMoney = db.NHAPKHOs.Where(x =>x.ispay==true&& x.NGAYNHAP.Month == datetime.Month && x.NGAYNHAP.Year == datetime.Year).Sum(x => x.tongtien);
                return sumMoney;
            }
            else if (date == 2)
            {
                double? sumMoney = db.NHAPKHOs.Where(x => x.ispay == true && x.NGAYNHAP.Year == datetime.Year).Sum(x => x.tongtien);
                return sumMoney;
            }
            else
            {
                double? sumMoney = db.NHAPKHOs.Where(x => x.ispay == true && x.NGAYNHAP.CompareTo(datetime)==0).Sum(x => x.tongtien);
                return sumMoney;
            }
        }

        public double? tinhTienThu(DateTime datetime, int date = 0)
        {
            if (date == 1)
            {
                double? sumMoney = db.HOADONs.Where(x => x.ispay == true && x.NGAYLAP.Month == datetime.Month && x.NGAYLAP.Year == datetime.Year).Sum(x => x.tongtien);
                return sumMoney;
            }
            else if (date == 2)
            {
                double? sumMoney = db.HOADONs.Where(x => x.ispay == true && x.NGAYLAP.Year == datetime.Year).Sum(x => x.tongtien);
                return sumMoney;
            }
            else
            {
                double? sumMoney = db.HOADONs.Where(x => x.ispay == true && x.NGAYLAP.CompareTo(datetime) == 0).Sum(x => x.tongtien);
                return sumMoney;
            }
        }

    }
}
