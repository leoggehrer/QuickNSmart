namespace QuickNSmart.Connector.Contracts.Business.Account
{
	public partial interface IAppAccess : IIdentifiable, ICopyable<IAppAccess>
	{
		QuickNSmart.Contracts.Persistence.Account.IIdentity Identity
		{
			get;
		}
		System.Collections.Generic.IEnumerable<QuickNSmart.Contracts.Persistence.Account.IRole> Roles
		{
			get;
		}
	}
}
