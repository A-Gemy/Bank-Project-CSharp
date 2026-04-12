using Bank_Project_CSharp.Core;
using System;

namespace Bank_Project_CSharp.Screens
{
    internal class clsLoginScreen : clsScreen
    {
        private const short MaxFailedLoginTrials = 3;

        private static bool TryLogin(string username, string password)
        {
            Global.CurrentUser = clsUser.Find(username, password);
            return !Global.CurrentUser.IsEmptyUser;
        }

        public static bool ShowLoginScreen()
        {
            short failedLoginTrials = 0;

            while (true)
            {
                Console.Clear();
                DrawScreenHeader("LOGIN SCREEN", width: 45);

                if (failedLoginTrials > 0)
                {
                    Console.WriteLine($"Invalid User Name/Password! Trials left: {MaxFailedLoginTrials - failedLoginTrials}\n");
                }

                Console.Write("Enter User Name or [Q] to exit: ");
                string userName = Console.ReadLine()?.Trim() ?? "";

                if (string.Equals(userName, "q", StringComparison.OrdinalIgnoreCase))
                    return false;

                Console.Write("Enter Password: ");
                string password = Console.ReadLine()?.Trim() ?? "";

                if (TryLogin(userName, password))
                {
                    Global.CurrentUser.RegisterLogin();
                    clsMainScreen.ShowMainMenu();
                    return true;
                }

                failedLoginTrials++;

                if (failedLoginTrials >= MaxFailedLoginTrials)
                {
                    Console.Clear();
                    DrawScreenHeader("LOGIN SCREEN", width: 45);
                    Console.WriteLine("System is locked after 3 failed login attempts.");
                    Console.WriteLine("\nPress any key to close the application...");
                    Console.ReadKey();
                    return false;
                }
            }

        }

    }
}