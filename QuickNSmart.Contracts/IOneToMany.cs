//@QnSBaseCode
//MdStart
using System.Collections.Generic;

namespace QuickNSmart.Contracts
{
    public partial interface IOneToMany<TFirst, TSecond> : IIdentifiable
        where TFirst : IIdentifiable
        where TSecond : IIdentifiable
    {
        TFirst FirstItem { get; }
        IEnumerable<TSecond> SecondItems { get; }

        void ClearSecondItems();
        TSecond CreateSecondItem();
        void AddSecondItem(TSecond secondItem);
        void RemoveSecondItem(TSecond secondItem);
    }
}
//MdEnd