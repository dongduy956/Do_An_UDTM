using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
   public class ItemNoronNextDay
    {
        private DateTime date;
        private double? revenue;

        public DateTime Date
        {
            get
            {
                return date;
            }

            set
            {
                date = value;
            }
        }

        public double? Revenue
        {
            get
            {
                return revenue;
            }

            set
            {
                revenue = value;
            }
        }
    }
}
