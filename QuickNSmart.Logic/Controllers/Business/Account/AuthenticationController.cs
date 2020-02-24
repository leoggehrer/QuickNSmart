//@QnSBaseCode
//MdStart
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonBase.Extensions;
using QuickNSmart.Adapters.Exceptions;
using QuickNSmart.Contracts.Business.Account;
using QuickNSmart.Logic.Controllers.Persistence.Account;
using QuickNSmart.Logic.Entities.Business.Account;
using QuickNSmart.Logic.Entities.Persistence.Account;
using QuickNSmart.Logic.Modules.Account;

namespace QuickNSmart.Logic.Controllers.Business.Account
{
    partial class AuthenticationController
    {
        private IdentityController identityController;
        private RoleController roleController;
        private IdentityXRoleController identityXroleController;

        partial void Constructed()
        {
            identityController = new IdentityController(this);
            roleController = new RoleController(this);
            identityXroleController = new IdentityXRoleController(this);
        }

        public int MaxPageSize => identityController.MaxPageSize;

        public Task<int> CountAsync()
        {
            return identityController.CountAsync();
        }

        public Task<IAuthentication> CreateAsync()
        {
            return Task.Run<IAuthentication>(() => new Authentication());
        }

        public async Task<IAuthentication> GetByIdAsync(int id)
        {
            var result = default(Authentication);
            var identity = await identityController.GetByIdAsync(id);

            if (identity != null)
            {
                result = new Authentication();
                result.Identity.CopyProperties(identity);
                foreach (var item in await identityXroleController.QueryAsync(p => p.IdentityId == identity.Id))
                {
                    var role = await roleController.GetByIdAsync(item.RoleId);

                    if (role != null)
                    {
                        var entity = new Role();

                        entity.CopyProperties(role);
                        result.RoleEntities.Add(entity);
                    }
                }
            }
            else
            {
                throw new LogicException(ErrorType.InvalidId, "Entity can't find!");
            }
            return result;
        }

        public Task<IEnumerable<IAuthentication>> GetAllAsync()
        {
            return Task.Run<IEnumerable<IAuthentication>>(async () =>
            {
                List<IAuthentication> result = new List<IAuthentication>();

                foreach (var item in (await identityController.GetAllAsync()).ToList())
                {
                    result.Add(await GetByIdAsync(item.Id));
                }
                return result;
            });
        }

        public Task<IEnumerable<IAuthentication>> GetPageListAsync(int pageIndex, int pageSize)
        {
            return Task.Run<IEnumerable<IAuthentication>>(async () =>
            {
                List<IAuthentication> result = new List<IAuthentication>();

                foreach (var item in (await identityController.GetPageListAsync(pageIndex, pageSize)).ToList())
                {
                    result.Add(await GetByIdAsync(item.Id));
                }
                return result;
            });
        }

        public Task<IEnumerable<IAuthentication>> QueryPageListAsync(string predicate, int pageIndex, int pageSize)
        {
            return Task.Run<IEnumerable<IAuthentication>>(async () =>
            {
                List<IAuthentication> result = new List<IAuthentication>();

                foreach (var item in (await identityController.QueryPageListAsync(predicate, pageIndex, pageSize)).ToList())
                {
                    result.Add(await GetByIdAsync(item.Id));
                }
                return result;
            });
        }

        public async Task<IAuthentication> InsertAsync(IAuthentication entity)
        {
            entity.CheckArgument(nameof(entity));
            entity.Identity.CheckArgument(nameof(entity.Identity));
            entity.Roles.CheckArgument(nameof(entity.Roles));

            var result = new Authentication();

            result.IdentityEntity.CopyProperties(entity.Identity);
            result.IdentityEntity.PasswordHash = AccountManager.CalculateHash(result.IdentityEntity.Password);
            await identityController.InsertAsync(result.IdentityEntity);
            foreach (var item in entity.Roles)
            {
                var role = new Role();
                var joinRole = new IdentityXRole();

                joinRole.Identity = result.IdentityEntity;
                if (item.Id == 0)
                {
                    item.Designation = ClearRoleDesigantion(item.Designation);

                    var qryItem = (await roleController.QueryAsync(e => e.Designation.Equals(item.Designation))).FirstOrDefault();

                    if (qryItem != null)
                    {
                        role.CopyProperties(qryItem);
                        joinRole.RoleId = role.Id;
                    }
                    else
                    {
                        role.CopyProperties(item);
                        await roleController.InsertAsync(role);
                        joinRole.Role = role;
                    }
                }
                else
                {
                    var qryItem = await roleController.GetByIdAsync(item.Id);

                    if (qryItem != null)
                    {
                        role.CopyProperties(qryItem);
                    }
                }
                await identityXroleController.InsertAsync(joinRole);
                result.RoleEntities.Add(role);
            }
            return result;
        }

        public Task SaveChangesAsync()
        {
            return Context.SaveAsync();
        }

        public Task<IAuthentication> UpdateAsync(IAuthentication entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            identityController.Dispose();
            roleController.Dispose();
            identityXroleController.Dispose();

            identityController = null;
            roleController = null;
            identityXroleController = null;
        }

        public static string ClearRoleDesigantion(string name)
        {
            StringBuilder result = new StringBuilder();

            if (name.HasContent())
            {
                foreach (var item in name)
                {
                    if (char.IsLetter(item) || char.IsDigit(item))
                    {
                        result.Append(result.Length == 0 ? char.ToUpper(item) : item);
                    }
                }
            }
            return result.ToString();
        }
    }
}
//MdENd