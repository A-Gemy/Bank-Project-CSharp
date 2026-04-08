using Bank_Project_CSharp.Core;
using System;

namespace Bank_Project_CSharp.Screens
{
    internal class clsAddNewUserScreen : clsScreen
    {
        private static bool ReadYesNo(string message)
        {
            char answer;

            do
            {
                Console.Write(message);
                answer = char.ToLower(Console.ReadKey().KeyChar);
                Console.WriteLine();
            } while (answer != 'y' && answer != 'n');

            return answer == 'y';
        }

        private static int ReadUserPermissions()
        {
            int permissions = 0;

            if (ReadYesNo("\nDo you want to give full access? y/n? "))
                return (int)clsUser.enPermissions.pAll;

            Console.WriteLine("\nDo you want to give access to:");

            if (ReadYesNo("\nShow Client List? y/n? "))
                permissions |= (int)clsUser.enPermissions.pListClients;

            if (ReadYesNo("\nAdd New Client? y/n? "))
                permissions |= (int)clsUser.enPermissions.pAddNewClient;

            if (ReadYesNo("\nDelete Client? y/n? "))
                permissions |= (int)clsUser.enPermissions.pDeleteClient;

            if (ReadYesNo("\nUpdate Client? y/n? "))
                permissions |= (int)clsUser.enPermissions.pUpdateClient;

            if (ReadYesNo("\nFind Client? y/n? "))
                permissions |= (int)clsUser.enPermissions.pFindClient;

            if (ReadYesNo("\nTransactions? y/n? "))
                permissions |= (int)clsUser.enPermissions.pTransactions;

            if (ReadYesNo("\nManage Users? y/n? "))
                permissions |= (int)clsUser.enPermissions.pManageUsers;

            return permissions;
        }

        private static void ReadUserInfo(clsUser user)
        {
            Console.Write("\nEnter First Name: ");
            string firstName = Console.ReadLine()?.Trim() ?? "";

            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine()?.Trim() ?? "";

            Console.Write("Enter Email: ");
            string email = Console.ReadLine()?.Trim() ?? "";

            Console.Write("Enter Phone: ");
            string phone = Console.ReadLine()?.Trim() ?? "";

            Console.Write("Enter Password: ");
            string password = Console.ReadLine()?.Trim() ?? "";

            int permissions = ReadUserPermissions();

            user.UpdateUserInfo(firstName, lastName, email, phone, password, permissions);
        }

        private static void PrintUser(clsUser user)
        {
            Console.WriteLine("\nUser Card:");
            Console.WriteLine("___________________");
            Console.WriteLine($"FirstName   : {user.FirstName}");
            Console.WriteLine($"LastName    : {user.LastName}");
            Console.WriteLine($"FullName    : {user.FullName}");
            Console.WriteLine($"Email       : {user.Email}");
            Console.WriteLine($"Phone       : {user.Phone}");
            Console.WriteLine($"User Name   : {user.UserName}");
            Console.WriteLine($"Password    : {user.Password}");
            Console.WriteLine($"Permissions : {user.Permissions}");
            Console.WriteLine("___________________");
        }


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

            ReadUserInfo(newUser);

            clsUser.enSaveResults saveResult = newUser.Save();

            switch (saveResult)
            {
                case clsUser.enSaveResults.svSucceeded:
                    Console.WriteLine("\nUser Added Successfully :-)");
                    PrintUser(newUser);
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