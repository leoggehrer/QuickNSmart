//@QnSBaseCode
//MdStart
using CommonBase.Extensions;
using QuickNSmart.Adapters.Exceptions;
using QuickNSmart.Contracts.Business.Account;
using QuickNSmart.Logic.Controllers.Persistence.Account;
using QuickNSmart.Logic.Entities.Business.Account;
using QuickNSmart.Logic.Entities.Persistence.Account;
using QuickNSmart.Logic.Modules.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickNSmart.Logic.Controllers.Business.Account
{
    partial class LoginUserController
    {
        private UserController userController;
        private RoleController roleController;
        private UserXRoleController userXroleController;

        partial void Constructed()
        {
            userController = new UserController(this);
            roleController = new RoleController(this);
            userXroleController = new UserXRoleController(this);
        }

        public int MaxPageSize => userController.MaxPageSize;

        public Task<int> CountAsync()
        {
            return userController.CountAsync();
        }

        public Task<ILoginUser> CreateAsync()
        {
            return Task.Run<ILoginUser>(() => new LoginUser());
        }

        public async Task<ILoginUser> GetByIdAsync(int id)
        {
            var result = default(LoginUser);
            var user = await userController.GetByIdAsync(id);

            if (user != null)
            {
                result = new LoginUser();
                result.User.CopyProperties(user);
                foreach (var item in await userXroleController.QueryAsync(p => p.UserId == user.Id))
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

        public Task<IEnumerable<ILoginUser>> GetAllAsync()
        {
            return Task.Run<IEnumerable<ILoginUser>>(async () =>
            {
                List<ILoginUser> result = new List<ILoginUser>();

                foreach (var item in (await userController.GetAllAsync()).ToList())
                {
                    result.Add(await GetByIdAsync(item.Id));
                }
                return result;
            });
        }

        public Task<IEnumerable<ILoginUser>> GetPageListAsync(int pageIndex, int pageSize)
        {
            return Task.Run<IEnumerable<ILoginUser>>(async () =>
            {
                List<ILoginUser> result = new List<ILoginUser>();

                foreach (var item in (await userController.GetPageListAsync(pageIndex, pageSize)).ToList())
                {
                    result.Add(await GetByIdAsync(item.Id));
                }
                return result;
            });
        }

        public Task<IEnumerable<ILoginUser>> QueryPageListAsync(string predicate, int pageIndex, int pageSize)
        {
            return Task.Run<IEnumerable<ILoginUser>>(async () =>
            {
                List<ILoginUser> result = new List<ILoginUser>();

                foreach (var item in (await userController.QueryPageListAsync(predicate, pageIndex, pageSize)).ToList())
                {
                    result.Add(await GetByIdAsync(item.Id));
                }
                return result;
            });
        }

        public async Task<ILoginUser> InsertAsync(ILoginUser entity)
        {
            entity.CheckArgument(nameof(entity));
            entity.User.CheckArgument(nameof(entity.User));
            entity.Roles.CheckArgument(nameof(entity.Roles));

            var result = new LoginUser();

            result.UserEntity.CopyProperties(entity.User);
            result.UserEntity.PasswordHash = AccountManager.CalculateHash(result.UserEntity.Password);
            await userController.InsertAsync(result.UserEntity);
            foreach (var item in entity.Roles)
            {
                var role = new Role();
                var joinRole = new UserXRole();

                joinRole.User = result.UserEntity;
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
                await userXroleController.InsertAsync(joinRole);
                result.RoleEntities.Add(role);
            }
            return result;
        }

        public Task SaveChangesAsync()
        {
            return Context.SaveAsync();
        }

        public Task<ILoginUser> UpdateAsync(ILoginUser entity)
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

            userController.Dispose();
            roleController.Dispose();
            userXroleController.Dispose();

            userController = null;
            roleController = null;
            userXroleController = null;
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
//MdEnd