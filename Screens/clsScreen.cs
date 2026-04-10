using Bank_Project_CSharp.Core;
using System;

namespace Bank_Project_CSharp.Screens
{
    internal class clsScreen
    {
        protected static void DrawScreenHeader(string title, string subTitle = "", int width = 80)
        {
            string border = new string('=', width);

            Console.WriteLine();
            Console.WriteLine(border);
            Console.WriteLine(CenterText(title, width));

            if (!string.IsNullOrWhiteSpace(subTitle))
            {
                Console.WriteLine(CenterText(subTitle, width));
            }

            Console.WriteLine(border);
            Console.WriteLine();
        }

        protected static string CenterText(string text, int width)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            if (text.Length >= width)
                return text;

            int leftPadding = (width - text.Length) / 2;
            return new string(' ', leftPadding) + text;
        }

        protected static clsBankClient ReadClientByAccountNumber(string prompt = "\nPlease Enter Account Number: ")
        {
            while (true)
            {
                Console.Write(prompt);
                string accountNumber = Console.ReadLine()?.Trim() ?? "";

                if (string.Equals(accountNumber, "q", StringComparison.OrdinalIgnoreCase))
                    return null;

                if (clsBankClient.IsClientExist(accountNumber))
                    return clsBankClient.Find(accountNumber);

                Console.WriteLine("Account Number is not found. Enter a valid one or press [Q] to cancel.");
            }
        }

        protected static clsUser ReadUserByUserName(string prompt = "\nPlease Enter User Name: ")
        {
            while (true)
            {

                Console.Write(prompt);
                string userName = Console.ReadLine()?.Trim() ?? "";

                if (string.Equals(userName, "q", StringComparison.OrdinalIgnoreCase))
                    return null;

                if (clsUser.IsUserExist(userName))
                    return clsUser.Find(userName);

                Console.WriteLine("User Name is not found. Enter a valid one or press [Q] to cancel.");
            }
        }

        protected static void ReadClientInfo(
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


    }
}
