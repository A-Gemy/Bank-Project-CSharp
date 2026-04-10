using Bank_Project_CSharp.Core;
using System;

namespace Bank_Project_CSharp.Screens
{
    internal class clsDeleteUserScreen : clsScreen
    {
        private static char ReadDeleteConfirmation()
        {
            char answer;

            do
            {
                Console.Write("\nAre you sure you want to delete this user (y/n)? ");
                answer = char.ToLower(Console.ReadKey().KeyChar);
            } while (answer != 'y' && answer != 'n');

            Console.WriteLine();
            return answer;
        }


        public static void ShowDeleteUserScreen()
        {
            DrawScreenHeader("DELETE USER SCREEN");

            clsUser user = ReadUserByUserName("\nPlease Enter User Name or [Q] to cancel: ");
            if (user == null)
            {
                Console.WriteLine("\nOperation cancelled.");
                return;
            }

            user.Print();

            char answer = ReadDeleteConfirmation();

            if (answer == 'y')
            {
                if (user.Delete())
                    Console.WriteLine("\nUser Deleted Successfully :-)");
                else
                    Console.WriteLine("\nError: user was not deleted.");
            }
            else
            {
                Console.WriteLine("\nOperation cancelled.");
            }
        }


    }
}
