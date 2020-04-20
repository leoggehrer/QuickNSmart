//@QnSBaseCode
//MdStart
using System.Collections.Generic;
using System.Linq;
using CommonBase.Extensions;

namespace QuickNSmart.Logic.Entities
{
    internal abstract partial class RelationObject<TMaster, TMasterEntity, TDetail, TDetailEntity> : IdentityObject
        where TMaster : Contracts.IIdentifiable
        where TDetail : Contracts.IIdentifiable
        where TMasterEntity : IdentityObject, Contracts.ICopyable<TMaster>, TMaster, new()
        where TDetailEntity : IdentityObject, Contracts.ICopyable<TDetail>, TDetail, new()
    {
        public virtual TMasterEntity MasterEntity { get; } = new TMasterEntity();
        public virtual TMaster Master => MasterEntity;

        public virtual List<TDetailEntity> DetailEntities { get; } = new List<TDetailEntity>();
        public virtual IEnumerable<TDetail> Details => DetailEntities as IEnumerable<TDetail>;

        public override int Id { get => MasterEntity.Id; set => MasterEntity.Id = value; }
        public override byte[] Timestamp { get => MasterEntity.Timestamp; set => MasterEntity.Timestamp = value; }

        public virtual void ClearDetails()
        {
            DetailEntities.Clear();
        }
        public virtual TDetail CreateDetail()
        {
            return new TDetailEntity();
        }
        public virtual void AddDetail(TDetail detail)
        {
            detail.CheckArgument(nameof(detail));

            var newDetail = new TDetailEntity();

            newDetail.CopyProperties(detail);
            DetailEntities.Add(newDetail);
        }
        public virtual void RemoveDetail(TDetail detail)
        {
            detail.CheckArgument(nameof(detail));

            var removeDetail = DetailEntities.FirstOrDefault(i => i.Id == detail.Id);

            if (removeDetail != null)
            {
                DetailEntities.Remove(removeDetail);
            }
        }
    }
}
//MdEnd