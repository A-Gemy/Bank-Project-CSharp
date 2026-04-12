using Bank_Project_CSharp.Core;

namespace Bank_Project_CSharp
{
    internal static class Global
    {
        public static clsUser CurrentUser = clsUser.Find("", "");
    }
}
