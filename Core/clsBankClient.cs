using System;
using System.Collections.Generic;
using System.IO;

namespace Bank_Project_CSharp.Core
{
    internal class clsBankClient : clsPerson
    {

        #region Constants & Enums

        private static readonly string _FileName =
            Path.Combine(AppContext.BaseDirectory, "Core", "Clients.txt");
        private const string _Separator = "#//#";

        internal enum enMode { EmptyMode = 0, UpdateMode = 1 };

        #endregion


        #region Private Fields

        private enMode _Mode;
        private string _AccountNumber;
        private string _PinCode;
        private decimal _AccountBalance;

        #endregion


        #region Properties

        public string AccountNumber => _AccountNumber;

        public string PinCode
        {
            get => _PinCode;
            protected set => _PinCode = value;
        }

        public decimal AccountBalance
        {
            get => _AccountBalance;
            protected set => _AccountBalance = value;
        }

        protected bool IsEmpty => _Mode == enMode.EmptyMode;
        public bool IsEmptyClient => IsEmpty;

        #endregion


        #region Constructor

        internal clsBankClient(
            enMode mode,
            string firstName,
            string lastName,
            string email,
            string phone,
            string accountNumber,
            string pinCode,
            decimal accountBalance)
            : base(firstName, lastName, email, phone)
        {
            _Mode = mode;
            _AccountNumber = accountNumber;
            _PinCode = pinCode;
            _AccountBalance = accountBalance;
        }

        #endregion


        #region Private Helper Methods (Internal Logic)

        private static clsBankClient _ConvertLineToClientObject(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
                return _GetEmptyClientObject();

            string[] clientData = line.Split(new string[] { _Separator }, StringSplitOptions.None);

            if (clientData.Length != 7)
                return _GetEmptyClientObject();

            // Trim empty spaces if any
            clientData = Array.ConvertAll(clientData, d => d.Trim());

            if (!decimal.TryParse(clientData[6], out decimal balance))
            {
                balance = 0m;
            }

            return new clsBankClient(
                enMode.UpdateMode,
                clientData[0],  // FirstName
                clientData[1],  // LastName
                clientData[2],  // Email
                clientData[3],  // Phone
                clientData[4],  // AccountNumber
                clientData[5],  // PinCode
                balance
                );
        }

        private static clsBankClient _GetEmptyClientObject()
        {
            return new clsBankClient(
                enMode.EmptyMode,
                "", "", "", "", "", "", 0m
            );
        }

        private static List<clsBankClient> _LoadAllClients()
        {
            string folder = Path.GetDirectoryName(_FileName);
            if (!string.IsNullOrEmpty(folder))
            {
                Directory.CreateDirectory(folder);
            }

            List<clsBankClient> clients = new List<clsBankClient>();

            if (!File.Exists(_FileName))
                return clients;

            foreach (string line in File.ReadAllLines(_FileName))
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    clients.Add(_ConvertLineToClientObject(line));
                }
            }

            return clients;
        }

        private string _ConvertClientToLine()
        {
            return string.Join(_Separator,
                FirstName,
                LastName,
                Email,
                Phone,
                AccountNumber,
                PinCode,
                AccountBalance.ToString()
            );
        }

        private void _SaveClientsDataToFile(List<clsBankClient> clients)
        {
            string folder = Path.GetDirectoryName(_FileName);
            if (!string.IsNullOrEmpty(folder))
            {
                Directory.CreateDirectory(folder);
            }

            using (StreamWriter writer = new StreamWriter(_FileName, false))
            {
                foreach (clsBankClient client in clients)
                {
                    writer.WriteLine(client._ConvertClientToLine());
                }
            }

        }

        private void _Update()
        {
            List<clsBankClient> _clients = _LoadAllClients();

            for (int i = 0; i < _clients.Count; i++)
            {
                if (_clients[i].AccountNumber == this.AccountNumber)
                {
                    _clients[i] = this;
                    break;
                }
            }

            _SaveClientsDataToFile(_clients);
        }

        #endregion


        #region Public Static Methods (Factory / Queries)

        public static clsBankClient Find(string accountNumber)
        {
            List<clsBankClient> clients = _LoadAllClients();

            foreach (clsBankClient client in clients)
            {
                if (client.AccountNumber == accountNumber)
                    return client;
            }

            return _GetEmptyClientObject();
        }

        public static clsBankClient Find(string accountNumber, string pinCode)
        {
            List<clsBankClient> clients = _LoadAllClients();

            foreach (clsBankClient client in clients)
            {
                if (client.AccountNumber == accountNumber &&
                    client.PinCode == pinCode)
                {
                    return client;
                }
            }

            return _GetEmptyClientObject();
        }

        public static bool IsClientExist(string accountNumber)
        {
            return !Find(accountNumber).IsEmptyClient;
        }

        #endregion


        #region Public Instance Methods

        public void Print()
        {
            Console.WriteLine("\nClient Card:");
            Console.WriteLine("___________________");
            Console.WriteLine($"FirstName   : {FirstName}");
            Console.WriteLine($"LastName    : {LastName}");
            Console.WriteLine($"FullName    : {FullName}");
            Console.WriteLine($"Email       : {Email}");
            Console.WriteLine($"Phone       : {Phone}");
            Console.WriteLine($"Acc. Number : {AccountNumber}");
            Console.WriteLine($"Balance     : {AccountBalance:C}");
            Console.WriteLine("___________________");
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.EmptyMode:
                    {
                        return false;
                    }

                case enMode.UpdateMode:
                    {
                        _Update();
                        return true;
                    }

            }
            return false;
        }

        #endregion


        #region Internal Update Methods

        internal void UpdateClientInfo(string firstName, string lastName, string email, string phone, string pinCode, decimal balance)
        {
            _FirstName = firstName;
            _LastName = lastName;
            _Email = email;
            _Phone = phone;
            _PinCode = pinCode;
            _AccountBalance = balance;
        }

        #endregion


    }
}
