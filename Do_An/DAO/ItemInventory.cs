using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
   public class ItemInventory
    {
        private string _date;
        private int malk,soLuongNhap, soLuongBan;
        public ItemInventory(){}

        public string _Date
        {
            get
            {
                return _date;
            }

            set
            {
                _date = value;
            }
        }

        public int Malk
        {
            get
            {
                return malk;
            }

            set
            {
                malk = value;
            }
        }

        public int SoLuongBan
        {
            get
            {
                return soLuongBan;
            }

            set
            {
                soLuongBan = value;
            }
        }

        public int SoLuongNhap
        {
            get
            {
                return soLuongNhap;
            }

            set
            {
                soLuongNhap = value;
            }
        }
    }
}
