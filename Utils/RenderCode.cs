using System;
using System.Linq;

namespace DotNet_E_Commerce_Glasses_Web.Utils
{
    public class RenderCode
    {
        public static string Code()
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
            int length = 8;
            Random random = new Random();

            string code = new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
            return code;
        }
    }
}