using Bank_Project_CSharp.Core;
using System;

namespace Bank_Project_CSharp
{
    internal class Program
    {
        static void ReadClientInfo(clsBankClient client)
        {
            Console.Write("\nEnter First Name: ");
            string firstName = Console.ReadLine()?.Trim() ?? "";

            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine()?.Trim() ?? "";

            Console.Write("Enter Email: ");
            string email = Console.ReadLine()?.Trim() ?? "";

            Console.Write("Enter Phone: ");
            string phone = Console.ReadLine()?.Trim() ?? "";

            Console.Write("Enter Pin Code: ");
            string pinCode = Console.ReadLine()?.Trim() ?? "";

            Console.Write("Enter Account Balance: ");
            decimal balance;
            while (!decimal.TryParse(Console.ReadLine(), out balance))
            {
                Console.Write("Invalid number, enter Account Balance again: ");
            }

            client.UpdateClientInfo(firstName, lastName, email, phone, pinCode, balance);
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
            ReadClientInfo(client);

            // Save client
            bool saveSucceeded = client.Save(); // We'll implement Save() next

            if (saveSucceeded)
            {
                Console.WriteLine("\nAccount Updated Successfully :-)");
                client.Print();
            }
            else
            {
                Console.WriteLine("\nError, Account was not saved because it's empty.");
            }
        }


        static void Main(string[] args)
        {

            UpdateClient();


            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
