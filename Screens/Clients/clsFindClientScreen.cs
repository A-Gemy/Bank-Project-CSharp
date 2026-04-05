using Bank_Project_CSharp.Core;
using System;

namespace Bank_Project_CSharp.Screens
{
    internal class clsFindClientScreen : clsScreen
    {
        public static void ShowFindClientScreen()
        {
            DrawScreenHeader("FIND CLIENT SCREEN");

            clsBankClient client = ReadClientByAccountNumber("\nPlease Enter Account Number or [Q] to cancel: ");

            if (client == null)
            {
                Console.WriteLine("\nOperation cancelled.");
                return;
            }

            Console.WriteLine("\nClient Found:");
            Console.WriteLine("--------------------");
            client.Print();
        }
    }
}
