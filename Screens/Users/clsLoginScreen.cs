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

        private static void Login()
        {
            bool loginFailed = false;

            do
            {
                Console.Clear();
                DrawScreenHeader("LOGIN SCREEN", width: 45);

                if (loginFailed)
                {
                    Console.WriteLine("Invalid User Name/Password!\n");
                }

                Console.Write("Enter User Name: ");
                string userName = Console.ReadLine()?.Trim() ?? "";

                Console.Write("Enter Password: ");
                string password = Console.ReadLine()?.Trim() ?? "";

                loginFailed = !TryLogin(userName, password);

            } while (loginFailed);

            clsMainScreen.ShowMainMenu();
        }

        public static void ShowLoginScreen()
        {
            Login();
        }
    }
}
