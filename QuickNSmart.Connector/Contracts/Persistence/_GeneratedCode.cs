namespace QuickNSmart.Connector.Contracts.Persistence.Account
{
	public partial interface IActionLog : IIdentifiable, ICopyable<IActionLog>
	{
		System.Int32 IdentityId
		{
			get;
			set;
		}
		System.DateTime Time
		{
			get;
			set;
		}
		System.String Subject
		{
			get;
			set;
		}
		System.String Action
		{
			get;
			set;
		}
		System.String Info
		{
			get;
			set;
		}
	}
}
namespace QuickNSmart.Connector.Contracts.Persistence.Account
{
	public partial interface IIdentity : IIdentifiable, ICopyable<IIdentity>
	{
		System.String Guid
		{
			get;
		}
		System.String Name
		{
			get;
			set;
		}
		System.String Email
		{
			get;
			set;
		}
		System.String Password
		{
			get;
			set;
		}
		System.Boolean EnableJwtAuth
		{
			get;
			set;
		}
		System.Int32 AccessFailedCount
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
namespace QuickNSmart.Connector.Contracts.Persistence.Account
{
	public partial interface IIdentityXRole : IIdentifiable, ICopyable<IIdentityXRole>
	{
		System.Int32 IdentityId
		{
			get;
			set;
		}
		System.Int32 RoleId
		{
			get;
			set;
		}
	}
}
namespace QuickNSmart.Connector.Contracts.Persistence.Account
{
	public partial interface ILoginSession : IIdentifiable, ICopyable<ILoginSession>
	{
		System.Int32 IdentityId
		{
			get;
		}
		System.Boolean IsRemoteAuth
		{
			get;
		}
		System.String Origin
		{
			get;
		}
		System.String Name
		{
			get;
		}
		System.String Email
		{
			get;
		}
		System.String JsonWebToken
		{
			get;
		}
		System.String SessionToken
		{
			get;
		}
		System.DateTime LoginTime
		{
			get;
		}
		System.DateTime LastAccess
		{
			get;
		}
		System.DateTime? LogoutTime
		{
			get;
		}
	}
}
namespace QuickNSmart.Connector.Contracts.Persistence.Account
{
	public partial interface IRole : IIdentifiable, ICopyable<IRole>
	{
		System.String Designation
		{
			get;
			set;
		}
		System.String Description
		{
			get;
			set;
		}
	}
}
