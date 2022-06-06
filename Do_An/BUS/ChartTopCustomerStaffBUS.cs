﻿using DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
   public class ChartTopCustomerStaffBUS
    {
        QL_LinhKienDBDataContext db = new QL_LinhKienDBDataContext();
        private static ChartTopCustomerStaffBUS instances;
        public static ChartTopCustomerStaffBUS Instances
        {
            get
            {
                if (instances == null)
                    instances = new ChartTopCustomerStaffBUS();
                return instances;
            }

            set
            {
                instances = value;
            }
        }
        
        public DataTable loadTopCustomerBuy(bool checkType,DateTime date)
        {
            return Support.ToDataTable(db.topCustomerBuy(checkType,date).ToList());
        }
        public DataTable loadTopStaffSell(bool checkType,DateTime date)
        { 
            return Support.ToDataTable(db.topStaffSell(checkType, date).ToList());
        }
    }
}
