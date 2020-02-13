//@QnSBaseCode
//MdStart
using QuickNSmart.Contracts.Business.Account;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuickNSmart.Logic.Controllers.Business.Account
{
    partial class LoginUserController
    {
        public int MaxPageSize => throw new NotImplementedException();

        public Task<int> CountAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ILoginUser> CreateAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ILoginUser>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ILoginUser> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ILoginUser>> GetPageListAsync(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<ILoginUser> InsertAsync(ILoginUser entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ILoginUser>> QueryPageListAsync(string predicate, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ILoginUser> UpdateAsync(ILoginUser entity)
        {
            throw new NotImplementedException();
        }
    }
}
//MdEnd