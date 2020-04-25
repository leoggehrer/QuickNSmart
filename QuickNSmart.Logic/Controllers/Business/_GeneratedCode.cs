namespace QuickNSmart.Logic.Controllers.Business.TestOneToMany
{
	sealed partial class InvoiceDetailsController : GenericOneToManyController<QuickNSmart.Contracts.Business.TestOneToMany.IInvoiceDetails, Entities.Business.TestOneToMany.InvoiceDetails, QuickNSmart.Contracts.Persistence.TestOneToMany.IInvoice, QuickNSmart.Logic.Entities.Persistence.TestOneToMany.Invoice, QuickNSmart.Contracts.Persistence.TestOneToMany.IInvoiceDetail, QuickNSmart.Logic.Entities.Persistence.TestOneToMany.InvoiceDetail>
	{
		static InvoiceDetailsController()
		{
			ClassConstructing();
			ClassConstructed();
		}
		static partial void ClassConstructing();
		static partial void ClassConstructed();
		public InvoiceDetailsController(DataContext.IContext context):base(context)
		{
			Constructing();
			Constructed();
		}
		partial void Constructing();
		partial void Constructed();
		public InvoiceDetailsController(ControllerObject controller):base(controller)
		{
			Constructing();
			Constructed();
		}
		protected override GenericController<QuickNSmart.Contracts.Persistence.TestOneToMany.IInvoice, QuickNSmart.Logic.Entities.Persistence.TestOneToMany.Invoice> CreateFirstEntityController(ControllerObject controller)
		{
			return new QuickNSmart.Logic.Controllers.Persistence.TestOneToMany.InvoiceController(controller);
		}
		protected override GenericController<QuickNSmart.Contracts.Persistence.TestOneToMany.IInvoiceDetail, QuickNSmart.Logic.Entities.Persistence.TestOneToMany.InvoiceDetail> CreateSecondEntityController(ControllerObject controller)
		{
			return new QuickNSmart.Logic.Controllers.Persistence.TestOneToMany.InvoiceDetailController(controller);
		}
	}
}
namespace QuickNSmart.Logic.Controllers.Business.Account
{
	[Logic.Modules.Security.Authorize("SysAdmin")]
	sealed partial class AppAccessController : BusinessControllerAdapter<QuickNSmart.Contracts.Business.Account.IAppAccess, Entities.Business.Account.AppAccess>
	{
		static AppAccessController()
		{
			ClassConstructing();
			ClassConstructed();
		}
		static partial void ClassConstructing();
		static partial void ClassConstructed();
		public AppAccessController(DataContext.IContext context):base(context)
		{
			Constructing();
			Constructed();
		}
		partial void Constructing();
		partial void Constructed();
		public AppAccessController(ControllerObject controller):base(controller)
		{
			Constructing();
			Constructed();
		}
	}
}
namespace QuickNSmart.Logic.Controllers.Business.Account
{
	sealed partial class IdentityUserController : GenericOneToOneController<QuickNSmart.Contracts.Business.Account.IIdentityUser, Entities.Business.Account.IdentityUser, QuickNSmart.Contracts.Persistence.Account.IIdentity, QuickNSmart.Logic.Entities.Persistence.Account.Identity, QuickNSmart.Contracts.Persistence.Account.IUser, QuickNSmart.Logic.Entities.Persistence.Account.User>
	{
		static IdentityUserController()
		{
			ClassConstructing();
			ClassConstructed();
		}
		static partial void ClassConstructing();
		static partial void ClassConstructed();
		public IdentityUserController(DataContext.IContext context):base(context)
		{
			Constructing();
			Constructed();
		}
		partial void Constructing();
		partial void Constructed();
		public IdentityUserController(ControllerObject controller):base(controller)
		{
			Constructing();
			Constructed();
		}
		protected override GenericController<QuickNSmart.Contracts.Persistence.Account.IIdentity, QuickNSmart.Logic.Entities.Persistence.Account.Identity> CreateFirstEntityController(ControllerObject controller)
		{
			return new QuickNSmart.Logic.Controllers.Persistence.Account.IdentityController(controller);
		}
		protected override GenericController<QuickNSmart.Contracts.Persistence.Account.IUser, QuickNSmart.Logic.Entities.Persistence.Account.User> CreateSecondEntityController(ControllerObject controller)
		{
			return new QuickNSmart.Logic.Controllers.Persistence.Account.UserController(controller);
		}
	}
}
