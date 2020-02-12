namespace QuickNSmart.Logic
{
	public static partial class Factory
	{
		public static Contracts.Client.IControllerAccess<I> Create<I>() where I : Contracts.IIdentifiable
		{
			Contracts.Client.IControllerAccess<I> result = null;
			if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IApplication))
			{
				result = new Controllers.Persistence.Account.ApplicationController(CreateContext()) as Contracts.Client.IControllerAccess<I>;
			}
			else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.ILoginUser))
			{
				result = new Controllers.Persistence.Account.LoginUserController(CreateContext()) as Contracts.Client.IControllerAccess<I>;
			}
			return result;
		}
		public static Contracts.Client.IControllerAccess<I> Create<I>(object sharedController) where I : Contracts.IIdentifiable
		{
			Contracts.Client.IControllerAccess<I> result = null;
			if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IApplication))
			{
				result = new Controllers.Persistence.Account.ApplicationController(sharedController as Controllers.ControllerObject) as Contracts.Client.IControllerAccess<I>;
			}
			else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.ILoginUser))
			{
				result = new Controllers.Persistence.Account.LoginUserController(sharedController as Controllers.ControllerObject) as Contracts.Client.IControllerAccess<I>;
			}
			return result;
		}
	}
}
