using System;
namespace fsinvestments
{
    public class Transaction
    {
        public String Type { get; set; }
        public DateTime Date { get; set; }
        public Decimal Shares { get; set; }
        public Decimal Price { get; set; }
        public String Investor { get; set; }
        public String SalesRep { get; set; }
        public String Fund { get; set; }

    }
}
