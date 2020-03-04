//@QnSBaseCode
//MdStart
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using QuickNSmart.Logic.Entities.Persistence.Account;

namespace QuickNSmart.Logic.Controllers.Persistence.Account
{
    partial class LoginSessionController
    {
        protected override Task BeforeInsertingAsync(LoginSession entity)
        {
            entity.LoginTime = DateTime.Now;
            entity.LastAccess = entity.LoginTime;
            entity.SessionToken = Guid.NewGuid().ToString();
            entity.JsonWebToken = Modules.Security.JsonWebToken.GenerateToken(entity.Roles.Select(e => new Claim(ClaimTypes.Role, e.Designation)));
            return base.BeforeInsertingAsync(entity);
        }
    }
}
//MdEnd