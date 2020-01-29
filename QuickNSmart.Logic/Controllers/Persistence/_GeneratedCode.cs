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
	sealed partial class UserController : GenericController<QuickNSmart.Contracts.Persistence.Account.IUser, Entities.Persistence.Account.User>
	{
		static UserController()
		{
			ClassConstructing();
			ClassConstructed();
		}
		static partial void ClassConstructing();
		static partial void ClassConstructed();
		public UserController(DataContext.IContext context):base(context)
		{
			Constructing();
			Constructed();
		}
		partial void Constructing();
		partial void Constructed();
		public UserController(ControllerObject controller):base(controller)
		{
			Constructing();
			Constructed();
		}
	}
}
