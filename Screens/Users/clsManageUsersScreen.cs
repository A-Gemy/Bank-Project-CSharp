using Bank_Project_CSharp.Core;
using System;

namespace Bank_Project_CSharp.Screens
{
    internal class clsManageUsersScreen : clsScreen
    {
        private enum enManageUsersMenuOptions
        {
            eListUsers = 1,
            eAddNewUser,
            eDeleteUser,
            eUpdateUser,
            eFindUser,
            eMainMenu
        }

        private static short ReadManageUsersMenuOption()
        {
            short choice;

            Console.Write("Choose what do you want to do? [1 to 6]? ");
            while (!short.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 6)
            {
                Console.Write("Invalid choice, enter a number between 1 and 6: ");
            }

            return choice;
        }

        private static void ShowManageUsersMenuOptions()
        {
            const int width = 45;
            string border = new string('-', width);

            Console.WriteLine(border);
            Console.WriteLine(CenterText("MANAGE USERS MENU", width));
            Console.WriteLine(border);
            Console.WriteLine("   [1] List Users");
            Console.WriteLine("   [2] Add New User");
            Console.WriteLine("   [3] Delete User");
            Console.WriteLine("   [4] Update User");
            Console.WriteLine("   [5] Find User");
            Console.WriteLine("   [6] Main Menu");
            Console.WriteLine(border);
        }

        private static void GoBackToManageUsersMenu()
        {
            Console.WriteLine("\nPress any key to go back to Manage Users Menu...");
            Console.ReadKey();
            ShowManageUsersMenu();
        }

        private static void ShowListUsersScreen()
        {
            Console.Clear();
            clsShowUsersListScreen.ShowUsersList();
            GoBackToManageUsersMenu();
        }

        private static void ShowAddNewUserScreen()
        {
            Console.Clear();
            clsAddNewUserScreen.ShowAddNewUserScreen();
            GoBackToManageUsersMenu();
        }

        private static void ShowDeleteUserScreen()
        {
            Console.Clear();
            clsDeleteUserScreen.ShowDeleteUserScreen();
            GoBackToManageUsersMenu();
        }

        private static void ShowUpdateUserScreen()
        {
            Console.Clear();
            clsUpdateUserScreen.ShowUpdateUserScreen();
            GoBackToManageUsersMenu();
        }

        private static void ShowFindUserScreen()
        {
            Console.Clear();
            clsFindUserScreen.ShowFindUserScreen();
            GoBackToManageUsersMenu();
        }

        private static void PerformManageUsersMenuOption(enManageUsersMenuOptions manageUsersMenuOption)
        {
            switch (manageUsersMenuOption)
            {
                case enManageUsersMenuOptions.eListUsers:
                    ShowListUsersScreen();
                    break;

                case enManageUsersMenuOptions.eAddNewUser:
                    ShowAddNewUserScreen();
                    break;

                case enManageUsersMenuOptions.eDeleteUser:
                    ShowDeleteUserScreen();
                    break;

                case enManageUsersMenuOptions.eUpdateUser:
                    ShowUpdateUserScreen();
                    break;

                case enManageUsersMenuOptions.eFindUser:
                    ShowFindUserScreen();
                    break;

                case enManageUsersMenuOptions.eMainMenu:
                    return;
            }
        }


        public static void ShowManageUsersMenu()
        {
            if (!CheckAccessRights(clsUser.enPermissions.pManageUsers))
                return;

            const int width = 45;

            Console.Clear();
            DrawScreenHeader("MANAGE USERS SCREEN", width: width);
            ShowManageUsersMenuOptions();
            PerformManageUsersMenuOption((enManageUsersMenuOptions)ReadManageUsersMenuOption());
        }

    }
}
