//@QnSBaseCode
//MdStart

namespace QuickNSmart.AspMvc.Models
{
    public abstract partial class OneToOneModel<TFirst, TFirstModel, TSecond, TSecondModel> : IdentityModel
        where TFirst : Contracts.IIdentifiable
        where TSecond : Contracts.IIdentifiable
        where TFirstModel : IdentityModel, Contracts.ICopyable<TFirst>, TFirst, new()
        where TSecondModel : IdentityModel, Contracts.ICopyable<TSecond>, TSecond, new()
    {
        public virtual TFirstModel FirstModel { get; } = new TFirstModel();
        public virtual TFirst FirstItem => FirstModel;

        public virtual TSecondModel SecondEntity { get; } = new TSecondModel();
        public virtual TSecond SecondItem => SecondEntity;

        public override int Id { get => FirstModel.Id; set => FirstModel.Id = value; }
        public override byte[] RowVersion { get => FirstModel.RowVersion; set => FirstModel.RowVersion = value; }
    }
}
//MdEnd