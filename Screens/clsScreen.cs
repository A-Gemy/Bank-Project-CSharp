using System;

namespace Bank_Project_CSharp.Screens
{
    internal class clsScreen
    {
        protected static void DrawScreenHeader(string title, string subTitle = "", int width = 80)
        {
            string border = new string('=', width);

            Console.WriteLine();
            Console.WriteLine(border);
            Console.WriteLine(CenterText(title, width));

            if (!string.IsNullOrWhiteSpace(subTitle))
            {
                Console.WriteLine(CenterText(subTitle, width));
            }

            Console.WriteLine(border);
            Console.WriteLine();
        }

        protected static string CenterText(string text, int width)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            if (text.Length >= width)
                return text;

            int leftPadding = (width - text.Length) / 2;
            return new string(' ', leftPadding) + text;
        }

    }
}
