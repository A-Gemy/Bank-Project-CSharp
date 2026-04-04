using Bank_Project_CSharp.Core;
using System;

namespace Bank_Project_CSharp.Screens
{
    internal class clsWithdrawScreen : clsScreen
    {
        private static decimal ReadWithdrawAmount()
        {
            decimal amount;

            Console.Write("Enter the amount to withdraw: ");
            while (!decimal.TryParse(Console.ReadLine(), out amount) || amount <= 0)
            {
                Console.Write("Invalid amount, enter a positive number: ");
            }

            return amount;
        }

        private static char ReadWithdrawConfirmation()
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

        public static void ShowWithdrawScreen()
        {
            DrawScreenHeader("WITHDRAW SCREEN");

            clsBankClient client = ReadClientByAccountNumber("\nPlease Enter Account Number or [Q] to cancel: ");
            if (client == null)
            {
                Console.WriteLine("\nOperation cancelled.");
                return;
            }

            client.Print();

            decimal amount = ReadWithdrawAmount();
            char answer = ReadWithdrawConfirmation();

            if (answer == 'y')
            {
                if (client.Withdraw(amount))
                {
                    Console.WriteLine("\nAmount Withdrawn Successfully.");
                    Console.WriteLine($"New Balance Is: {client.AccountBalance:C}");
                }
                else
                {
                    Console.WriteLine("\nCannot withdraw, insufficient balance!");
                    Console.WriteLine($"Amount to withdraw is: {amount:C}");
                    Console.WriteLine($"Your Balance is: {client.AccountBalance:C}");
                }
            }
            else
            {
                Console.WriteLine("\nOperation cancelled.");
            }


        }
    }
}
