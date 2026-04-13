using Bank_Project_CSharp.Core;
using System;

namespace Bank_Project_CSharp.Screens
{
    internal class clsScreen
    {
        private static string GetSystemInfoLine(int width)
        {
            string userName = Global.CurrentUser.IsEmptyUser ? "N/A" : Global.CurrentUser.UserName;
            string currentDate = DateTime.Now.ToString("dd-MM-yyyy");

            string info = $"User: {userName}     Date: {currentDate}";
            return CenterText(info, width);
        }

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

            Console.WriteLine(GetSystemInfoLine(width));
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

        protected static bool ReadYesNo(string message)
        {
            char answer;

            do
            {
                Console.Write(message);
                answer = char.ToLower(Console.ReadKey().KeyChar);
                Console.WriteLine();
            } while (answer != 'y' && answer != 'n');

            return answer == 'y';
        }

        protected static int ReadUserPermissions()
        {
            int permissions = 0;

            if (ReadYesNo("\nDo you want to give full access? y/n? "))
                return (int)clsUser.enPermissions.pAll;

            Console.WriteLine("\nDo you want to give access to:");

            if (ReadYesNo("\nShow Client List? y/n? "))
                permissions |= (int)clsUser.enPermissions.pListClients;

            if (ReadYesNo("\nAdd New Client? y/n? "))
                permissions |= (int)clsUser.enPermissions.pAddNewClient;

            if (ReadYesNo("\nDelete Client? y/n? "))
                permissions |= (int)clsUser.enPermissions.pDeleteClient;

            if (ReadYesNo("\nUpdate Client? y/n? "))
                permissions |= (int)clsUser.enPermissions.pUpdateClient;

            if (ReadYesNo("\nFind Client? y/n? "))
                permissions |= (int)clsUser.enPermissions.pFindClient;

            if (ReadYesNo("\nTransactions? y/n? "))
                permissions |= (int)clsUser.enPermissions.pTransactions;

            if (ReadYesNo("\nManage Users? y/n? "))
                permissions |= (int)clsUser.enPermissions.pManageUsers;

            if (ReadYesNo("\nShow Login Register? y/n? "))
                permissions |= (int)clsUser.enPermissions.pLoginRegister;

            return permissions;
        }

        protected static void ReadUserInfo(out string firstName,
            out string lastName,
            out string email,
            out string phone,
            out string password,
            out int permissions)
        {
            Console.Write("\nEnter First Name: ");
            firstName = Console.ReadLine()?.Trim() ?? "";

            Console.Write("Enter Last Name: ");
            lastName = Console.ReadLine()?.Trim() ?? "";

            Console.Write("Enter Email: ");
            email = Console.ReadLine()?.Trim() ?? "";

            Console.Write("Enter Phone: ");
            phone = Console.ReadLine()?.Trim() ?? "";

            Console.Write("Enter Password: ");
            password = Console.ReadLine()?.Trim() ?? "";

            permissions = ReadUserPermissions();
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


        protected static bool CheckAccessRights(clsUser.enPermissions permission)
        {
            if (!Global.CurrentUser.CheckAccessPermission(permission))
            {
                Console.Clear();
                Console.WriteLine("\n______________________________________");
                Console.WriteLine("  Access Denied! Contact your Admin.");
                Console.WriteLine("______________________________________\n");
                return false;
            }

            return true;
        }

    }
}
