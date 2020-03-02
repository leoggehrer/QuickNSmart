namespace QuickNSmart.Logic.Controllers.Persistence.Account
{
	sealed partial class ClientController : GenericController<QuickNSmart.Contracts.Persistence.Account.IClient, Entities.Persistence.Account.Client>
	{
		static ClientController()
		{
			ClassConstructing();
			ClassConstructed();
		}
		static partial void ClassConstructing();
		static partial void ClassConstructed();
		public ClientController(DataContext.IContext context):base(context)
		{
			Constructing();
			Constructed();
		}
		partial void Constructing();
		partial void Constructed();
		public ClientController(ControllerObject controller):base(controller)
		{
			Constructing();
			Constructed();
		}
	}
}
namespace QuickNSmart.Logic.Controllers.Persistence.Account
{
	sealed partial class IdentityController : GenericController<QuickNSmart.Contracts.Persistence.Account.IIdentity, Entities.Persistence.Account.Identity>
	{
		static IdentityController()
		{
			ClassConstructing();
			ClassConstructed();
		}
		static partial void ClassConstructing();
		static partial void ClassConstructed();
		public IdentityController(DataContext.IContext context):base(context)
		{
			Constructing();
			Constructed();
		}
		partial void Constructing();
		partial void Constructed();
		public IdentityController(ControllerObject controller):base(controller)
		{
			Constructing();
			Constructed();
		}
	}
}
namespace QuickNSmart.Logic.Controllers.Persistence.Account
{
	sealed partial class IdentityXRoleController : GenericController<QuickNSmart.Contracts.Persistence.Account.IIdentityXRole, Entities.Persistence.Account.IdentityXRole>
	{
		static IdentityXRoleController()
		{
			ClassConstructing();
			ClassConstructed();
		}
		static partial void ClassConstructing();
		static partial void ClassConstructed();
		public IdentityXRoleController(DataContext.IContext context):base(context)
		{
			Constructing();
			Constructed();
		}
		partial void Constructing();
		partial void Constructed();
		public IdentityXRoleController(ControllerObject controller):base(controller)
		{
			Constructing();
			Constructed();
		}
	}
}
namespace QuickNSmart.Logic.Controllers.Persistence.Account
{
	sealed partial class LoginSessionController : GenericController<QuickNSmart.Contracts.Persistence.Account.ILoginSession, Entities.Persistence.Account.LoginSession>
	{
		static LoginSessionController()
		{
			ClassConstructing();
			ClassConstructed();
		}
		static partial void ClassConstructing();
		static partial void ClassConstructed();
		public LoginSessionController(DataContext.IContext context):base(context)
		{
			Constructing();
			Constructed();
		}
		partial void Constructing();
		partial void Constructed();
		public LoginSessionController(ControllerObject controller):base(controller)
		{
			Constructing();
			Constructed();
		}
	}
}
namespace QuickNSmart.Logic.Controllers.Persistence.Account
{
	sealed partial class RoleController : GenericController<QuickNSmart.Contracts.Persistence.Account.IRole, Entities.Persistence.Account.Role>
	{
		static RoleController()
		{
			ClassConstructing();
			ClassConstructed();
		}
		static partial void ClassConstructing();
		static partial void ClassConstructed();
		public RoleController(DataContext.IContext context):base(context)
		{
			Constructing();
			Constructed();
		}
		partial void Constructing();
		partial void Constructed();
		public RoleController(ControllerObject controller):base(controller)
		{
			Constructing();
			Constructed();
		}
	}
}
