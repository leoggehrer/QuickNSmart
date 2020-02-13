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

    }
}
//MdEnd