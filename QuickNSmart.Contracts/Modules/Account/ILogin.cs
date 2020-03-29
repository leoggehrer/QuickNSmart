using System;
using System.Collections.Generic;
using System.Text;

namespace QuickNSmart.Contracts.Modules.Account
{
    public interface ILogin
    {
        public string Email { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }
    }
}
