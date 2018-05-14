using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;

namespace fsinvestments
{
    public static class ReportGenerator
    {
        public static void GenerateReports(List<Transaction> transactions)
        {
            GenerateSalesRepsReports(transactions);
            GenerateInvestorProfitReport(transactions);
        }

        public static void GenerateSalesRepsReports(List<Transaction> transactions)
        {
            //create a list of summaries for the sales reps
            Dictionary<string, SalesRepSummary> repsSummaries = new Dictionary<string, SalesRepSummary>();

            // go through each tansaction and tally the data to the relavent fields for the rep on that transaction
            foreach (Transaction transaction in transactions)
            {
                //if the sales rep for this transaction is not yet in the list then add the rep
                if (!repsSummaries.ContainsKey(transaction.SalesRep))
                {
                    SalesRepSummary newRep = new SalesRepSummary();
                    newRep.Name = transaction.SalesRep;
                    newRep.FundsUnderManagement = new List<SalesRepFund>();
                    repsSummaries.Add(transaction.SalesRep, newRep);
                }
                SalesRepSummary repSummary = repsSummaries[transaction.SalesRep];

                // if this transaction is the first time the sales rep is trading this fund
                // add the fund to the sale rep's list of funds
                if (!repSummary.FundsUnderManagement.Any(f => f.FundName == transaction.Fund))
                {
                    SalesRepFund newFund = new SalesRepFund();
                    newFund.FundName = transaction.Fund;
                    repSummary.FundsUnderManagement.Add(newFund);
                }

                SalesRepFund fund = repSummary.FundsUnderManagement.FirstOrDefault(f => f.FundName == transaction.Fund);

                // increment fund shares under management for sales rep based on transaction type
                if (transaction.Type == "BUY")
                {
                    fund.SharesUnderManagement += transaction.Shares;

                }
                else
                {
                    fund.SharesUnderManagement -= transaction.Shares;
                }

                //tally sales summary for sales rep based on date of buy transactions
                //if the transaction type is SELL then move on to the next transaction
                if (transaction.Type == "SELL")
                {
                    continue;
                }

                if (transaction.Date >= TimeUtils.GetStartOfMonth())
                {
                    repSummary.MonthToDateSales += transaction.Price * transaction.Shares;
                    repSummary.QuarterToDateSales += transaction.Price * transaction.Shares;
                    repSummary.YearToDateSales += transaction.Price * transaction.Shares;
                    repSummary.LifeTimeSales += transaction.Price * transaction.Shares;
                }
                else if (transaction.Date >= TimeUtils.GetStartOfQuarter())
                {
                    repSummary.QuarterToDateSales += transaction.Price * transaction.Shares;
                    repSummary.YearToDateSales += transaction.Price * transaction.Shares;
                    repSummary.LifeTimeSales += transaction.Price * transaction.Shares;
                }
                else if (transaction.Date >= TimeUtils.GetStartOfQuarter())
                {
                    repSummary.YearToDateSales += transaction.Price * transaction.Shares;
                    repSummary.LifeTimeSales += transaction.Price * transaction.Shares;
                }
                else
                {
                    repSummary.LifeTimeSales += transaction.Price * transaction.Shares;
                }
            }
           
            FileUtils.writeSalesRepReport(repsSummaries);
        }

        //please see readme.txt for an explaination for the calculation used for calculating profit
        public static void GenerateInvestorProfitReport(List<Transaction> transactions)
        {
            //create a list of investors and their funds to track their profits
            Dictionary<string, Investor> investors = new Dictionary<string, Investor>();

            //make sure transactions are ordered by date ascending and assess profit of each sale
            //based on average cost per share of shares held at time of sale
            foreach (Transaction transaction in transactions.OrderBy(t => t.Date))
            {
                // if the investor for the current transaction is not yet on the list then add the investor
                if (! investors.ContainsKey(transaction.Investor))
                {
                    Investor newInvestor = new Investor();
                    newInvestor.Name = transaction.Investor;
                    newInvestor.FundsTraded = new List<InvestorFund>();
                    investors.Add(transaction.Investor, newInvestor);
                }

                Investor investor = investors[transaction.Investor];

                // if this transaction is the first time the investor is trading this fund
                // add the fund to the investor's list of funds
                if (! investor.FundsTraded.Any(f => f.FundName == transaction.Fund))
                {
                    InvestorFund newFund = new InvestorFund();
                    newFund.FundName = transaction.Fund;
                    investor.FundsTraded.Add(newFund);
                }

                InvestorFund fund = investor.FundsTraded.FirstOrDefault(f => f.FundName == transaction.Fund);

                //if the transaction is a BUY then recalculate the average buy price
                //if the transaction is a SELL then calculate the gain/loss for that transaction and tally it toward the fund's total
                if (transaction.Type == "BUY")
                {
                    decimal newTotalCost = (fund.CurrentShares * fund.CurrentSharesAveragePrice) + (transaction.Shares * transaction.Price);
                    decimal newTotalShares = fund.CurrentShares + transaction.Shares;
                    fund.CurrentShares = newTotalShares;
                    fund.CurrentSharesAveragePrice = newTotalCost / fund.CurrentShares;
                }
                else
                {
                    decimal profitPerShare = transaction.Price - fund.CurrentSharesAveragePrice;
                    decimal totalTransactionProfit = profitPerShare * transaction.Shares;
                    fund.Profit += totalTransactionProfit;
                }
            }

            FileUtils.writeInvestorReport(investors);
        }




    }
}
