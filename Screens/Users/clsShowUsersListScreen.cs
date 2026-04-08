using Bank_Project_CSharp.Core;
using System;
using System.Collections.Generic;

namespace Bank_Project_CSharp.Screens
{
    internal class clsShowUsersListScreen : clsScreen
    {
        private const int UserNameWidth = 15;
        private const int NameWidth = 20;
        private const int PhoneWidth = 12;
        private const int EmailWidth = 30;
        private const int PermissionsWidth = 12;

        private static string GetUsersHeader()
        {
            return
                $"| {"User Name",-UserNameWidth} " +
                $"| {"Full Name",-NameWidth} " +
                $"| {"Phone",-PhoneWidth} " +
                $"| {"Email",-EmailWidth} " +
                $"| {"Permissions",PermissionsWidth} |";
        }

        private static void PrintUsersListHeader(string title)
        {
            string header = GetUsersHeader();
            string border = new string('=', header.Length);
            string separator = new string('-', header.Length);
            int titlePadding = (header.Length - title.Length) / 2;

            Console.WriteLine();
            Console.WriteLine(border);
            Console.WriteLine(new string(' ', titlePadding) + title);
            Console.WriteLine(border);
            Console.WriteLine(header);
            Console.WriteLine(separator);
        }

        private static void PrintUserRecordLine(clsUser user)
        {
            Console.WriteLine(
                $"| {user.UserName,-UserNameWidth} " +
                $"| {user.FullName,-NameWidth} " +
                $"| {user.Phone,-PhoneWidth} " +
                $"| {user.Email,-EmailWidth} " +
                $"| {user.Permissions,PermissionsWidth} |");
        }

        private static void PrintUsersListBody(List<clsUser> users)
        {
            string header = GetUsersHeader();

            if (users.Count == 0)
            {
                string message = "No users available in the system.";
                Console.WriteLine($"| {message.PadRight(header.Length - 4)} |");
                return;
            }

            foreach (clsUser user in users)
            {
                PrintUserRecordLine(user);
            }
        }

        private static void PrintUsersListFooter(int totalUsers)
        {
            string header = GetUsersHeader();
            string border = new string('=', header.Length);
            string footerText = $"Total Users: {totalUsers}";
            int padding = (header.Length - footerText.Length) / 2;

            Console.WriteLine(border);
            Console.WriteLine(new string(' ', padding) + footerText);
            Console.WriteLine(border);
        }

        public static void ShowUsersList()
        {
            List<clsUser> users = clsUser.GetUsersList();

            DrawScreenHeader("SHOW USERS SCREEN", width: GetUsersHeader().Length);
            PrintUsersListHeader("USERS LIST");
            PrintUsersListBody(users);
            PrintUsersListFooter(users.Count);
        }

    }
}
