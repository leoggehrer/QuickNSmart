namespace QuickNSmart.Logic
{
	public static partial class Factory
	{
		public static Contracts.Client.IControllerAccess<I> Create<I>() where I : Contracts.IIdentifiable
		{
			Contracts.Client.IControllerAccess<I> result = null;
			if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.ILoginSession))
			{
				result = new Controllers.Persistence.Account.LoginSessionController(CreateContext()) as Contracts.Client.IControllerAccess<I>;
			}
			else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IRole))
			{
				result = new Controllers.Persistence.Account.RoleController(CreateContext()) as Contracts.Client.IControllerAccess<I>;
			}
			else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IUser))
			{
				result = new Controllers.Persistence.Account.UserController(CreateContext()) as Contracts.Client.IControllerAccess<I>;
			}
			else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IUserXRole))
			{
				result = new Controllers.Persistence.Account.UserXRoleController(CreateContext()) as Contracts.Client.IControllerAccess<I>;
			}
			else if (typeof(I) == typeof(QuickNSmart.Contracts.Business.Account.ILoginUser))
			{
				result = new Controllers.Business.Account.LoginUserController(CreateContext()) as Contracts.Client.IControllerAccess<I>;
			}
			return result;
		}
		public static Contracts.Client.IControllerAccess<I> Create<I>(object sharedController) where I : Contracts.IIdentifiable
		{
			Contracts.Client.IControllerAccess<I> result = null;
			if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.ILoginSession))
			{
				result = new Controllers.Persistence.Account.LoginSessionController(sharedController as Controllers.ControllerObject) as Contracts.Client.IControllerAccess<I>;
			}
			else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IRole))
			{
				result = new Controllers.Persistence.Account.RoleController(sharedController as Controllers.ControllerObject) as Contracts.Client.IControllerAccess<I>;
			}
			else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IUser))
			{
				result = new Controllers.Persistence.Account.UserController(sharedController as Controllers.ControllerObject) as Contracts.Client.IControllerAccess<I>;
			}
			else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IUserXRole))
			{
				result = new Controllers.Persistence.Account.UserXRoleController(sharedController as Controllers.ControllerObject) as Contracts.Client.IControllerAccess<I>;
			}
			else if (typeof(I) == typeof(QuickNSmart.Contracts.Business.Account.ILoginUser))
			{
				result = new Controllers.Business.Account.LoginUserController(sharedController as Controllers.ControllerObject) as Contracts.Client.IControllerAccess<I>;
			}
			return result;
		}
	}
}
