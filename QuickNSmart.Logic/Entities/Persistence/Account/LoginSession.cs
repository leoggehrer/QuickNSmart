﻿//@QnSBaseCode
//MdStart
using System;
using System.Collections.Generic;

namespace QuickNSmart.Logic.Entities.Persistence.Account
{
    partial class LoginSession
    {
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

                return ts.TotalSeconds > Logic.Modules.Security.Authorization.TimeOutInSec || LogoutTime.HasValue;
            }
        }
        internal bool HasChanged { get; private set; }

        internal string Email => Identity?.Email;
        internal byte[] PasswordHash => Identity?.PasswordHash;

        internal List<Role> Roles { get; } = new List<Role>();
        #endregion Ignore properties
    }
}
//MdEnd