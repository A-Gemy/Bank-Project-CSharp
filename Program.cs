using Bank_Project_CSharp.Core;
using Bank_Project_CSharp.Screens;
using System;
using System.Collections.Generic;

namespace Bank_Project_CSharp
{
    internal class Program
    {
        static void ReadClientInfo(
            out string firstName,
            out string lastName,
            out string email,
            out string phone,
            out string pinCode,
            out decimal balance)
        {
            Console.Write("\nEnter First Name: ");
            firstName = Console.ReadLine()?.Trim() ?? "";

            Console.Write("Enter Last Name: ");
            lastName = Console.ReadLine()?.Trim() ?? "";

            Console.Write("Enter Email: ");
            email = Console.ReadLine()?.Trim() ?? "";

            Console.Write("Enter Phone: ");
            phone = Console.ReadLine()?.Trim() ?? "";

            Console.Write("Enter Pin Code: ");
            pinCode = Console.ReadLine()?.Trim() ?? "";

            Console.Write("Enter Account Balance: ");
            while (!decimal.TryParse(Console.ReadLine(), out balance))
            {
                Console.Write("Invalid number, enter Account Balance again: ");
            }

        }

        static void UpdateClient()
        {
            Console.Write("\nPlease Enter Client Account Number: ");
            string accountNumber = Console.ReadLine()?.Trim() ?? "";

            // Validate client exists
            while (!clsBankClient.IsClientExist(accountNumber))
            {
                Console.Write("Account Number not found. Enter again: ");
                accountNumber = Console.ReadLine()?.Trim() ?? "";
            }

            // Load client 
            clsBankClient client = clsBankClient.Find(accountNumber);
            client.Print();

            Console.WriteLine("\n\nUpdate Client Info:");
            Console.WriteLine("____________________\n");

            // Read updated info 
            ReadClientInfo(
                out string firstName,
                out string lastName,
                out string email,
                out string phone,
                out string pinCode,
                out decimal balance
            );

            client.UpdateClientInfo(firstName, lastName, email, phone, pinCode, balance);

            // Save client
            clsBankClient.enSaveResults saveSucceeded = client.Save(); // We'll implement Save() next

            switch (saveSucceeded)
            {
                case clsBankClient.enSaveResults.svSucceeded:
                    {
                        Console.WriteLine("\nAccount Updated Successfully :-)");
                        client.Print();
                        break;
                    }
                case clsBankClient.enSaveResults.svFailedEmptyObject:
                    {
                        Console.WriteLine("\nError, Account was not saved.");
                        break;
                    }
            }
        }

        static void AddNewClient()
        {
            Console.Write("\nPlease Enter New Client Account Number: ");
            string accountNumber = Console.ReadLine()?.Trim() ?? "";

            // Validate client Not exists
            while (clsBankClient.IsClientExist(accountNumber))
            {
                Console.Write("Account Number is already exists. Enter again: ");
                accountNumber = Console.ReadLine()?.Trim() ?? "";
            }

            clsBankClient newClient = clsBankClient.GetAddNewClientObject(accountNumber);

            ReadClientInfo(
                out string firstName,
                out string lastName,
                out string email,
                out string phone,
                out string pinCode,
                out decimal balance
            );

            newClient.UpdateClientInfo(firstName, lastName, email, phone, pinCode, balance);

            // Save new client
            clsBankClient.enSaveResults saveSucceeded = newClient.Save(); // We'll implement Save() next

            switch (saveSucceeded)
            {
                case clsBankClient.enSaveResults.svSucceeded:
                    {
                        Console.WriteLine("\nAccount Added Successfully :-)");
                        newClient.Print();
                        break;
                    }
                case clsBankClient.enSaveResults.svFailedEmptyObject:
                    {
                        Console.WriteLine("\nError account was not saved because it's Empty");
                        break;
                    }
                case clsBankClient.enSaveResults.svFailedAccountNumberExists:
                    {
                        Console.WriteLine("\nError account was not saved because account number is used!");
                        break;
                    }
            }

        }

        static void DeleteClient()
        {
            Console.Write("\nPlease Enter Account Number: ");
            string accountNumber = Console.ReadLine()?.Trim() ?? "";

            while (!clsBankClient.IsClientExist(accountNumber))
            {
                Console.Write("Account Number is not found. Enter existing one: ");
                accountNumber = Console.ReadLine()?.Trim() ?? "";
            }

            clsBankClient client = clsBankClient.Find(accountNumber);
            client.Print();

            char answer;
            do
            {
                Console.Write("\nAre you sure you want to delete this client (y/n)? ");
                answer = char.ToLower(Console.ReadKey().KeyChar);
            }
            while (answer != 'y' && answer != 'n');
            Console.WriteLine();

            if (answer == 'y')
            {
                if (client.Delete())
                {
                    Console.WriteLine("Client Deleted Successfully :-)");
                }
                else
                {
                    Console.WriteLine("Error: client was not deleted.");
                }
            }
            else
            {
                Console.WriteLine("Operation cancelled.");
            }
        }


        private const int BalanceAccWidth = 18;
        private const int BalanceNameWidth = 25;
        private const int BalanceAmountWidth = 15;

        static string GetBalancesHeader()
        {
            return
                $"| {"Account Number",-BalanceAccWidth} " +
                $"| {"Client Name",-BalanceNameWidth} " +
                $"| {"Balance",BalanceAmountWidth} |";
        }

        static void PrintBalancesListHeader(string title)
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

        static void PrintClientRecordBalanceLine(clsBankClient client)
        {
            Console.WriteLine(
                $"| {client.AccountNumber,-BalanceAccWidth} " +
                $"| {client.FullName,-BalanceNameWidth} " +
                $"| {client.AccountBalance,BalanceAmountWidth:C} |");
        }

        static void PrintBalancesListBody(List<clsBankClient> clients)
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

        static void PrintBalancesListFooter(decimal totalBalances)
        {
            string header = GetBalancesHeader();
            string border = new string('=', header.Length);
            string footerText = $"Total Balances: {totalBalances:C}";
            int padding = (header.Length - footerText.Length) / 2;

            Console.WriteLine(border);
            Console.WriteLine(new string(' ', padding) + footerText);
            Console.WriteLine(border);
        }

        static void ShowTotalBalances()
        {
            List<clsBankClient> clients = clsBankClient.GetClientsList();
            decimal totalBalances = clsBankClient.GetTotalBalances();

            PrintBalancesListHeader($"BALANCES LIST ({clients.Count}) Client(s)");
            PrintBalancesListBody(clients);
            PrintBalancesListFooter(totalBalances);
        }

        static void Main(string[] args)
        {

            //UpdateClient();
            //AddNewClient();
            //DeleteClient();
            //ShowClientsList();
            //ShowTotalBalances();

            clsMainScreen.ShowMainMenu();



        }
    }
}
