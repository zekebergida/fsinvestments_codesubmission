using System;
namespace fsinvestments
{
    public class InvestorFund
    {
        public String FundName { get; set; }
        public Decimal CurrentShares { get; set; }
        public Decimal CurrentSharesAveragePrice { get; set; }
        public Decimal Profit { get; set; }
    }
}
