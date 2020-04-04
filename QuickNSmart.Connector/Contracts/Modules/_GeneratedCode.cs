namespace QuickNSmart.Connector.Contracts.Modules.Account
{
	public partial interface ILogin
	{
		System.String Email
		{
			get;
			set;
		}
		System.DateTime LoginTime
		{
			get;
			set;
		}
		System.DateTime? LogoutTime
		{
			get;
			set;
		}
	}
}
namespace QuickNSmart.Connector.Contracts.Modules.Account
{
	public partial interface ILoginUser
	{
		System.String Email
		{
			get;
			set;
		}
		System.DateTime LoginTime
		{
			get;
			set;
		}
		System.DateTime? LogoutTime
		{
			get;
			set;
		}
		System.String Name
		{
			get;
			set;
		}
		QuickNSmart.Contracts.Modules.Common.State State
		{
			get;
			set;
		}
	}
}
