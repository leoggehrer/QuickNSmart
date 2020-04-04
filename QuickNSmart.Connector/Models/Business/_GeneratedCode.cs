namespace QuickNSmart.Connector.Models.Business.Account
{
	partial class AppAccess : QuickNSmart.Connector.Contracts.Business.Account.IAppAccess
	{
		static AppAccess()
		{
			ClassConstructing();
			ClassConstructed();
		}
		static partial void ClassConstructing();
		static partial void ClassConstructed();
		public AppAccess()
		{
			Constructing();
			Constructed();
		}
		partial void Constructing();
		partial void Constructed();
		public QuickNSmart.Contracts.Business.Account.IAppAccess DelegateObject
		{
			get;
			set;
		}
		public QuickNSmart.Contracts.Persistence.Account.IIdentity Identity
		{
			get
			{
				OnIdentityReading();
				return DelegateObject.Identity;
			}
		}
		partial void OnIdentityReading();
		public System.Collections.Generic.IEnumerable<QuickNSmart.Contracts.Persistence.Account.IRole> Roles
		{
			get
			{
				OnRolesReading();
				return DelegateObject.Roles;
			}
		}
		partial void OnRolesReading();
		public void CopyProperties(QuickNSmart.Connector.Contracts.Business.Account.IAppAccess other)
		{
			if (other == null)
			{
				throw new System.ArgumentNullException(nameof(other));
			}
			bool handled = false;
			BeforeCopyProperties(other, ref handled);
			if (handled == false)
			{
				DelegateObject.CopyProperties(other as QuickNSmart.Contracts.Business.Account.IAppAccess);
			}
			AfterCopyProperties(other);
		}
		partial void BeforeCopyProperties(QuickNSmart.Connector.Contracts.Business.Account.IAppAccess other, ref bool handled);
		partial void AfterCopyProperties(QuickNSmart.Connector.Contracts.Business.Account.IAppAccess other);
		public void ClearRoles()
		{
			DelegateObject.ClearRoles();
		}
		public QuickNSmart.Connector.Contracts.Persistence.Account.IRole CreateRole()
		{
			var model = new QuickNSmart.Connector.Models.Persistence.Account.Role();
			model.DelegateObject = DelegateObject.CreateRole();
			return model;
		}
		public void AddRole(QuickNSmart.Contracts.Persistence.Account.IRole role)
		{
			DelegateObject.AddRole(role);
		}
		public void RemoveRole(QuickNSmart.Contracts.Persistence.Account.IRole role)
		{
			DelegateObject.RemoveRole(role);
		}
	}
	partial class AppAccess : Models.IdentityModel
	{
	}
}
