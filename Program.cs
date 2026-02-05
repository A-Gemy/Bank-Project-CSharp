using Bank_Project_CSharp.Core;
using System;

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

        static void Main(string[] args)
        {

            //UpdateClient();
            AddNewClient();

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
