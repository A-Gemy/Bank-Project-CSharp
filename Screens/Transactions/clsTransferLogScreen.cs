using Bank_Project_CSharp.Core;
using System;
using System.Collections.Generic;

namespace Bank_Project_CSharp.Screens
{
    internal class clsTransferLogScreen : clsScreen
    {
        private const int DateTimeWidth = 20;
        private const int SourceWidth = 15;
        private const int DestinationWidth = 15;
        private const int AmountWidth = 12;
        private const int SourceBalanceWidth = 15;
        private const int DestinationBalanceWidth = 18;
        private const int UserWidth = 15;

        private static string GetTransferLogHeader()
        {
            return
                $"| {"Date/Time",-DateTimeWidth} " +
                $"| {"Source",-SourceWidth} " +
                $"| {"Destination",-DestinationWidth} " +
                $"| {"Amount",AmountWidth} " +
                $"| {"Src Balance",SourceBalanceWidth} " +
                $"| {"Dst Balance",DestinationBalanceWidth} " +
                $"| {"User",-UserWidth} |";
        }

        private static void PrintTransferLogHeader(string title)
        {
            string header = GetTransferLogHeader();
            string border = new string('=', header.Length);
            string separator = new string('-', header.Length);
            int titlePadding = (header.Length - title.Length) / 2;

            Console.WriteLine();
            Console.WriteLine(border);
            Console.WriteLine(new string(' ', titlePadding) + title);
            Console.WriteLine(border);
            Console.WriteLine(header);
            Console.WriteLine(separator);
        }

        private static void PrintTransferLogRecordLine(string[] record)
        {
            Console.WriteLine(
                $"| {record[0],-DateTimeWidth} " +
                $"| {record[1],-SourceWidth} " +
                $"| {record[2],-DestinationWidth} " +
                $"| {decimal.Parse(record[3]),AmountWidth:F2} " +
                $"| {decimal.Parse(record[4]),SourceBalanceWidth:F2} " +
                $"| {decimal.Parse(record[5]),DestinationBalanceWidth:F2} " +
                $"| {record[6],-UserWidth} |");
        }

        private static void PrintTransferLogBody(List<string[]> records)
        {
            string header = GetTransferLogHeader();

            if (records.Count == 0)
            {
                string message = "No transfer log entries available.";
                Console.WriteLine($"| {message.PadRight(header.Length - 4)} |");
                return;
            }

            foreach (string[] record in records)
            {
                PrintTransferLogRecordLine(record);
            }
        }

        private static void PrintTransferLogFooter(int totalRecords)
        {
            string header = GetTransferLogHeader();
            string border = new string('=', header.Length);
            string footerText = $"Total Records: {totalRecords}";
            int padding = (header.Length - footerText.Length) / 2;

            Console.WriteLine(border);
            Console.WriteLine(new string(' ', padding) + footerText);
            Console.WriteLine(border);
        }


        public static void ShowTransferLogScreen()
        {
            List<string[]> records = clsBankClient.GetTransferLogList();

            DrawScreenHeader("TRANSFER LOG SCREEN", width: GetTransferLogHeader().Length);
            PrintTransferLogHeader("TRANSFER LOG LIST");
            PrintTransferLogBody(records);
            PrintTransferLogFooter(records.Count);
        }
    }
}
