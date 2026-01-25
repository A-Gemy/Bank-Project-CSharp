namespace Bank_Project_CSharp.Core
{
    internal abstract class clsPerson
    {
        protected string _FirstName;
        protected string _LastName;
        protected string _Email;
        protected string _Phone;


        public string FirstName
        {
            get => _FirstName;
            protected set => _FirstName = value;
        }
        public string LastName
        {
            get => _LastName;
            protected set => _LastName = value;
        }
        public string Email
        {
            get => _Email;
            protected set => _Email = value;
        }
        public string Phone
        {
            get => _Phone;
            protected set => _Phone = value;
        }

        public string FullName => $"{FirstName} {LastName}";


        protected clsPerson(string firstName, string lastName, string email, string phone)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
        }

    }
}
