using Bank_Project_CSharp.Screens;

namespace Bank_Project_CSharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool runApp = true;

            while (runApp)
            {
                runApp = clsLoginScreen.ShowLoginScreen();
            }
        }
    }
}
