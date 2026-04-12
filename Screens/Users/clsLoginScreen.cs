using Bank_Project_CSharp.Core;
using System;

namespace Bank_Project_CSharp.Screens
{
    internal class clsLoginScreen : clsScreen
    {
        private static bool TryLogin(string username, string password)
        {
            Global.CurrentUser = clsUser.Find(username, password);
            return !Global.CurrentUser.IsEmptyUser;
        }

        public static bool ShowLoginScreen()
        {
            bool loginFailed = false;

            while (true)
            {
                Console.Clear();
                DrawScreenHeader("LOGIN SCREEN", width: 45);

                if (loginFailed)
                {
                    Console.WriteLine("Invalid User Name/Password!\n");
                }

                Console.Write("Enter User Name or [Q] to exit: ");
                string userName = Console.ReadLine()?.Trim() ?? "";

                if (string.Equals(userName, "q", StringComparison.OrdinalIgnoreCase))
                    return false;

                Console.Write("Enter Password: ");
                string password = Console.ReadLine()?.Trim() ?? "";

                if (TryLogin(userName, password))
                {
                    clsMainScreen.ShowMainMenu();
                    return true;
                }

                loginFailed = true;
            }

        }
    }
}