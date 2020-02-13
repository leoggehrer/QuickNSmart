namespace QuickNSmart.Adapters
{
	public static partial class Factory
	{
		public static Contracts.Client.IAdapterAccess<I> Create<I>() where I : Contracts.IIdentifiable
		{
			Contracts.Client.IAdapterAccess<I> result = null;
			if (Adapter == AdapterType.Controller)
			{
				if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.ILoginSession))
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
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Business.Account.ILoginUser))
				{
					result = new Controller.GenericControllerAdapter<QuickNSmart.Contracts.Business.Account.ILoginUser>() as Contracts.Client.IAdapterAccess<I>;
				}
			}
			else if (Adapter == AdapterType.Service)
			{
				if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.ILoginSession))
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
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Business.Account.ILoginUser))
				{
					result = new Service.GenericServiceAdapter<QuickNSmart.Contracts.Business.Account.ILoginUser, Transfer.Business.Account.LoginUser>(BaseUri, "LoginUser") as Contracts.Client.IAdapterAccess<I>;
				}
			}
			return result;
		}
	}
}
