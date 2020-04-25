namespace QuickNSmart.Adapters
{
	public static partial class Factory
	{
		public static Contracts.Client.IAdapterAccess<I> Create<I>()
		{
			Contracts.Client.IAdapterAccess<I> result = null;
			if (Adapter == AdapterType.Controller)
			{
				if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.TestOneToMany.IInvoice))
				{
					result = new Controller.GenericControllerAdapter<QuickNSmart.Contracts.Persistence.TestOneToMany.IInvoice>() as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.TestOneToMany.IInvoiceDetail))
				{
					result = new Controller.GenericControllerAdapter<QuickNSmart.Contracts.Persistence.TestOneToMany.IInvoiceDetail>() as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IRole))
				{
					result = new Controller.GenericControllerAdapter<QuickNSmart.Contracts.Persistence.Account.IRole>() as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IUser))
				{
					result = new Controller.GenericControllerAdapter<QuickNSmart.Contracts.Persistence.Account.IUser>() as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Business.TestOneToMany.IInvoiceDetails))
				{
					result = new Controller.GenericControllerAdapter<QuickNSmart.Contracts.Business.TestOneToMany.IInvoiceDetails>() as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Business.Account.IAppAccess))
				{
					result = new Controller.GenericControllerAdapter<QuickNSmart.Contracts.Business.Account.IAppAccess>() as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Business.Account.IIdentityUser))
				{
					result = new Controller.GenericControllerAdapter<QuickNSmart.Contracts.Business.Account.IIdentityUser>() as Contracts.Client.IAdapterAccess<I>;
				}
			}
			else if (Adapter == AdapterType.Service)
			{
				if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.TestOneToMany.IInvoice))
				{
					result = new Service.GenericServiceAdapter<QuickNSmart.Contracts.Persistence.TestOneToMany.IInvoice, Transfer.Persistence.TestOneToMany.Invoice>(BaseUri, "Invoice") as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.TestOneToMany.IInvoiceDetail))
				{
					result = new Service.GenericServiceAdapter<QuickNSmart.Contracts.Persistence.TestOneToMany.IInvoiceDetail, Transfer.Persistence.TestOneToMany.InvoiceDetail>(BaseUri, "InvoiceDetail") as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IRole))
				{
					result = new Service.GenericServiceAdapter<QuickNSmart.Contracts.Persistence.Account.IRole, Transfer.Persistence.Account.Role>(BaseUri, "Role") as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IUser))
				{
					result = new Service.GenericServiceAdapter<QuickNSmart.Contracts.Persistence.Account.IUser, Transfer.Persistence.Account.User>(BaseUri, "User") as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Business.TestOneToMany.IInvoiceDetails))
				{
					result = new Service.GenericServiceAdapter<QuickNSmart.Contracts.Business.TestOneToMany.IInvoiceDetails, Transfer.Business.TestOneToMany.InvoiceDetails>(BaseUri, "InvoiceDetails") as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Business.Account.IAppAccess))
				{
					result = new Service.GenericServiceAdapter<QuickNSmart.Contracts.Business.Account.IAppAccess, Transfer.Business.Account.AppAccess>(BaseUri, "AppAccess") as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Business.Account.IIdentityUser))
				{
					result = new Service.GenericServiceAdapter<QuickNSmart.Contracts.Business.Account.IIdentityUser, Transfer.Business.Account.IdentityUser>(BaseUri, "IdentityUser") as Contracts.Client.IAdapterAccess<I>;
				}
			}
			return result;
		}
		public static Contracts.Client.IAdapterAccess<I> Create<I>(string sessionToken)
		{
			Contracts.Client.IAdapterAccess<I> result = null;
			if (Adapter == AdapterType.Controller)
			{
				if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.TestOneToMany.IInvoice))
				{
					result = new Controller.GenericControllerAdapter<QuickNSmart.Contracts.Persistence.TestOneToMany.IInvoice>(sessionToken) as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.TestOneToMany.IInvoiceDetail))
				{
					result = new Controller.GenericControllerAdapter<QuickNSmart.Contracts.Persistence.TestOneToMany.IInvoiceDetail>(sessionToken) as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IRole))
				{
					result = new Controller.GenericControllerAdapter<QuickNSmart.Contracts.Persistence.Account.IRole>(sessionToken) as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IUser))
				{
					result = new Controller.GenericControllerAdapter<QuickNSmart.Contracts.Persistence.Account.IUser>(sessionToken) as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Business.TestOneToMany.IInvoiceDetails))
				{
					result = new Controller.GenericControllerAdapter<QuickNSmart.Contracts.Business.TestOneToMany.IInvoiceDetails>(sessionToken) as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Business.Account.IAppAccess))
				{
					result = new Controller.GenericControllerAdapter<QuickNSmart.Contracts.Business.Account.IAppAccess>(sessionToken) as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Business.Account.IIdentityUser))
				{
					result = new Controller.GenericControllerAdapter<QuickNSmart.Contracts.Business.Account.IIdentityUser>(sessionToken) as Contracts.Client.IAdapterAccess<I>;
				}
			}
			else if (Adapter == AdapterType.Service)
			{
				if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.TestOneToMany.IInvoice))
				{
					result = new Service.GenericServiceAdapter<QuickNSmart.Contracts.Persistence.TestOneToMany.IInvoice, Transfer.Persistence.TestOneToMany.Invoice>(sessionToken, BaseUri, "Invoice") as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.TestOneToMany.IInvoiceDetail))
				{
					result = new Service.GenericServiceAdapter<QuickNSmart.Contracts.Persistence.TestOneToMany.IInvoiceDetail, Transfer.Persistence.TestOneToMany.InvoiceDetail>(sessionToken, BaseUri, "InvoiceDetail") as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IRole))
				{
					result = new Service.GenericServiceAdapter<QuickNSmart.Contracts.Persistence.Account.IRole, Transfer.Persistence.Account.Role>(sessionToken, BaseUri, "Role") as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IUser))
				{
					result = new Service.GenericServiceAdapter<QuickNSmart.Contracts.Persistence.Account.IUser, Transfer.Persistence.Account.User>(sessionToken, BaseUri, "User") as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Business.TestOneToMany.IInvoiceDetails))
				{
					result = new Service.GenericServiceAdapter<QuickNSmart.Contracts.Business.TestOneToMany.IInvoiceDetails, Transfer.Business.TestOneToMany.InvoiceDetails>(sessionToken, BaseUri, "InvoiceDetails") as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Business.Account.IAppAccess))
				{
					result = new Service.GenericServiceAdapter<QuickNSmart.Contracts.Business.Account.IAppAccess, Transfer.Business.Account.AppAccess>(sessionToken, BaseUri, "AppAccess") as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Business.Account.IIdentityUser))
				{
					result = new Service.GenericServiceAdapter<QuickNSmart.Contracts.Business.Account.IIdentityUser, Transfer.Business.Account.IdentityUser>(sessionToken, BaseUri, "IdentityUser") as Contracts.Client.IAdapterAccess<I>;
				}
			}
			return result;
		}
	}
}
