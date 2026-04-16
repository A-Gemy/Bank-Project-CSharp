using Bank_Project_CSharp.Core;
using System;

namespace Bank_Project_CSharp.Screens
{
    internal class clsTransactionsScreen : clsScreen
    {
        private enum enTransactionsMenuOptions
        {
            eDeposit = 1,
            eWithdraw = 2,
            eShowTotalBalances = 3,
            eTransfer = 4,
            eTransferLog = 5,
            eShowMainMenu = 6
        }

        private static short ReadTransactionsMenuOption()
        {
            short choice;

            Console.Write("Choose what do you want to do? [1 to 6]? ");
            while (!short.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 6)
            {
                Console.Write("Invalid choice, enter a number between 1 and 6: ");
            }

            return choice;
        }

        private static void ShowTransactionsMenuOptions()
        {
            const int width = 45;
            string border = new string('-', width);

            Console.WriteLine(border);
            Console.WriteLine(CenterText("TRANSACTIONS MENU", width));
            Console.WriteLine(border);
            Console.WriteLine("   [1] Deposit");
            Console.WriteLine("   [2] Withdraw");
            Console.WriteLine("   [3] Total Balances");
            Console.WriteLine("   [4] Transfer");
            Console.WriteLine("   [5] Transfer Log");
            Console.WriteLine("   [6] Main Menu");
            Console.WriteLine(border);
        }

        private static void GoBackToTransactionsMenu()
        {
            Console.WriteLine("\nPress any key to go back to Transactions Menu...");
            Console.ReadKey();
            ShowTransactionsMenu();
        }

        private static void ShowDepositScreen()
        {
            Console.Clear();
            clsDepositScreen.ShowDepositScreen();
            GoBackToTransactionsMenu();
        }

        private static void ShowWithdrawScreen()
        {
            Console.Clear();
            clsWithdrawScreen.ShowWithdrawScreen();
            GoBackToTransactionsMenu();
        }

        private static void ShowTotalBalancesScreen()
        {
            Console.Clear();
            clsTotalBalancesScreen.ShowTotalBalancesScreen();
            GoBackToTransactionsMenu();
        }

        private static void ShowTransferScreen()
        {
            Console.Clear();
            clsTransferScreen.ShowTransferScreen();
            GoBackToTransactionsMenu();
        }

        private static void ShowTransferLogScreen()
        {
            Console.Clear();
            clsTransferLogScreen.ShowTransferLogScreen();
            GoBackToTransactionsMenu();
        }

        private static void PerformTransactionsMenuOption(enTransactionsMenuOptions transactionsMenuOption)
        {
            switch (transactionsMenuOption)
            {
                case enTransactionsMenuOptions.eDeposit:
                    ShowDepositScreen();
                    break;

                case enTransactionsMenuOptions.eWithdraw:
                    ShowWithdrawScreen();
                    break;

                case enTransactionsMenuOptions.eShowTotalBalances:
                    ShowTotalBalancesScreen();
                    break;

                case enTransactionsMenuOptions.eTransfer:
                    ShowTransferScreen();
                    break;

                case enTransactionsMenuOptions.eTransferLog:
                    ShowTransferLogScreen();
                    break;

                case enTransactionsMenuOptions.eShowMainMenu:
                    return;
            }
        }


        public static void ShowTransactionsMenu()
        {
            if (!CheckAccessRights(clsUser.enPermissions.pTransactions))
                return;

            const int width = 45;

            Console.Clear();
            DrawScreenHeader("TRANSACTIONS SCREEN", width: width);
            ShowTransactionsMenuOptions();
            PerformTransactionsMenuOption((enTransactionsMenuOptions)ReadTransactionsMenuOption());
        }

    }
}