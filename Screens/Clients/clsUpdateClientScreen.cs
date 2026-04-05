using Bank_Project_CSharp.Core;
using System;

namespace Bank_Project_CSharp.Screens
{
    internal class clsUpdateClientScreen : clsScreen
    {

        private static void ShowUpdateResult(clsBankClient client, clsBankClient.enSaveResults saveResult)
        {
            switch (saveResult)
            {
                case clsBankClient.enSaveResults.svSucceeded:
                    Console.WriteLine("\nAccount Updated Successfully :-)");
                    client.Print();
                    break;

                case clsBankClient.enSaveResults.svFailedEmptyObject:
                    Console.WriteLine("\nError, Account was not saved.");
                    break;
            }
        }


        public static void ShowUpdateClientScreen()
        {
            DrawScreenHeader("UPDATE CLIENT SCREEN");

            clsBankClient client = ReadClientByAccountNumber("\nPlease Enter Client Account Number or [Q] to cancel: ");
            if (client == null)
            {
                Console.WriteLine("\nOperation cancelled.");
                return;
            }

            client.Print();

            Console.WriteLine("\nUpdate Client Info:");
            Console.WriteLine("--------------------");

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

            clsBankClient.enSaveResults saveResult = client.Save();
            ShowUpdateResult(client, saveResult);

        }

    }
}
