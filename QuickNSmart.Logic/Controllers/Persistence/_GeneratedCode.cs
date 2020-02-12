namespace QuickNSmart.Logic.Controllers.Persistence.Account
{
	sealed partial class ApplicationController : GenericController<QuickNSmart.Contracts.Persistence.Account.IApplication, Entities.Persistence.Account.Application>
	{
		static ApplicationController()
		{
			ClassConstructing();
			ClassConstructed();
		}
		static partial void ClassConstructing();
		static partial void ClassConstructed();
		public ApplicationController(DataContext.IContext context):base(context)
		{
			Constructing();
			Constructed();
		}
		partial void Constructing();
		partial void Constructed();
		public ApplicationController(ControllerObject controller):base(controller)
		{
			Constructing();
			Constructed();
		}
	}
}
namespace QuickNSmart.Logic.Controllers.Persistence.Account
{
	sealed partial class LoginUserController : GenericController<QuickNSmart.Contracts.Persistence.Account.ILoginUser, Entities.Persistence.Account.LoginUser>
	{
		static LoginUserController()
		{
			ClassConstructing();
			ClassConstructed();
		}
		static partial void ClassConstructing();
		static partial void ClassConstructed();
		public LoginUserController(DataContext.IContext context):base(context)
		{
			Constructing();
			Constructed();
		}
		partial void Constructing();
		partial void Constructed();
		public LoginUserController(ControllerObject controller):base(controller)
		{
			Constructing();
			Constructed();
		}
	}
}
