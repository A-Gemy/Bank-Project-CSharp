using System;
using System.Collections.Generic;
using System.IO;

namespace Bank_Project_CSharp.Core
{
    internal class clsUser : clsPerson
    {

        #region Constants & Enums

        private static readonly string _FileName =
            Path.Combine(AppContext.BaseDirectory, "Core", "Users.txt");

        private static readonly string _LoginRegisterFileName =
            Path.Combine(AppContext.BaseDirectory, "Core", "LoginRegister.txt");

        private const string _Separator = "#//#";

        internal enum enMode { EmptyMode = 0, UpdateMode = 1, AddNewMode = 2 }

        internal enum enSaveResults { svFailedEmptyObject = 0, svSucceeded = 1, svFailedUserNameExists = 2 }

        internal enum enPermissions
        {
            pAll = -1,
            pListClients = 1,
            pAddNewClient = 2,
            pDeleteClient = 4,
            pUpdateClient = 8,
            pFindClient = 16,
            pTransactions = 32,
            pManageUsers = 64,
        }

        #endregion


        #region Private Fields

        private enMode _Mode;
        private string _UserName;
        private string _Password;
        private int _Permissions;

        #endregion


        #region Properties

        public string UserName => _UserName;

        public string Password
        {
            get => _Password;
            protected set => _Password = value;
        }

        public int Permissions
        {
            get => _Permissions;
            protected set => _Permissions = value;
        }

        protected bool IsEmpty => _Mode == enMode.EmptyMode;
        public bool IsEmptyUser => IsEmpty;

        #endregion


        #region Constructor

        internal clsUser(
            enMode mode,
            string firstName,
            string lastName,
            string email,
            string phone,
            string userName,
            string password,
            int permissions)
            : base(firstName, lastName, email, phone)
        {
            _Mode = mode;
            _UserName = userName;
            _Password = password;
            _Permissions = permissions;
        }

        #endregion


        #region Private Helper Methods (Internal Logic)

        private static clsUser _ConvertLineToUserObject(string line)
        {
            if (string.IsNullOrEmpty(line))
                return _GetEmptyUserObject();

            string[] userData = line.Split(new string[] { _Separator }, StringSplitOptions.None);

            if (userData.Length != 7)
                return _GetEmptyUserObject();

            userData = Array.ConvertAll(userData, d => d.Trim());

            if (!int.TryParse(userData[6], out int permissions))
                permissions = 0;

            return new clsUser(
                enMode.UpdateMode,
                userData[0],  // FirstName
                userData[1],  // LastName
                userData[2],  // Email
                userData[3],  // Phone
                userData[4],  // UserName
                userData[5],  // Password
                permissions
                );
        }

        private static clsUser _GetEmptyUserObject()
        {
            return new clsUser(
                enMode.EmptyMode,
                "", "", "", "", "", "", 0
             );
        }

        private static List<clsUser> _LoadAllUsers()
        {
            string folder = Path.GetDirectoryName(_FileName);
            if (!string.IsNullOrEmpty(folder))
            {
                Directory.CreateDirectory(folder);
            }

            List<clsUser> users = new List<clsUser>();

            if (!File.Exists(_FileName))
                return users;

            string[] lines = File.ReadAllLines(_FileName);
            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    clsUser user = _ConvertLineToUserObject(line);
                    if (!user.IsEmptyUser)
                    {
                        users.Add(user);
                    }
                }
            }

            return users;
        }

        private static List<string[]> _LoadLoginRegisterList()
        {
            List<string[]> loginRegisters = new List<string[]>();

            if (!File.Exists(_LoginRegisterFileName))
                return loginRegisters;

            string[] lines = File.ReadAllLines(_LoginRegisterFileName);

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string[] loginData = line.Split(new string[] { _Separator }, StringSplitOptions.None);

                if (loginData.Length == 3)
                    loginRegisters.Add(loginData);
            }
            return loginRegisters;
        }

        private string _ConvertUserToLine()
        {
            return string.Join(_Separator,
                FirstName,
                LastName,
                Email,
                Phone,
                UserName,
                Password,
                Permissions.ToString()
            );
        }

        private string _ConvertLoginRegisterLine()
        {
            return string.Join(_Separator,
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                UserName,
                Permissions.ToString()
                );
        }

        private void _SaveUsersDataToFile(List<clsUser> users)
        {
            string folder = Path.GetDirectoryName(_FileName);
            if (!string.IsNullOrEmpty(folder))
            {
                Directory.CreateDirectory(folder);
            }

            using (StreamWriter writer = new StreamWriter(_FileName, false))
            {
                foreach (clsUser user in users)
                {
                    writer.WriteLine(user._ConvertUserToLine());
                }
            }
        }

        private void _Update()
        {
            List<clsUser> users = _LoadAllUsers();

            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].UserName == this.UserName)
                {
                    users[i] = this;
                    break;
                }
            }

            _SaveUsersDataToFile(users);
        }

        private void _AddNew()
        {
            string folder = Path.GetDirectoryName(_FileName);
            if (!string.IsNullOrEmpty(folder))
            {
                Directory.CreateDirectory(folder);
            }

            using (StreamWriter writer = new StreamWriter(_FileName, true))
            {
                writer.WriteLine(_ConvertUserToLine());
            }
        }

        private void _Reset()
        {
            _Mode = enMode.EmptyMode;
            _FirstName = "";
            _LastName = "";
            _Email = "";
            _Phone = "";
            _UserName = "";
            _Password = "";
            _Permissions = 0;
        }

        #endregion


        #region Public Static Methods (Factory / Queries)

        public static clsUser Find(string userName)
        {
            List<clsUser> users = _LoadAllUsers();

            foreach (clsUser user in users)
            {
                if (user.UserName == userName)
                    return user;
            }

            return _GetEmptyUserObject();
        }

        public static clsUser Find(string userName, string password)
        {
            List<clsUser> users = _LoadAllUsers();

            foreach (clsUser user in users)
            {
                if (user.UserName == userName && user.Password == password)
                    return user;
            }

            return _GetEmptyUserObject();
        }

        public static bool IsUserExist(string userName)
        {
            return !Find(userName).IsEmptyUser;
        }

        public static clsUser GetAddNewUserObject(string userName)
        {
            return new clsUser(
                enMode.AddNewMode,
                "", "", "", "", userName, "", 0
            );
        }

        public static List<clsUser> GetUsersList()
        {
            return _LoadAllUsers();
        }

        public static List<string[]> GetLoginRegisterList()
        {
            return _LoadLoginRegisterList();
        }

        #endregion


        #region Public Instance Methods

        public void Print()
        {
            Console.WriteLine("\nUser Card:");
            Console.WriteLine("___________________");
            Console.WriteLine($"FirstName   : {FirstName}");
            Console.WriteLine($"LastName    : {LastName}");
            Console.WriteLine($"FullName    : {FullName}");
            Console.WriteLine($"Email       : {Email}");
            Console.WriteLine($"Phone       : {Phone}");
            Console.WriteLine($"User Name   : {UserName}");
            Console.WriteLine($"Permissions : {Permissions}");
            Console.WriteLine("___________________");
        }

        public enSaveResults Save()
        {
            switch (_Mode)
            {
                case enMode.EmptyMode:
                    {
                        return enSaveResults.svFailedEmptyObject;
                    }

                case enMode.UpdateMode:
                    {
                        _Update();
                        return enSaveResults.svSucceeded;
                    }

                case enMode.AddNewMode:
                    {
                        if (IsUserExist(_UserName))
                            return enSaveResults.svFailedUserNameExists;
                        else
                        {
                            _AddNew();
                            _Mode = enMode.UpdateMode;
                            return enSaveResults.svSucceeded;
                        }
                    }
            }

            return enSaveResults.svFailedEmptyObject;
        }

        public bool Delete()
        {
            List<clsUser> users = _LoadAllUsers();
            List<clsUser> updatedUsers = new List<clsUser>();
            bool found = false;

            foreach (clsUser user in users)
            {
                if (user.UserName == _UserName)
                {
                    found = true;
                    continue;
                }

                updatedUsers.Add(user);
            }

            if (!found)
                return false;

            _SaveUsersDataToFile(updatedUsers);
            _Reset();

            return true;
        }

        public bool CheckAccessPermission(enPermissions permissions)
        {
            if (Permissions == (int)enPermissions.pAll)
                return true;

            return ((int)permissions & Permissions) == (int)permissions;
        }

        public void RegisterLogin()
        {
            string folder = Path.GetDirectoryName(_LoginRegisterFileName);
            if (!string.IsNullOrEmpty(folder))
            {
                Directory.CreateDirectory(folder);
            }

            using (StreamWriter sw = new StreamWriter(_LoginRegisterFileName, true))
            {
                sw.WriteLine(_ConvertLoginRegisterLine());
            }
        }

        #endregion


        #region Internal Update Methods

        internal void UpdateUserInfo(string firstName, string lastName, string email, string phone, string password, int permissions)
        {
            _FirstName = firstName;
            _LastName = lastName;
            _Email = email;
            _Phone = phone;
            _Password = password;
            _Permissions = permissions;
        }

        #endregion

    }
}
