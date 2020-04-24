namespace QuickNSmart.Logic.Controllers.Business.TestRelation
{
	sealed partial class InvoiceDetailsController : GenericOneToManyController<QuickNSmart.Contracts.Business.TestRelation.IInvoiceDetails, Entities.Business.TestRelation.InvoiceDetails, QuickNSmart.Contracts.Persistence.TestRelation.IInvoice, QuickNSmart.Logic.Entities.Persistence.TestRelation.Invoice, QuickNSmart.Contracts.Persistence.TestRelation.IInvoiceDetail, QuickNSmart.Logic.Entities.Persistence.TestRelation.InvoiceDetail>
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
		protected override GenericController<QuickNSmart.Contracts.Persistence.TestRelation.IInvoice, QuickNSmart.Logic.Entities.Persistence.TestRelation.Invoice> CreateOneEntityController(ControllerObject controller)
		{
			return new QuickNSmart.Logic.Controllers.Persistence.TestRelation.InvoiceController(controller);
		}
		protected override GenericController<QuickNSmart.Contracts.Persistence.TestRelation.IInvoiceDetail, QuickNSmart.Logic.Entities.Persistence.TestRelation.InvoiceDetail> CreateManyEntityController(ControllerObject controller)
		{
			return new QuickNSmart.Logic.Controllers.Persistence.TestRelation.InvoiceDetailController(controller);
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
