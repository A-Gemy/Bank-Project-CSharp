using Bank_Project_CSharp.Core;
using System;

namespace Bank_Project_CSharp.Screens
{
    internal class clsAddNewUserScreen : clsScreen
    {
        public static void ShowAddNewUserScreen()
        {
            DrawScreenHeader("ADD NEW USER SCREEN", width: 45);

            Console.Write("\nPlease Enter User Name: ");
            string userName = Console.ReadLine()?.Trim() ?? "";

            while (clsUser.IsUserExist(userName))
            {
                Console.Write("\nUser Name is already used, choose another one: ");
                userName = Console.ReadLine()?.Trim() ?? "";
            }

            clsUser newUser = clsUser.GetAddNewUserObject(userName);

            ReadUserInfo(
                out string firstName,
                out string lastName,
                out string email,
                out string phone,
                out string password,
                out int permissions
            );

            newUser.UpdateUserInfo(firstName, lastName, email, phone, password, permissions);

            clsUser.enSaveResults saveResult = newUser.Save();

            switch (saveResult)
            {
                case clsUser.enSaveResults.svSucceeded:
                    Console.WriteLine("\nUser Added Successfully :-)");
                    newUser.Print();
                    break;

                case clsUser.enSaveResults.svFailedEmptyObject:
                    Console.WriteLine("\nError user was not saved because it's Empty");
                    break;

                case clsUser.enSaveResults.svFailedUserNameExists:
                    Console.WriteLine("\nError user was not saved because User Name is used!");
                    break;
            }
        }

    }
}