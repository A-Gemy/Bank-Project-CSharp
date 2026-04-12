using Bank_Project_CSharp.Core;
using System;

namespace Bank_Project_CSharp.Screens
{
    internal class clsMainScreen : clsScreen
    {
        private enum enMainMenuOptions
        {
            eListClients = 1,
            eAddNewClient = 2,
            eDeleteClient = 3,
            eUpdateClient = 4,
            eFindClient = 5,
            eShowTransactionsMenu = 6,
            eManageUsers = 7,
            eShowLoginRegister = 8,
            eLogout = 9
        }


        private static short ReadMainMenuOption()
        {
            short choice;

            Console.Write("Choose what do you want to do? [1 to 9]? ");
            while (!short.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 9)
            {
                Console.Write("Invalid choice, enter a number between 1 and 9: ");
            }

            return choice;
        }

        private static void ShowMainMenuOptions()
        {
            const int width = 45;
            string border = new string('-', width);

            Console.WriteLine(border);
            Console.WriteLine(CenterText("MAIN MENU", width));
            Console.WriteLine(border);
            Console.WriteLine("   [1] Show Client List");
            Console.WriteLine("   [2] Add New Client");
            Console.WriteLine("   [3] Delete Client");
            Console.WriteLine("   [4] Update Client Info");
            Console.WriteLine("   [5] Find Client");
            Console.WriteLine("   [6] Transactions");
            Console.WriteLine("   [7] Manage Users");
            Console.WriteLine("   [8] Login Register");
            Console.WriteLine("   [9] Logout");
            Console.WriteLine(border);
        }

        private static void GoBackToMainMenu()
        {
            Console.WriteLine("\nPress any key to go back to Main Menu...");
            Console.ReadKey();
            ShowMainMenu();
        }

        private static void ShowAllClientsScreen()
        {
            Console.Clear();
            clsShowClientsListScreen.ShowClientsList();
            GoBackToMainMenu();
        }

        private static void ShowAddNewClientScreen()
        {
            Console.Clear();
            clsAddNewClientScreen.ShowAddNewClientScreen();
            GoBackToMainMenu();
        }

        private static void ShowDeleteClientScreen()
        {
            Console.Clear();
            clsDeleteClientScreen.ShowDeleteClientScreen();
            GoBackToMainMenu();
        }

        private static void ShowUpdateClientScreen()
        {
            Console.Clear();
            clsUpdateClientScreen.ShowUpdateClientScreen();
            GoBackToMainMenu();
        }

        private static void ShowFindClientScreen()
        {
            Console.Clear();
            clsFindClientScreen.ShowFindClientScreen();
            GoBackToMainMenu();
        }

        private static void ShowTransactionsMenuScreen()
        {
            clsTransactionsScreen.ShowTransactionsMenu();
            GoBackToMainMenu();
        }

        private static void ShowManageUsersMenuScreen()
        {
            clsManageUsersScreen.ShowManageUsersMenu();
            GoBackToMainMenu();
        }

        private static void ShowLoginRegisterScreen()
        {
            Console.Clear();
            clsLoginRegisterScreen.ShowLoginRegisterScreen();
            GoBackToMainMenu();
        }

        private static void Logout()
        {
            Global.CurrentUser = clsUser.Find("", "");
        }

        private static void PerformMainMenuOption(enMainMenuOptions mainMenuOption)
        {
            switch (mainMenuOption)
            {
                case enMainMenuOptions.eListClients:
                    ShowAllClientsScreen();
                    break;

                case enMainMenuOptions.eAddNewClient:
                    ShowAddNewClientScreen();
                    break;

                case enMainMenuOptions.eDeleteClient:
                    ShowDeleteClientScreen();
                    break;

                case enMainMenuOptions.eUpdateClient:
                    ShowUpdateClientScreen();
                    break;

                case enMainMenuOptions.eFindClient:
                    ShowFindClientScreen();
                    break;

                case enMainMenuOptions.eShowTransactionsMenu:
                    ShowTransactionsMenuScreen();
                    break;

                case enMainMenuOptions.eManageUsers:
                    ShowManageUsersMenuScreen();
                    break;

                case enMainMenuOptions.eShowLoginRegister:
                    ShowLoginRegisterScreen();
                    break;

                case enMainMenuOptions.eLogout:
                    Logout();
                    break;
            }
        }


        public static void ShowMainMenu()
        {
            const int width = 45;

            Console.Clear();
            DrawScreenHeader("MAIN SCREEN", width: width);
            ShowMainMenuOptions();
            PerformMainMenuOption((enMainMenuOptions)ReadMainMenuOption());
        }


    }
}
