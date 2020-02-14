//@QnSBaseCode
//MdStart
using QuickNSmart.Logic.Entities.Persistence.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickNSmart.Logic.Modules.Account
{
    public static class AccountManager
    {
        static AccountManager()
        {
            if (string.IsNullOrEmpty(SystemAuthorizationToken))
            {
                SystemAuthorizationToken = Guid.NewGuid().ToString();
            }
        }

        private const int UpdateDelay = 60000;

        private static List<LoginSession> LoginSessions { get; } = new List<LoginSession>();
        internal static string SystemAuthorizationToken { get; set; }

        internal static byte[] CalculateHash(string plainText)
        {
            if (String.IsNullOrEmpty(plainText))
                throw new ArgumentNullException(nameof(plainText));

            System.Security.Cryptography.SHA1 sha1 = System.Security.Cryptography.SHA1.Create();
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] hashedBytes = sha1.ComputeHash(plainTextBytes);
            return hashedBytes;
        }
    }
}
//MdEnd