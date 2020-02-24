//@QnSBaseCode
//MdStart
using System.Collections.Generic;
using System.Text.Json.Serialization;
using CommonBase.Extensions;
using QuickNSmart.Contracts.Persistence.Account;
using QuickNSmart.Transfer.Persistence.Account;

namespace QuickNSmart.Transfer.Business.Account
{
    partial class Authentication
    {
        [JsonPropertyName(nameof(Identity))]
        public Identity IdentityEntity { get; } = new Identity();
        partial void OnIdentityReading()
        {
            _identity = IdentityEntity;
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