using Bank_Project_CSharp.Core;
using System;

namespace Bank_Project_CSharp.Screens
{
    internal class clsUpdateClientScreen : clsScreen
    {
        private static string ReadAccountNumber()
        {
            Console.Write("\nPlease Enter Client Account Number: ");
            return Console.ReadLine()?.Trim() ?? "";
        }

        private static clsBankClient ReadClientToUpdate()
        {
            string accountNumber = ReadAccountNumber();

            while (!clsBankClient.IsClientExist(accountNumber))
            {
                Console.WriteLine("Account Number not found.");
                accountNumber = ReadAccountNumber();
            }

            return clsBankClient.Find(accountNumber);
        }

        private static void ReadClientInfo(
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

            clsBankClient client = ReadClientToUpdate();
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
