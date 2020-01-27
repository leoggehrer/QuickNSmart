//@QnSBaseCode
//MdStart
using QuickNSmart.Logic.DataContext;

namespace QuickNSmart.Logic.Controllers.Persistence
{
    internal abstract partial class QuickNSmartController<I, E> : GenericController<I, E>
       where I : Contracts.IIdentifiable
       where E : Entities.IdentityObject, I, Contracts.ICopyable<I>, new()
    {
        internal IQuickNSmartContext QuickNSmartContext => (IQuickNSmartContext)Context;

        protected QuickNSmartController(IContext context)
            : base(context)
        {
        }
        protected QuickNSmartController(ControllerObject controller)
            : base(controller)
        {
        }
    }
}
//MdEnd
