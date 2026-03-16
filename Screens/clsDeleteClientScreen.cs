using Bank_Project_CSharp.Core;
using System;

namespace Bank_Project_CSharp.Screens
{
    internal class clsDeleteClientScreen : clsScreen
    {

        private static string ReadAccountNumber()
        {
            Console.Write("\nPlease Enter Account Number: ");
            return Console.ReadLine()?.Trim() ?? "";
        }

        private static clsBankClient ReadClientToDelete()
        {
            string accountNumber = ReadAccountNumber();

            while (!clsBankClient.IsClientExist(accountNumber))
            {
                Console.Write("Account Number is not found.");
                accountNumber = ReadAccountNumber();
            }

            return clsBankClient.Find(accountNumber);
        }

        private static char ReadDeleteConfirmation()
        {
            char answer;

            do
            {
                Console.Write("\nAre you sure you want to delete this client (y/n)? ");
                answer = char.ToLower(Console.ReadKey().KeyChar);
            }
            while (answer != 'y' && answer != 'n');

            Console.WriteLine();
            return answer;
        }


        public static void ShowDeleteClientScreen()
        {
            DrawScreenHeader("DELETE CLIENT SCREEN");

            clsBankClient client = ReadClientToDelete();
            client.Print();

            char answer = ReadDeleteConfirmation();

            if (answer == 'y')
            {
                if (client.Delete())
                    Console.WriteLine("\nClient Deleted Successfully :-)");
                else
                    Console.WriteLine("\nError: client was not deleted.");
            }
            else
            {
                Console.WriteLine("\nOperation cancelled.");
            }
        }


    }
}
