//@QnSBaseCode
//MdStart
using CommonBase.Extensions;
using QuickNSmart.Contracts.Business.Account;
using QuickNSmart.Contracts.Persistence.Account;
using QuickNSmart.Logic.Entities.Persistence.Account;
using System.Collections.Generic;

namespace QuickNSmart.Logic.Entities.Business.Account
{
    partial class LoginUser
    {
        internal User UserEntity { get; } = new User();
        partial void OnUserReading()
        {
            _user = UserEntity;
        }
        internal List<Role> RoleEntities { get; } = new List<Role>();
        partial void OnRolesReading()
        {
            _roles = RoleEntities;
        }
        public IRole CreateRole()
        {
            return new Role();
        }
        public void AddRole(IRole role)
        {
            role.CheckArgument(nameof(role));
            var newItem = new Role();

            newItem.CopyProperties(role);
            RoleEntities.Add(newItem);
        }
        public void RemoveRole(IRole role)
        {
            role.CheckArgument(nameof(role));

            foreach (var item in RoleEntities)
            {
                if (item.Id != 0 && item.Id == role.Id)
                {
                    RoleEntities.Remove(item);
                }
                else if (item.Description != null && item.Description.Equals(role.Description, System.StringComparison.CurrentCultureIgnoreCase))
                {
                    RoleEntities.Remove(item);
                }
            }
        }
        partial void BeforeCopyProperties(ILoginUser other, ref bool handled)
        {
            other.CheckArgument(nameof(other));
            other.User.CheckArgument(nameof(other.User));
            other.Roles.CheckArgument(nameof(other.Roles));

            handled = true;
            UserEntity.CopyProperties(other.User);
            RoleEntities.Clear();
            foreach (var item in other.Roles)
            {
                var newItem = new Role();

                newItem.CopyProperties(item);
                RoleEntities.Add(newItem);
            }
        }
    }
}
//MdEnd