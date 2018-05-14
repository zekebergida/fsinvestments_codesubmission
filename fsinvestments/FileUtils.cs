using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

namespace fsinvestments
{
    public static class FileUtils
    {
       public static List<Transaction> getTransactionFileData(string filepath)
        {
            List<Transaction> transactions = new List<Transaction>();

            StreamReader data_reader = new StreamReader(@filepath);
            var csv = new CsvReader(data_reader);
            csv.Read();
            while (csv.Read())
            {
                Transaction transaction = new Transaction();
                transaction.Date = DateTime.Parse(csv[0]);
                transaction.Type = csv[1];
                transaction.Shares = Decimal.Parse(csv[2]);
                transaction.Price = Decimal.Parse(csv[3], NumberStyles.Currency);
                transaction.Fund = csv[4];
                transaction.Investor = csv[5];
                transaction.SalesRep = csv[6];

                transactions.Add(transaction);
            }

            return transactions;
        }

        public static void writeSalesRepReport(Dictionary<string, SalesRepSummary> saleRepsSummaries)
        {
            List<Object> reportRecords = new List<Object>();
            foreach (SalesRepSummary repSummary in saleRepsSummaries.Values)
            {
                decimal bondshares = !repSummary.FundsUnderManagement.Any(f => f.FundName == "BOND FUND") ? 0 : repSummary.FundsUnderManagement.FirstOrDefault(f => f.FundName == "BOND FUND").SharesUnderManagement;
                decimal stockshares = !repSummary.FundsUnderManagement.Any(f => f.FundName == "STOCK FUND") ? 0 : repSummary.FundsUnderManagement.FirstOrDefault(f => f.FundName == "STOCK FUND").SharesUnderManagement;
                var record = new
                {
                    SalesRep = repSummary.Name,
                    MonthSales = String.Format("{0:C2}", repSummary.MonthToDateSales),
                    QuarterSales = String.Format("{0:C2}", repSummary.QuarterToDateSales),
                    YearSales = String.Format("{0:C2}", repSummary.YearToDateSales),
                    LifetimeSales = String.Format("{0:C2}", repSummary.LifeTimeSales),
                    BondFundSharesManaging = bondshares,
                    StockFundSharesManaging = stockshares,
                };
                reportRecords.Add(record);
            }
            using (TextWriter writer = new StreamWriter(@"../rep_summary.csv"))
            {
                var csv = new CsvWriter(writer);
                csv.WriteRecords(reportRecords); //
            }
        }

        public static void writeInvestorReport(Dictionary<string, Investor> investors)
        {
            List<Object> reportRecords = new List<Object>();
            foreach (Investor investor in investors.Values)
            {
                decimal bondprofit = !investor.FundsTraded.Any(f => f.FundName == "BOND FUND") ? 0 : investor.FundsTraded.FirstOrDefault(f => f.FundName == "BOND FUND").Profit;
                decimal stockprofit = !investor.FundsTraded.Any(f => f.FundName == "STOCK FUND") ? 0 : investor.FundsTraded.FirstOrDefault(f => f.FundName == "STOCK FUND").Profit;
                var record = new
                {
                    Investor = investor.Name,
                    BondFundProfits = String.Format("{0:C2}", bondprofit),
                    StockFundProfits = String.Format("{0:C2}", stockprofit),
                };
                reportRecords.Add(record);
            }
            using (TextWriter writer = new StreamWriter(@"../investor_summary.csv"))
            {
                var csv = new CsvWriter(writer);
                csv.WriteRecords(reportRecords);
            }
        }
    }
}
