namespace QuickNSmart.Logic
{
	public static partial class Factory
	{
		public static Contracts.Client.IControllerAccess<I> Create<I>() where I : Contracts.IIdentifiable
		{
			Contracts.Client.IControllerAccess<I> result = null;
			if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.TestOneToMany.IInvoice))
			{
				result = new Controllers.Persistence.TestOneToMany.InvoiceController(CreateContext()) as Contracts.Client.IControllerAccess<I>;
			}
			else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.TestOneToMany.IInvoiceDetail))
			{
				result = new Controllers.Persistence.TestOneToMany.InvoiceDetailController(CreateContext()) as Contracts.Client.IControllerAccess<I>;
			}
			else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IRole))
			{
				result = new Controllers.Persistence.Account.RoleController(CreateContext()) as Contracts.Client.IControllerAccess<I>;
			}
			else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IUser))
			{
				result = new Controllers.Persistence.Account.UserController(CreateContext()) as Contracts.Client.IControllerAccess<I>;
			}
			else if (typeof(I) == typeof(QuickNSmart.Contracts.Business.TestOneToMany.IInvoiceDetails))
			{
				result = new Controllers.Business.TestOneToMany.InvoiceDetailsController(CreateContext()) as Contracts.Client.IControllerAccess<I>;
			}
			else if (typeof(I) == typeof(QuickNSmart.Contracts.Business.Account.IAppAccess))
			{
				result = new Controllers.Business.Account.AppAccessController(CreateContext()) as Contracts.Client.IControllerAccess<I>;
			}
			else if (typeof(I) == typeof(QuickNSmart.Contracts.Business.Account.IIdentityUser))
			{
				result = new Controllers.Business.Account.IdentityUserController(CreateContext()) as Contracts.Client.IControllerAccess<I>;
			}
			return result;
		}
		public static Contracts.Client.IControllerAccess<I> Create<I>(object sharedController) where I : Contracts.IIdentifiable
		{
			Contracts.Client.IControllerAccess<I> result = null;
			if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.TestOneToMany.IInvoice))
			{
				result = new Controllers.Persistence.TestOneToMany.InvoiceController(sharedController as Controllers.ControllerObject) as Contracts.Client.IControllerAccess<I>;
			}
			else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.TestOneToMany.IInvoiceDetail))
			{
				result = new Controllers.Persistence.TestOneToMany.InvoiceDetailController(sharedController as Controllers.ControllerObject) as Contracts.Client.IControllerAccess<I>;
			}
			else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IRole))
			{
				result = new Controllers.Persistence.Account.RoleController(sharedController as Controllers.ControllerObject) as Contracts.Client.IControllerAccess<I>;
			}
			else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IUser))
			{
				result = new Controllers.Persistence.Account.UserController(sharedController as Controllers.ControllerObject) as Contracts.Client.IControllerAccess<I>;
			}
			else if (typeof(I) == typeof(QuickNSmart.Contracts.Business.TestOneToMany.IInvoiceDetails))
			{
				result = new Controllers.Business.TestOneToMany.InvoiceDetailsController(sharedController as Controllers.ControllerObject) as Contracts.Client.IControllerAccess<I>;
			}
			else if (typeof(I) == typeof(QuickNSmart.Contracts.Business.Account.IAppAccess))
			{
				result = new Controllers.Business.Account.AppAccessController(sharedController as Controllers.ControllerObject) as Contracts.Client.IControllerAccess<I>;
			}
			else if (typeof(I) == typeof(QuickNSmart.Contracts.Business.Account.IIdentityUser))
			{
				result = new Controllers.Business.Account.IdentityUserController(sharedController as Controllers.ControllerObject) as Contracts.Client.IControllerAccess<I>;
			}
			return result;
		}
		public static Contracts.Client.IControllerAccess<I> Create<I>(string sessionToken) where I : Contracts.IIdentifiable
		{
			Contracts.Client.IControllerAccess<I> result = null;
			if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.TestOneToMany.IInvoice))
			{
				result = new Controllers.Persistence.TestOneToMany.InvoiceController(CreateContext())
				{
					SessionToken = sessionToken
				}
				as Contracts.Client.IControllerAccess<I>;
			}
			else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.TestOneToMany.IInvoiceDetail))
			{
				result = new Controllers.Persistence.TestOneToMany.InvoiceDetailController(CreateContext())
				{
					SessionToken = sessionToken
				}
				as Contracts.Client.IControllerAccess<I>;
			}
			else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IRole))
			{
				result = new Controllers.Persistence.Account.RoleController(CreateContext())
				{
					SessionToken = sessionToken
				}
				as Contracts.Client.IControllerAccess<I>;
			}
			else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IUser))
			{
				result = new Controllers.Persistence.Account.UserController(CreateContext())
				{
					SessionToken = sessionToken
				}
				as Contracts.Client.IControllerAccess<I>;
			}
			else if (typeof(I) == typeof(QuickNSmart.Contracts.Business.TestOneToMany.IInvoiceDetails))
			{
				result = new Controllers.Business.TestOneToMany.InvoiceDetailsController(CreateContext())
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
			else if (typeof(I) == typeof(QuickNSmart.Contracts.Business.Account.IIdentityUser))
			{
				result = new Controllers.Business.Account.IdentityUserController(CreateContext())
				{
					SessionToken = sessionToken
				}
				as Contracts.Client.IControllerAccess<I>;
			}
			return result;
		}
	}
}
