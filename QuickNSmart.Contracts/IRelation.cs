//@QnSBaseCode
//MdStart
using System.Collections.Generic;

namespace QuickNSmart.Contracts
{
    public partial interface IRelation<TMaster, TDetail> : IIdentifiable
        where TMaster : IIdentifiable
        where TDetail : IIdentifiable
    {
        TMaster Master { get; }
        IEnumerable<TDetail> Details { get; }

        void ClearDetails();
        TDetail CreateDetail();
        void AddDetail(TDetail detail);
        void RemoveDetail(TDetail detail);
    }
}
//MdEnd