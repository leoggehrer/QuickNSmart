namespace QuickNSmart.Logic.Controllers.Business.Account
{
	sealed partial class AuthenticationController : ControllerObject, Contracts.Client.IControllerAccess<QuickNSmart.Contracts.Business.Account.IAuthentication>
	{
		static AuthenticationController()
		{
			ClassConstructing();
			ClassConstructed();
		}
		static partial void ClassConstructing();
		static partial void ClassConstructed();
		public AuthenticationController(DataContext.IContext context):base(context)
		{
			Constructing();
			Constructed();
		}
		partial void Constructing();
		partial void Constructed();
		public AuthenticationController(ControllerObject controller):base(controller)
		{
			Constructing();
			Constructed();
		}
	}
}
namespace QuickNSmart.Logic.Controllers.Business.Account
{
	sealed partial class LoginUserController : ControllerObject, Contracts.Client.IControllerAccess<QuickNSmart.Contracts.Business.Account.ILoginUser>
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
