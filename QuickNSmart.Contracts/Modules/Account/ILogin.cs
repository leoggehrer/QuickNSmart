//@QnSIgnore
using System;

namespace QuickNSmart.Contracts.Modules.Account
{
    public interface ILogin : ICopyable<ILogin>
    {
        public string Email { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }
    }
}
