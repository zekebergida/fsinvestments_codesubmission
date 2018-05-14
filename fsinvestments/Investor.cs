using System;
using System.Collections.Generic;

namespace fsinvestments
{
    public class Investor
    {
        public String Name { get; set; }
        public IList<InvestorFund> FundsTraded { get; set; }
    }
}
