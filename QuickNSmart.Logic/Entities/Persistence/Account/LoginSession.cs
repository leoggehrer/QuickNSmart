using System;
using System.Collections.Generic;
using System.Text;

namespace QuickNSmart.Logic.Entities.Persistence.Account
{
    partial class LoginSession
    {
        #region Ignore properties
        internal bool IsActive => IsTimeout == false;
        internal bool IsTimeout
        {
            get
            {
                TimeSpan ts = DateTime.Now - LastAccess;

                return ts.TotalSeconds > 1800 || LogoutTime.HasValue;
            }
        }
        internal bool HasChanged { get; private set; }

        partial void OnLastAccessChanged()
        {
            HasChanged = true;
        }

        internal string Email { get; set; }
        internal byte[] PasswordHash { get; set; }

        internal List<Role> Roles { get; } = new List<Role>();
        #endregion Ignore properties
    }
}
