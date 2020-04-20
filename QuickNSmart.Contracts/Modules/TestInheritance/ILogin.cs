//@QnSIgnore
using System;

namespace QuickNSmart.Contracts.Modules.TestInheritance
{
    public interface ILogin : ICopyable<ILogin>
    {
        public string Email { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }
    }
}
