using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SIS.IRunes.Services
{
    public class HashService : IHashSevice
    {
        public string Hash(string stringToHash)
        {
            var salt = "MySalt13458913$%";
            stringToHash += salt;

            using(var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(stringToHash));

                var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                return hash;
            }
        }
    }
}
