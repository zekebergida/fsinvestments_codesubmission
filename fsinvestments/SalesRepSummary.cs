using System;
using System.Collections.Generic;

namespace fsinvestments
{
    public class SalesRepSummary
    {
        public String Name { get; set; }
        public Decimal MonthToDateSales { get; set; }
        public Decimal QuarterToDateSales { get; set; }
        public Decimal YearToDateSales { get; set; }
        public Decimal LifeTimeSales { get; set; }
        public Decimal AssetsUnderManagement { get; set; }
        public List<SalesRepFund> FundsUnderManagement { get; set; }
    }
}
