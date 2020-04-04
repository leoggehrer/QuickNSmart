using System;
using System.Collections.Generic;
using System.Text;

namespace QuickNSmart.Contracts.Modules.Account
{
    public interface ILoginUser : ILogin, ICopyable<ILoginUser>
    {
        public string Name { get; set; }
        public Contracts.Modules.Common.State State { get; set; }
    }
}
