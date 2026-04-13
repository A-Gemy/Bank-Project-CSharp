using Bank_Project_CSharp.Core;
using System;

namespace Bank_Project_CSharp.Screens
{
    internal class clsTransferScreen : clsScreen
    {
        private static void PrintClient(clsBankClient client)
        {
            Console.WriteLine("\nClient Card:");
            Console.WriteLine("___________________");
            Console.WriteLine($"Full Name   : {client.FullName}");
            Console.WriteLine($"Acc. Number : {client.AccountNumber}");
            Console.WriteLine($"Balance     : {client.AccountBalance:C}");
            Console.WriteLine("___________________");
        }

        private static clsBankClient ReadDestinationClient(string sourceAccountNumber)
        {
            clsBankClient destinationClient = ReadClientByAccountNumber("\nPlease Enter Account Number to Transfer To: ");
            while (destinationClient != null && destinationClient.AccountNumber == sourceAccountNumber)
            {
                Console.WriteLine("\nSource and destination accounts cannot be the same.");
                destinationClient = ReadClientByAccountNumber("\nPlease Enter Account Number to Transfer To: ");
            }

            return destinationClient;
        }

        private static decimal ReadTransferAmount(clsBankClient sourceClient)
        {
            decimal amount;

            Console.Write("\nEnter Transfer Amount: ");
            while (!decimal.TryParse(Console.ReadLine(), out amount) || amount <= 0 || amount > sourceClient.AccountBalance)
            {
                Console.Write("Invalid amount or exceeds available balance, enter another amount: ");
            }

            return amount;
        }

        private static char ReadTransferConfirmation()
        {
            char answer;

            do
            {
                Console.Write("\nAre you sure you want to perform this operation (y/n)? ");
                answer = char.ToLower(Console.ReadKey().KeyChar);
            }
            while (answer != 'y' && answer != 'n');

            Console.WriteLine();
            return answer;
        }


        public static void ShowTransferScreen()
        {
            DrawScreenHeader("TRANSFER SCREEN");

            clsBankClient sourceClient = ReadClientByAccountNumber("\nPlease Enter Account Number to Transfer From or [Q] to cancel: ");
            if (sourceClient == null)
            {
                Console.WriteLine("\nOperation cancelled.");
                return;
            }

            PrintClient(sourceClient);

            clsBankClient destinationClient = ReadDestinationClient(sourceClient.AccountNumber);
            if (destinationClient == null)
            {
                Console.WriteLine("\nOperation cancelled.");
                return;
            }

            PrintClient(destinationClient);

            decimal amount = ReadTransferAmount(sourceClient);
            char answer = ReadTransferConfirmation();

            if (answer == 'y')
            {
                if (sourceClient.Transfer(amount, destinationClient))
                {
                    Console.WriteLine("\nTransfer done successfully.");
                    PrintClient(sourceClient);
                    PrintClient(destinationClient);
                }
                else
                {
                    Console.WriteLine("\nTransfer failed.");
                }
            }
            else
            {
                Console.WriteLine("\nOperation cancelled.");
            }
        }

    }
}
