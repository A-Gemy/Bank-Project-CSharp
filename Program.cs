using Bank_Project_CSharp.Core;
using System;

namespace Bank_Project_CSharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            clsBankClient Client1 = clsBankClient.Find("A101");
            Client1.Print();

            clsBankClient Client2 = clsBankClient.Find("A103", "1234");
            Client2.Print();


            Console.ReadKey();
        }
    }
}
