using Bank_Project_CSharp.Core;
using System;
using System.Collections.Generic;

namespace Bank_Project_CSharp.Screens
{
    internal class clsTotalBalancesScreen : clsScreen
    {
        private const int BalanceAccWidth = 18;
        private const int BalanceNameWidth = 25;
        private const int BalanceAmountWidth = 15;

        private static string GetBalancesHeader()
        {
            return
                $"| {"Account Number",-BalanceAccWidth} " +
                $"| {"Client Name",-BalanceNameWidth} " +
                $"| {"Balance",BalanceAmountWidth} |";
        }

        private static void PrintBalancesListHeader(string title)
        {
            string header = GetBalancesHeader();
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

        private static void PrintClientRecordBalanceLine(clsBankClient client)
        {
            Console.WriteLine(
                $"| {client.AccountNumber,-BalanceAccWidth} " +
                $"| {client.FullName,-BalanceNameWidth} " +
                $"| {client.AccountBalance,BalanceAmountWidth:C} |");
        }

        private static void PrintBalancesListBody(List<clsBankClient> clients)
        {
            string header = GetBalancesHeader();

            if (clients.Count == 0)
            {
                string message = "No clients available in the system.";
                Console.WriteLine($"| {message.PadRight(header.Length - 4)} |");
                return;
            }

            foreach (clsBankClient client in clients)
            {
                PrintClientRecordBalanceLine(client);
            }
        }

        private static void PrintBalancesListFooter(decimal totalBalances)
        {
            string header = GetBalancesHeader();
            string border = new string('=', header.Length);
            string footerText = $"Total Balances: {totalBalances:C}";
            int padding = (header.Length - footerText.Length) / 2;

            Console.WriteLine(border);
            Console.WriteLine(new string(' ', padding) + footerText);
            Console.WriteLine(border);
        }


        public static void ShowTotalBalancesScreen()
        {
            List<clsBankClient> clients = clsBankClient.GetClientsList();
            decimal totalBalances = clsBankClient.GetTotalBalances();

            DrawScreenHeader("TOTAL BALANCES SCREEN", width: GetBalancesHeader().Length);
            PrintBalancesListHeader($"BALANCES LIST ({clients.Count}) Client(s)");
            PrintBalancesListBody(clients);
            PrintBalancesListFooter(totalBalances);
        }

    }
}
