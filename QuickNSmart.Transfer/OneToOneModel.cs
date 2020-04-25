//@QnSBaseCode
//MdStart
using System.Text.Json.Serialization;

namespace QuickNSmart.Transfer
{
    public abstract partial class OneToOneModel<TFirst, TFirstModel, TSecond, TSecondModel> : IdentityModel
        where TFirst : Contracts.IIdentifiable
        where TSecond : Contracts.IIdentifiable
        where TFirstModel : IdentityModel, Contracts.ICopyable<TFirst>, TFirst, new()
        where TSecondModel : IdentityModel, Contracts.ICopyable<TSecond>, TSecond, new()
    {
        public virtual TFirstModel FirstModel { get; set; } = new TFirstModel();
        [JsonIgnore]
        public virtual TFirst FirstItem => FirstModel;

        public virtual TSecondModel SecondModel { get; set; } = new TSecondModel();
        [JsonIgnore]
        public virtual TSecond SecondItem => SecondModel;

        public override int Id { get => FirstModel.Id; set => FirstModel.Id = value; }
        public override byte[] Timestamp { get => FirstModel.Timestamp; set => FirstModel.Timestamp = value; }
    }
}
//MdEnd