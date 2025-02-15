﻿using System.Security.Cryptography;
using System.Text;

namespace DotNet_E_Commerce_Glasses_Web.Utils
{
    /// <summary>
    /// Sử dụng MD5 để mã hóa mật khẩu
    /// </summary>
    public class PasswordSercurity
    {
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
                byte2String += targetData[i].ToString("x2");
            return byte2String;
        }
    }
}