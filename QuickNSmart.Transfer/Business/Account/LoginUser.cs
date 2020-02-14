//@QnSBaseCode
//MdStart
using CommonBase.Extensions;
using QuickNSmart.Contracts.Persistence.Account;
using QuickNSmart.Transfer.Persistence.Account;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace QuickNSmart.Transfer.Business.Account
{
    partial class LoginUser
    {
        [JsonPropertyName(nameof(User))]
        public User UserEntity { get; } = new User();
        partial void OnUserReading()
        {
            _user = UserEntity;
        }
        [JsonPropertyName(nameof(Roles))]
        public List<Role> RoleEntities { get; } = new List<Role>();
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
    }
}
//MdEnd