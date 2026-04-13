using Bank_Project_CSharp.Core;
using System;
using System.Collections.Generic;

namespace Bank_Project_CSharp.Screens
{
    internal class clsLoginRegisterScreen : clsScreen
    {
        private const int DateTimeWidth = 22;
        private const int UserNameWidth = 15;
        private const int PermissionsWidth = 12;

        private static string GetLoginRegisterHeader()
        {
            return
                $"| {"Date/Time",-DateTimeWidth} " +
                $"| {"User Name",-UserNameWidth} " +
                $"| {"Permissions",PermissionsWidth} |";
        }

        private static void PrintLoginRegisterHeader(string title)
        {
            string header = GetLoginRegisterHeader();
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

        private static void PrintLoginRegisterRecordLine(string[] record)
        {
            Console.WriteLine(
                $"| {record[0],-DateTimeWidth} " +
                $"| {record[1],-UserNameWidth} " +
                $"| {record[2],PermissionsWidth} |");
        }

        private static void PrintLoginRegisterBody(List<string[]> records)
        {
            string header = GetLoginRegisterHeader();

            if (records.Count == 0)
            {
                string message = "No login register entries available.";
                Console.WriteLine($"| {message.PadRight(header.Length - 4)} |");
                return;
            }

            foreach (string[] record in records)
            {
                PrintLoginRegisterRecordLine(record);
            }
        }

        private static void PrintLoginRegisterFooter(int totalRecords)
        {
            string header = GetLoginRegisterHeader();
            string border = new string('=', header.Length);
            string footerText = $"Total Records: {totalRecords}";
            int padding = (header.Length - footerText.Length) / 2;

            Console.WriteLine(border);
            Console.WriteLine(new string(' ', padding) + footerText);
            Console.WriteLine(border);
        }


        public static void ShowLoginRegisterScreen()
        {
            if (!CheckAccessRights(clsUser.enPermissions.pLoginRegister))
                return;

            List<string[]> records = clsUser.GetLoginRegisterList();

            DrawScreenHeader("LOGIN REGISTER SCREEN", width: GetLoginRegisterHeader().Length);
            PrintLoginRegisterHeader("LOGIN REGISTER LIST");
            PrintLoginRegisterBody(records);
            PrintLoginRegisterFooter(records.Count);

        }

    }
}
