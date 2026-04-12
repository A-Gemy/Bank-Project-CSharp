using Bank_Project_CSharp.Core;
using System;
using System.Collections.Generic;

namespace Bank_Project_CSharp.Screens
{
    internal class clsShowClientsListScreen : clsScreen
    {
        private const int AccWidth = 15;
        private const int NameWidth = 20;
        private const int PhoneWidth = 12;
        private const int EmailWidth = 30;
        private const int PinWidth = 10;
        private const int BalanceWidth = 15;


        private static string GetClientsHeader()
        {
            return
                $"| {"Account Number",-AccWidth} " +
                $"| {"Client Name",-NameWidth} " +
                $"| {"Phone",-PhoneWidth} " +
                $"| {"Email",-EmailWidth} " +
                $"| {"Pin Code",-PinWidth} " +
                $"| {"Balance",BalanceWidth} |";
        }

        private static void PrintClientsListHeader(string title)
        {
            string header = GetClientsHeader();
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

        private static void PrintClientRecordLine(clsBankClient client)
        {
            Console.WriteLine(
                $"| {client.AccountNumber,-AccWidth} " +
                $"| {client.FullName,-NameWidth} " +
                $"| {client.Phone,-PhoneWidth} " +
                $"| {client.Email,-EmailWidth} " +
                $"| {client.PinCode,-PinWidth} " +
                $"| {client.AccountBalance,BalanceWidth:C} |");
        }

        private static void PrintClientsListBody(List<clsBankClient> clients)
        {
            string header = GetClientsHeader();

            if (clients.Count == 0)
            {
                string message = "No clients available in the system.";
                Console.WriteLine($"| {message.PadRight(header.Length - 4)} |");
                return;
            }

            foreach (clsBankClient client in clients)
            {
                PrintClientRecordLine(client);
            }
        }

        private static void PrintClientsListFooter(int totalClients)
        {
            string header = GetClientsHeader();
            string border = new string('=', header.Length);
            string footerText = $"Total Clients: {totalClients}";
            int padding = (header.Length - footerText.Length) / 2;

            Console.WriteLine(border);
            Console.WriteLine(new string(' ', padding) + footerText);
            Console.WriteLine(border);
        }


        public static void ShowClientsList()
        {
            if (!CheckAccessRights(clsUser.enPermissions.pListClients))
                return;

            List<clsBankClient> clients = clsBankClient.GetClientsList();

            DrawScreenHeader("SHOW CLIENTS SCREEN", width: GetClientsHeader().Length);
            PrintClientsListHeader("CLIENT LIST");
            PrintClientsListBody(clients);
            PrintClientsListFooter(clients.Count);
        }


    }
}
