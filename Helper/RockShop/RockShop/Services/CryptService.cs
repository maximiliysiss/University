﻿using System.Text;

namespace RockShop.Services
{
    /// <summary>
    /// Сервис шифрования
    /// </summary>
    public interface ICryptService
    {
        /// <summary>
        /// mD5 хеширование
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string CreateMD5(string input);
    }

    public class CryptService : ICryptService
    {
        public string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
