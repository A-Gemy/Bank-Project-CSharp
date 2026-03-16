using Bank_Project_CSharp.Core;
using System;

namespace Bank_Project_CSharp.Screens
{
    internal class clsAddNewClientScreen : clsScreen
    {

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


        public static void ShowAddNewClientScreen()
        {
            DrawScreenHeader("ADD NEW CLIENT SCREEN", width: 45);

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

            clsBankClient.enSaveResults saveSucceeded = newClient.Save();

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


    }
}
