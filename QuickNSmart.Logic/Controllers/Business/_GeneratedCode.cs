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
