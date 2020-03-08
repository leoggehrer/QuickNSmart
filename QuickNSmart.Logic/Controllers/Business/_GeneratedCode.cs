namespace QuickNSmart.Logic.Controllers.Business.Account
{
	[Logic.Modules.Security.Authorize("SysAdmin")]
	sealed partial class AppAccessController : BusinessControllerAdapter<QuickNSmart.Contracts.Business.Account.IAppAccess>
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
