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
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Business.Account.IAppAccess))
				{
					result = new Controller.GenericControllerAdapter<QuickNSmart.Contracts.Business.Account.IAppAccess>() as Contracts.Client.IAdapterAccess<I>;
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
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Business.Account.IAppAccess))
				{
					result = new Service.GenericServiceAdapter<QuickNSmart.Contracts.Business.Account.IAppAccess, Transfer.Business.Account.AppAccess>(BaseUri, "AppAccess") as Contracts.Client.IAdapterAccess<I>;
				}
			}
			return result;
		}
		public static Contracts.Client.IAdapterAccess<I> Create<I>(string authenticationToken) where I : Contracts.IIdentifiable
		{
			Contracts.Client.IAdapterAccess<I> result = null;
			if (Adapter == AdapterType.Controller)
			{
				if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IClient))
				{
					result = new Controller.GenericControllerAdapter<QuickNSmart.Contracts.Persistence.Account.IClient>(authenticationToken) as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IIdentity))
				{
					result = new Controller.GenericControllerAdapter<QuickNSmart.Contracts.Persistence.Account.IIdentity>(authenticationToken) as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IIdentityXRole))
				{
					result = new Controller.GenericControllerAdapter<QuickNSmart.Contracts.Persistence.Account.IIdentityXRole>(authenticationToken) as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.ILoginSession))
				{
					result = new Controller.GenericControllerAdapter<QuickNSmart.Contracts.Persistence.Account.ILoginSession>(authenticationToken) as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IRole))
				{
					result = new Controller.GenericControllerAdapter<QuickNSmart.Contracts.Persistence.Account.IRole>(authenticationToken) as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Business.Account.IAppAccess))
				{
					result = new Controller.GenericControllerAdapter<QuickNSmart.Contracts.Business.Account.IAppAccess>(authenticationToken) as Contracts.Client.IAdapterAccess<I>;
				}
			}
			else if (Adapter == AdapterType.Service)
			{
				if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IClient))
				{
					result = new Service.GenericServiceAdapter<QuickNSmart.Contracts.Persistence.Account.IClient, Transfer.Persistence.Account.Client>(authenticationToken, BaseUri, "Client") as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IIdentity))
				{
					result = new Service.GenericServiceAdapter<QuickNSmart.Contracts.Persistence.Account.IIdentity, Transfer.Persistence.Account.Identity>(authenticationToken, BaseUri, "Identity") as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IIdentityXRole))
				{
					result = new Service.GenericServiceAdapter<QuickNSmart.Contracts.Persistence.Account.IIdentityXRole, Transfer.Persistence.Account.IdentityXRole>(authenticationToken, BaseUri, "IdentityXRole") as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.ILoginSession))
				{
					result = new Service.GenericServiceAdapter<QuickNSmart.Contracts.Persistence.Account.ILoginSession, Transfer.Persistence.Account.LoginSession>(authenticationToken, BaseUri, "LoginSession") as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IRole))
				{
					result = new Service.GenericServiceAdapter<QuickNSmart.Contracts.Persistence.Account.IRole, Transfer.Persistence.Account.Role>(authenticationToken, BaseUri, "Role") as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Business.Account.IAppAccess))
				{
					result = new Service.GenericServiceAdapter<QuickNSmart.Contracts.Business.Account.IAppAccess, Transfer.Business.Account.AppAccess>(authenticationToken, BaseUri, "AppAccess") as Contracts.Client.IAdapterAccess<I>;
				}
			}
			return result;
		}
	}
}
