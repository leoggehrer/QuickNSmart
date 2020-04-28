//@QnSBaseCode
//MdStart
using QuickNSmart.Contracts.Persistence.Account;
using System;
using System.Collections.Generic;

namespace QuickNSmart.Logic.Entities.Persistence.Account
{
    partial class LoginSession
    {
        #region Identity members
        partial void OnNameReading()
        {
            if (Identity != null)
            {
                _name = Identity.Name;
            }
        }
        partial void OnEmailReading()
        {
            if (Identity != null)
            {
                _email = Identity.Email;
            }
        }
        partial void OnIdentityIdReading()
        {
            if (Identity != null)
            {
                _identityId = Identity.Id;
            }
        }
        private byte[] passwordHash;
        internal byte[] PasswordHash
        {
            get
            {
                if (Identity != null)
                {
                    passwordHash = Identity.PasswordHash;
                }
                return passwordHash;
            }
            set
            {
                passwordHash = value;
            }
        }
        private byte[] passwordSalt;
        internal byte[] PasswordSalt
        {
            get
            {
                if (Identity != null)
                {
                    passwordSalt = Identity.PasswordSalt;
                }
                return passwordSalt;
            }
            set
            {
                passwordSalt = value;
            }
        }
        #endregion Identity members

        partial void OnOriginReading()
        {
            if (_origin == null)
            {
                _origin = nameof(QuickNSmart);
            }
        }
        partial void OnLastAccessChanged()
        {
            HasChanged = true;
        }

        #region Ignore properties
        internal bool IsActive => IsTimeout == false;
        internal bool IsTimeout
        {
            get
            {
                TimeSpan ts = DateTime.Now - LastAccess;

                return LogoutTime.HasValue || ts.TotalSeconds > Logic.Modules.Security.Authorization.TimeOutInSec;
            }
        }
        internal bool HasChanged { get; set; }
        internal List<Role> Roles { get; } = new List<Role>();
        #endregion Ignore properties

        partial void AfterCopyProperties(ILoginSession other)
        {
            if (other is LoginSession loginSession)
            {
                PasswordHash = loginSession.PasswordHash;
                PasswordSalt = loginSession.PasswordSalt;

                Roles.Clear();
                Roles.AddRange(loginSession.Roles);
            }
        }
    }
}
//MdEnd