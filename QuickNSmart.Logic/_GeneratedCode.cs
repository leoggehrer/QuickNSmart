namespace QuickNSmart.Logic
{
	public static partial class Factory
	{
		public static Contracts.Client.IControllerAccess<I> Create<I>() where I : Contracts.IIdentifiable
		{
			Contracts.Client.IControllerAccess<I> result = null;
			if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IActionLog))
			{
				result = new Controllers.Persistence.Account.ActionLogController(CreateContext()) as Contracts.Client.IControllerAccess<I>;
			}
			else if (typeof(I) == typeof(QuickNSmart.Contracts.Business.Account.IAppAccess))
			{
				result = new Controllers.Business.Account.AppAccessController(CreateContext()) as Contracts.Client.IControllerAccess<I>;
			}
			return result;
		}
		public static Contracts.Client.IControllerAccess<I> Create<I>(object sharedController) where I : Contracts.IIdentifiable
		{
			Contracts.Client.IControllerAccess<I> result = null;
			if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IActionLog))
			{
				result = new Controllers.Persistence.Account.ActionLogController(sharedController as Controllers.ControllerObject) as Contracts.Client.IControllerAccess<I>;
			}
			else if (typeof(I) == typeof(QuickNSmart.Contracts.Business.Account.IAppAccess))
			{
				result = new Controllers.Business.Account.AppAccessController(sharedController as Controllers.ControllerObject) as Contracts.Client.IControllerAccess<I>;
			}
			return result;
		}
		public static Contracts.Client.IControllerAccess<I> Create<I>(string sessionToken) where I : Contracts.IIdentifiable
		{
			Contracts.Client.IControllerAccess<I> result = null;
			if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IActionLog))
			{
				result = new Controllers.Persistence.Account.ActionLogController(CreateContext())
				{
					SessionToken = sessionToken
				}
				as Contracts.Client.IControllerAccess<I>;
			}
			else if (typeof(I) == typeof(QuickNSmart.Contracts.Business.Account.IAppAccess))
			{
				result = new Controllers.Business.Account.AppAccessController(CreateContext())
				{
					SessionToken = sessionToken
				}
				as Contracts.Client.IControllerAccess<I>;
			}
			return result;
		}
	}
}
