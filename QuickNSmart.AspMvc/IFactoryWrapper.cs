//@QnSBaseCode
//MdStart
using QuickNSmart.Contracts;
using QuickNSmart.Contracts.Client;

namespace QuickNSmart.AspMvc
{
    public interface IFactoryWrapper
    {
        IAdapterAccess<I> Create<I>() where I : IIdentifiable;
        IAdapterAccess<I> Create<I>(string sessionToken) where I : IIdentifiable;
    }
}
//MdEnd