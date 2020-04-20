namespace QuickNSmart.Adapters
{
	public static partial class Factory
	{
		public static Contracts.Client.IAdapterAccess<I> Create<I>()
		{
			Contracts.Client.IAdapterAccess<I> result = null;
			if (Adapter == AdapterType.Controller)
			{
				if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.TestRelation.IInvoice))
				{
					result = new Controller.GenericControllerAdapter<QuickNSmart.Contracts.Persistence.TestRelation.IInvoice>() as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.TestRelation.IInvoiceDetail))
				{
					result = new Controller.GenericControllerAdapter<QuickNSmart.Contracts.Persistence.TestRelation.IInvoiceDetail>() as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IRole))
				{
					result = new Controller.GenericControllerAdapter<QuickNSmart.Contracts.Persistence.Account.IRole>() as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Business.TestRelation.IInvoiceDetails))
				{
					result = new Controller.GenericControllerAdapter<QuickNSmart.Contracts.Business.TestRelation.IInvoiceDetails>() as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Business.Account.IAppAccess))
				{
					result = new Controller.GenericControllerAdapter<QuickNSmart.Contracts.Business.Account.IAppAccess>() as Contracts.Client.IAdapterAccess<I>;
				}
			}
			else if (Adapter == AdapterType.Service)
			{
				if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.TestRelation.IInvoice))
				{
					result = new Service.GenericServiceAdapter<QuickNSmart.Contracts.Persistence.TestRelation.IInvoice, Transfer.Persistence.TestRelation.Invoice>(BaseUri, "Invoice") as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.TestRelation.IInvoiceDetail))
				{
					result = new Service.GenericServiceAdapter<QuickNSmart.Contracts.Persistence.TestRelation.IInvoiceDetail, Transfer.Persistence.TestRelation.InvoiceDetail>(BaseUri, "InvoiceDetail") as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IRole))
				{
					result = new Service.GenericServiceAdapter<QuickNSmart.Contracts.Persistence.Account.IRole, Transfer.Persistence.Account.Role>(BaseUri, "Role") as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Business.TestRelation.IInvoiceDetails))
				{
					result = new Service.GenericServiceAdapter<QuickNSmart.Contracts.Business.TestRelation.IInvoiceDetails, Transfer.Business.TestRelation.InvoiceDetails>(BaseUri, "InvoiceDetails") as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Business.Account.IAppAccess))
				{
					result = new Service.GenericServiceAdapter<QuickNSmart.Contracts.Business.Account.IAppAccess, Transfer.Business.Account.AppAccess>(BaseUri, "AppAccess") as Contracts.Client.IAdapterAccess<I>;
				}
			}
			return result;
		}
		public static Contracts.Client.IAdapterAccess<I> Create<I>(string sessionToken)
		{
			Contracts.Client.IAdapterAccess<I> result = null;
			if (Adapter == AdapterType.Controller)
			{
				if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.TestRelation.IInvoice))
				{
					result = new Controller.GenericControllerAdapter<QuickNSmart.Contracts.Persistence.TestRelation.IInvoice>(sessionToken) as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.TestRelation.IInvoiceDetail))
				{
					result = new Controller.GenericControllerAdapter<QuickNSmart.Contracts.Persistence.TestRelation.IInvoiceDetail>(sessionToken) as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IRole))
				{
					result = new Controller.GenericControllerAdapter<QuickNSmart.Contracts.Persistence.Account.IRole>(sessionToken) as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Business.TestRelation.IInvoiceDetails))
				{
					result = new Controller.GenericControllerAdapter<QuickNSmart.Contracts.Business.TestRelation.IInvoiceDetails>(sessionToken) as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Business.Account.IAppAccess))
				{
					result = new Controller.GenericControllerAdapter<QuickNSmart.Contracts.Business.Account.IAppAccess>(sessionToken) as Contracts.Client.IAdapterAccess<I>;
				}
			}
			else if (Adapter == AdapterType.Service)
			{
				if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.TestRelation.IInvoice))
				{
					result = new Service.GenericServiceAdapter<QuickNSmart.Contracts.Persistence.TestRelation.IInvoice, Transfer.Persistence.TestRelation.Invoice>(sessionToken, BaseUri, "Invoice") as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.TestRelation.IInvoiceDetail))
				{
					result = new Service.GenericServiceAdapter<QuickNSmart.Contracts.Persistence.TestRelation.IInvoiceDetail, Transfer.Persistence.TestRelation.InvoiceDetail>(sessionToken, BaseUri, "InvoiceDetail") as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IRole))
				{
					result = new Service.GenericServiceAdapter<QuickNSmart.Contracts.Persistence.Account.IRole, Transfer.Persistence.Account.Role>(sessionToken, BaseUri, "Role") as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Business.TestRelation.IInvoiceDetails))
				{
					result = new Service.GenericServiceAdapter<QuickNSmart.Contracts.Business.TestRelation.IInvoiceDetails, Transfer.Business.TestRelation.InvoiceDetails>(sessionToken, BaseUri, "InvoiceDetails") as Contracts.Client.IAdapterAccess<I>;
				}
				else if (typeof(I) == typeof(QuickNSmart.Contracts.Business.Account.IAppAccess))
				{
					result = new Service.GenericServiceAdapter<QuickNSmart.Contracts.Business.Account.IAppAccess, Transfer.Business.Account.AppAccess>(sessionToken, BaseUri, "AppAccess") as Contracts.Client.IAdapterAccess<I>;
				}
			}
			return result;
		}
	}
}
