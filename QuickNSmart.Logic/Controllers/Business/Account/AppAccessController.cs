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
    partial class AppAccessController
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

        public override int MaxPageSize => identityController.MaxPageSize;

        public override Task<int> CountAsync()
        {
            return identityController.CountAsync();
        }

        public override Task<IAppAccess> CreateAsync()
        {
            return Task.Run<IAppAccess>(() => new AppAccess());
        }

        public override async Task<IAppAccess> GetByIdAsync(int id)
        {
            var result = default(AppAccess);
            var identity = await identityController.GetByIdAsync(id).ConfigureAwait(false);

            if (identity != null)
            {
                result = new AppAccess();
                result.Identity.CopyProperties(identity);
                foreach (var item in (await identityXroleController.QueryAsync(p => p.IdentityId == identity.Id).ConfigureAwait(false)).ToList())
                {
                    var role = await roleController.GetByIdAsync(item.RoleId).ConfigureAwait(false);

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

        public override Task<IEnumerable<IAppAccess>> GetAllAsync()
        {
            return Task.Run<IEnumerable<IAppAccess>>(async () =>
            {
                List<IAppAccess> result = new List<IAppAccess>();

                foreach (var item in (await identityController.GetAllAsync().ConfigureAwait(false)).ToList())
                {
                    result.Add(await GetByIdAsync(item.Id));
                }
                return result;
            });
        }

        public override Task<IEnumerable<IAppAccess>> GetPageListAsync(int pageIndex, int pageSize)
        {
            return Task.Run<IEnumerable<IAppAccess>>(async () =>
            {
                List<IAppAccess> result = new List<IAppAccess>();

                foreach (var item in (await identityController.GetPageListAsync(pageIndex, pageSize).ConfigureAwait(false)).ToList())
                {
                    result.Add(await GetByIdAsync(item.Id));
                }
                return result;
            });
        }

        public override Task<IEnumerable<IAppAccess>> QueryPageListAsync(string predicate, int pageIndex, int pageSize)
        {
            return Task.Run<IEnumerable<IAppAccess>>(async () =>
            {
                List<IAppAccess> result = new List<IAppAccess>();

                foreach (var item in (await identityController.QueryPageListAsync(predicate, pageIndex, pageSize).ConfigureAwait(false)).ToList())
                {
                    result.Add(await GetByIdAsync(item.Id));
                }
                return result;
            });
        }

        public override async Task<IAppAccess> InsertAsync(IAppAccess entity)
        {
            entity.CheckArgument(nameof(entity));
            entity.Identity.CheckArgument(nameof(entity.Identity));
            entity.Roles.CheckArgument(nameof(entity.Roles));

            var result = new AppAccess();

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

                    var qryItem = (await roleController.QueryAsync(e => e.Designation.Equals(item.Designation)).ConfigureAwait(false)).FirstOrDefault();

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

        public override Task SaveChangesAsync()
        {
            return Context.SaveAsync();
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