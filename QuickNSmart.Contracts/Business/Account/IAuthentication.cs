﻿//@QnSBaseCode
//MdStart
using System.Collections.Generic;
using QuickNSmart.Contracts.Persistence.Account;

namespace QuickNSmart.Contracts.Business.Account
{
    public partial interface IAuthentication : IIdentifiable, ICopyable<IAuthentication>
    {
        IIdentity Identity { get; }
        IEnumerable<IRole> Roles { get; }

        IRole CreateRole();
        void AddRole(IRole role);
        void RemoveRole(IRole role);
    }
}
//MdEnd