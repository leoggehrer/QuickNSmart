namespace QuickNSmart.Adapters
{
	public static partial class Factory
	{
		public static Contracts.Client.IAdapterAccess<I> Create<I>() where I : Contracts.IIdentifiable
		{
			Contracts.Client.IAdapterAccess<I> result = null;
			if (Adapter == AdapterType.Controller)
			{
				if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IClient))
				{
					result = new Controller.GenericControllerAdapter<QuickNSmart.Contracts.Persistence.Account.IClient>() as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IIdentity))
				{
					result = new Controller.GenericControllerAdapter<QuickNSmart.Contracts.Persistence.Account.IIdentity>() as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IIdentityXApplication))
				{
					result = new Controller.GenericControllerAdapter<QuickNSmart.Contracts.Persistence.Account.IIdentityXApplication>() as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IIdentityXRole))
				{
					result = new Controller.GenericControllerAdapter<QuickNSmart.Contracts.Persistence.Account.IIdentityXRole>() as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.ILoginSession))
				{
					result = new Controller.GenericControllerAdapter<QuickNSmart.Contracts.Persistence.Account.ILoginSession>() as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IRole))
				{
					result = new Controller.GenericControllerAdapter<QuickNSmart.Contracts.Persistence.Account.IRole>() as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IUser))
				{
					result = new Controller.GenericControllerAdapter<QuickNSmart.Contracts.Persistence.Account.IUser>() as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IUserXRole))
				{
					result = new Controller.GenericControllerAdapter<QuickNSmart.Contracts.Persistence.Account.IUserXRole>() as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Business.Account.IAuthentication))
				{
					result = new Controller.GenericControllerAdapter<QuickNSmart.Contracts.Business.Account.IAuthentication>() as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Business.Account.ILoginUser))
				{
					result = new Controller.GenericControllerAdapter<QuickNSmart.Contracts.Business.Account.ILoginUser>() as Contracts.Client.IAdapterAccess<I>;
				}
			}
			else if (Adapter == AdapterType.Service)
			{
				if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IClient))
				{
					result = new Service.GenericServiceAdapter<QuickNSmart.Contracts.Persistence.Account.IClient, Transfer.Persistence.Account.Client>(BaseUri, "Client") as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IIdentity))
				{
					result = new Service.GenericServiceAdapter<QuickNSmart.Contracts.Persistence.Account.IIdentity, Transfer.Persistence.Account.Identity>(BaseUri, "Identity") as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IIdentityXApplication))
				{
					result = new Service.GenericServiceAdapter<QuickNSmart.Contracts.Persistence.Account.IIdentityXApplication, Transfer.Persistence.Account.IdentityXApplication>(BaseUri, "IdentityXApplication") as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IIdentityXRole))
				{
					result = new Service.GenericServiceAdapter<QuickNSmart.Contracts.Persistence.Account.IIdentityXRole, Transfer.Persistence.Account.IdentityXRole>(BaseUri, "IdentityXRole") as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.ILoginSession))
				{
					result = new Service.GenericServiceAdapter<QuickNSmart.Contracts.Persistence.Account.ILoginSession, Transfer.Persistence.Account.LoginSession>(BaseUri, "LoginSession") as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IRole))
				{
					result = new Service.GenericServiceAdapter<QuickNSmart.Contracts.Persistence.Account.IRole, Transfer.Persistence.Account.Role>(BaseUri, "Role") as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IUser))
				{
					result = new Service.GenericServiceAdapter<QuickNSmart.Contracts.Persistence.Account.IUser, Transfer.Persistence.Account.User>(BaseUri, "User") as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IUserXRole))
				{
					result = new Service.GenericServiceAdapter<QuickNSmart.Contracts.Persistence.Account.IUserXRole, Transfer.Persistence.Account.UserXRole>(BaseUri, "UserXRole") as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Business.Account.IAuthentication))
				{
					result = new Service.GenericServiceAdapter<QuickNSmart.Contracts.Business.Account.IAuthentication, Transfer.Business.Account.Authentication>(BaseUri, "Authentication") as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Business.Account.ILoginUser))
				{
					result = new Service.GenericServiceAdapter<QuickNSmart.Contracts.Business.Account.ILoginUser, Transfer.Business.Account.LoginUser>(BaseUri, "LoginUser") as Contracts.Client.IAdapterAccess<I>;
				}
			}
			return result;
		}
	}
}
