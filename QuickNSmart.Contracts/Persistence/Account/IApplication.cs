//@TestCode
//MdStart
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickNSmart.Contracts.Persistence.Account
{
    public partial interface IApplication : IIdentifiable, ICopyable<IApplication>
    {
        string Name { get; set; }
        string Token { get; set; }
        State State { get; set; }
    }
}
//MdEnd