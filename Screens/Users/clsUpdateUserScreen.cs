using Bank_Project_CSharp.Core;
using System;

namespace Bank_Project_CSharp.Screens
{
    internal class clsUpdateUserScreen : clsScreen
    {
        private static char ReadUpdateConfirmation()
        {
            char answer;

            do
            {
                Console.Write("\nAre you sure you want to update this user (y/n)? ");
                answer = char.ToLower(Console.ReadKey().KeyChar);
            }
            while (answer != 'y' && answer != 'n');

            Console.WriteLine();
            return answer;
        }

        private static void ShowUpdateResult(clsUser user, clsUser.enSaveResults saveResult)
        {
            switch (saveResult)
            {
                case clsUser.enSaveResults.svSucceeded:
                    Console.WriteLine("\nUser Updated Successfully :-)");
                    user.Print();
                    break;

                case clsUser.enSaveResults.svFailedEmptyObject:
                    Console.WriteLine("\nError, user was not saved.");
                    break;
            }
        }

        public static void ShowUpdateUserScreen()
        {
            DrawScreenHeader("UPDATE USER SCREEN");

            clsUser user = ReadUserByUserName("\nPlease Enter User Name or [Q] to cancel: ");
            if (user == null)
            {
                Console.WriteLine("\nOperation cancelled.");
                return;
            }

            user.Print();

            char answer = ReadUpdateConfirmation();
            if (answer == 'n')
            {
                Console.WriteLine("\nOperation cancelled.");
                return;
            }

            Console.WriteLine("\nUpdate User Info:");
            Console.WriteLine("--------------------");

            ReadUserInfo(
                out string firstName,
                out string lastName,
                out string email,
                out string phone,
                out string password,
                out int permissions
            );

            user.UpdateUserInfo(firstName, lastName, email, phone, password, permissions);

            clsUser.enSaveResults saveResult = user.Save();
            ShowUpdateResult(user, saveResult);
        }

    }
}
