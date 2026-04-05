using Bank_Project_CSharp.Core;
using System;

namespace Bank_Project_CSharp.Screens
{
    internal class clsDepositScreen : clsScreen
    {
        private static decimal ReadDepositAmount()
        {
            decimal amount;

            Console.Write("\nPlease Enter Deposit Amount: ");
            while (!decimal.TryParse(Console.ReadLine(), out amount) || amount <= 0)
            {
                Console.Write("Invalid amount, enter a positive number: ");
            }

            return amount;
        }

        private static char ReadDepositConfirmation()
        {
            char answer;

            do
            {
                Console.Write("\nAre you sure you want to perform this transaction (y/n)? ");
                answer = char.ToLower(Console.ReadKey().KeyChar);
            } while (answer != 'y' && answer != 'n');

            Console.WriteLine();
            return answer;
        }

        public static void ShowDepositScreen()
        {
            DrawScreenHeader("DEPOSIT SCREEN");

            clsBankClient client = ReadClientByAccountNumber("\nPlease Enter Account Number or [Q] to cancel: ");

            if (client == null)
            {
                Console.WriteLine("\nOperation cancelled.");
                return;
            }

            client.Print();

            decimal amount = ReadDepositAmount();
            char answer = ReadDepositConfirmation();

            if (answer == 'y')
            {
                client.Deposit(amount);
                Console.WriteLine("\nAmount Deposited Successfully.");
                Console.WriteLine($"New Balance Is: {client.AccountBalance:C}");
            }
            else
            {
                Console.WriteLine("\nOperation cancelled.");
            }
        }
    }
}
