﻿using System.Text;

namespace AuthAPI.Services
{
    /// <summary>
    /// Сервис для шифрования
    /// </summary>
    public interface ICryptService
    {
        /// <summary>
        /// Получить хэш
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        string CreateHash(string data);
    }

    public class CryptService : ICryptService
    {
        public string CreateHash(string data)
        {
            using System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();

            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(data);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
                sb.Append(hashBytes[i].ToString("X2"));
            return sb.ToString();
        }
    }
}
