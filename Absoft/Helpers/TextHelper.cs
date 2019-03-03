using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Absoft.Helpers
{
    public static class TextHelper
    {
        public static string ToUnSign(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return "";
            }

            input = input.Trim();
            for (int i = 0x20; i < 0x30; i++)
            {
                input = input.Replace(((char)i).ToString(), " ");
            }
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string str = input.Normalize(NormalizationForm.FormD);
            string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
            while (str2.IndexOf("?") >= 0)
            {
                str2 = str2.Remove(str2.IndexOf("?"), 1);
            }
            return str2;
        }

        public static string ToTitleCase(this string title)
        {
            return Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(title.ToLower());
        }

        public static string ToTrim(this string value)
        {
            return Regex.Replace(value, @"\s+", " ").Trim();
        }

        public static string ToUpperFirstLetter(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1);

            return str.ToUpper();
        }

        public static string ToConvertFullDateFormat(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            var arr = value.Split("-");

            return $"{arr[2]}/{arr[1]}/{arr[0]}";
        }
    }
}
