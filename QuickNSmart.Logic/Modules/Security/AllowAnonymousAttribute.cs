//@QnSBaseCode
//MdStart
using System;

namespace QuickNSmart.Logic.Modules.Security
{
    [AttributeUsage(AttributeTargets.Method)]
    internal partial class AllowAnonymousAttribute : AuthorizeAttribute
    {
        public AllowAnonymousAttribute()
            : base(false)
        {

        }
    }
}
//MdEnd