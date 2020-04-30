//@QnSBaseCode
//MdStart
using System.Collections.Generic;
using System.Linq;
using CommonBase.Extensions;

namespace QuickNSmart.Logic.Entities
{
    internal abstract partial class OneToManyObject<TFirst, TFirstEntity, TSecond, TSecondEntity> : IdentityObject
        where TFirst : Contracts.IIdentifiable
        where TSecond : Contracts.IIdentifiable
        where TFirstEntity : IdentityObject, Contracts.ICopyable<TFirst>, TFirst, new()
        where TSecondEntity : IdentityObject, Contracts.ICopyable<TSecond>, TSecond, new()
    {
        public virtual TFirstEntity FirstEntity { get; } = new TFirstEntity();
        public virtual TFirst FirstItem => FirstEntity;

        public virtual List<TSecondEntity> SecondEntities { get; } = new List<TSecondEntity>();
        public virtual IEnumerable<TSecond> SecondItems => SecondEntities as IEnumerable<TSecond>;

        public override int Id { get => FirstEntity.Id; set => FirstEntity.Id = value; }
        public override byte[] RowVersion { get => FirstEntity.RowVersion; set => FirstEntity.RowVersion = value; }

        public virtual void ClearSecondItems()
        {
            SecondEntities.Clear();
        }
        public virtual TSecond CreateSecondItem()
        {
            return new TSecondEntity();
        }
        public virtual void AddSecondItem(TSecond secondItem)
        {
            secondItem.CheckArgument(nameof(secondItem));

            var newSecond = new TSecondEntity();

            newSecond.CopyProperties(secondItem);
            SecondEntities.Add(newSecond);
        }
        public virtual void RemoveSecondItem(TSecond secondItem)
        {
            secondItem.CheckArgument(nameof(secondItem));

            var removeSecond = SecondEntities.FirstOrDefault(i => i.Id == secondItem.Id);

            if (removeSecond != null)
            {
                SecondEntities.Remove(removeSecond);
            }
        }
    }
}
//MdEnd