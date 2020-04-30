//@QnSBaseCode
//MdStart
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using CommonBase.Extensions;

namespace QuickNSmart.Transfer
{
    public abstract partial class OneToManyModel<TFirst, TFirstModel, TSecond, TSecondModel> : IdentityModel
        where TFirst : Contracts.IIdentifiable
        where TSecond : Contracts.IIdentifiable
        where TFirstModel : IdentityModel, Contracts.ICopyable<TFirst>, TFirst, new()
        where TSecondModel : IdentityModel, Contracts.ICopyable<TSecond>, TSecond, new()
    {
        public virtual TFirstModel FirstModel { get; set; } = new TFirstModel();
        [JsonIgnore]
        public virtual TFirst FirstItem => FirstModel;

        public virtual List<TSecondModel> SecondModels { get; set; } = new List<TSecondModel>();
        [JsonIgnore]
        public virtual IEnumerable<TSecond> SecondItems => SecondModels as IEnumerable<TSecond>;

        public override int Id { get => FirstModel.Id; set => FirstModel.Id = value; }
        public override byte[] RowVersion { get => FirstModel.RowVersion; set => FirstModel.RowVersion = value; }

        public virtual void ClearSecondItems()
        {
            SecondModels.Clear();
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
            SecondModels.Add(newDetail);
        }
        public virtual void RemoveSecondItem(TSecond secondItem)
        {
            secondItem.CheckArgument(nameof(secondItem));

            var removeDetail = SecondModels.FirstOrDefault(i => i.Id == secondItem.Id);

            if (removeDetail != null)
            {
                SecondModels.Remove(removeDetail);
            }
        }
    }
}
//MdEnd