using Bank_Project_CSharp.Core;
using System;

namespace Bank_Project_CSharp.Screens
{
    internal class clsFindUserScreen : clsScreen
    {
        public static void ShowFindUserScreen()
        {
            DrawScreenHeader("FIND USER SCREEN");

            clsUser user = ReadUserByUserName("\nPlease Enter User Name or [Q] to cancel: ");

            if (user == null)
            {
                Console.WriteLine("\nOperation cancelled.");
                return;
            }

            Console.WriteLine("\nUser Found:");
            Console.WriteLine("--------------------");
            user.Print();
        }

    }
}
