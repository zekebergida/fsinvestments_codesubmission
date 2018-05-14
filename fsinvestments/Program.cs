using System;
using System.IO;
using System.Collections.Generic;
using CsvHelper;
using System.Globalization;

namespace fsinvestments
{
    class Program
    {
        static void Main(string[] args)
        {
            string filepath = "../Data.csv";
            Console.WriteLine("Enter the filepath for the transaction data you would like reports for.");
            Console.WriteLine("Enter 'test' to run the test Data.csv file");

            string response = Console.ReadLine();
            if (!response.Equals("test"))
            {
                filepath = response;
            }

            List<Transaction> transactions = FileUtils.getTransactionFileData(filepath);

            ReportGenerator.GenerateReports(transactions);
            Console.WriteLine("Find your reports in this solution's root directory.");
        }
    }
}
