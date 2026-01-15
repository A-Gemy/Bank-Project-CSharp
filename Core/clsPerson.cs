namespace Bank_Project_CSharp.Core
{
    internal class clsPerson
    {
        private string _FirstName { get; set; }
        private string _LastName { get; set; }
        private string _Email { get; set; }
        private string _Phone { get; set; }


        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public string FullName
        {
            get => _FirstName + " " + _LastName;
        }


        clsPerson(string firstName, string lastName, string email, string phone)
        {
            _FirstName = firstName;
            _LastName = lastName;
            _Email = email;
            _Phone = phone;
        }

    }
}
