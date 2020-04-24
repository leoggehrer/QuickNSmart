//@QnSBaseCode
//MdStart
using System.Collections.Generic;
using System.Linq;
using CommonBase.Extensions;

namespace QuickNSmart.AspMvc.Models
{
    public abstract partial class OneToManyModel<TFirst, TFirstModel, TSecond, TSecondModel> : IdentityModel
        where TFirst : Contracts.IIdentifiable
        where TSecond : Contracts.IIdentifiable
        where TFirstModel : IdentityModel, Contracts.ICopyable<TFirst>, TFirst, new()
        where TSecondModel : IdentityModel, Contracts.ICopyable<TSecond>, TSecond, new()
    {
        public virtual TFirstModel FirstModel { get; } = new TFirstModel();
        public virtual TFirst FirstItem => FirstModel;

        public virtual List<TSecondModel> SecondEntities { get; } = new List<TSecondModel>();
        public virtual IEnumerable<TSecond> SecondItems => SecondEntities as IEnumerable<TSecond>;

        public override int Id { get => FirstModel.Id; set => FirstModel.Id = value; }
        public override byte[] Timestamp { get => FirstModel.Timestamp; set => FirstModel.Timestamp = value; }

        public virtual void ClearSecondItems()
        {
            SecondEntities.Clear();
        }
        public virtual TSecond CreateSecondItem()
        {
            return new TSecondModel();
        }
        public virtual void AddSecondItem(TSecond secondItem)
        {
            secondItem.CheckArgument(nameof(secondItem));

            var newDetail = new TSecondModel();

            newDetail.CopyProperties(secondItem);
            SecondEntities.Add(newDetail);
        }
        public virtual void RemoveSecondItem(TSecond secondItem)
        {
            secondItem.CheckArgument(nameof(secondItem));

            var removeDetail = SecondEntities.FirstOrDefault(i => i.Id == secondItem.Id);

            if (removeDetail != null)
            {
                SecondEntities.Remove(removeDetail);
            }
        }
    }
}
//MdEnd