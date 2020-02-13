namespace QuickNSmart.Transfer.Business.Account
{
	using System.Text.Json.Serialization;
	public partial class LoginUser : QuickNSmart.Contracts.Business.Account.ILoginUser
	{
		static LoginUser()
		{
			ClassConstructing();
			ClassConstructed();
		}
		static partial void ClassConstructing();
		static partial void ClassConstructed();
		public LoginUser()
		{
			Constructing();
			Constructed();
		}
		partial void Constructing();
		partial void Constructed();
		public QuickNSmart.Contracts.Persistence.Account.IUser User
		{
			get
			{
				OnUserReading();
				return _user;
			}
			set
			{
				bool handled = false;
				OnUserChanging(ref handled, ref _user);
				if (handled == false)
				{
					this._user = value;
				}
				OnUserChanged();
			}
		}
		private QuickNSmart.Contracts.Persistence.Account.IUser _user;
		partial void OnUserReading();
		partial void OnUserChanging(ref bool handled, ref QuickNSmart.Contracts.Persistence.Account.IUser _user);
		partial void OnUserChanged();
		public System.Collections.Generic.IEnumerable<QuickNSmart.Contracts.Persistence.Account.IRole> Roles
		{
			get
			{
				OnRolesReading();
				return _roles;
			}
			set
			{
				bool handled = false;
				OnRolesChanging(ref handled, ref _roles);
				if (handled == false)
				{
					this._roles = value;
				}
				OnRolesChanged();
			}
		}
		private System.Collections.Generic.IEnumerable<QuickNSmart.Contracts.Persistence.Account.IRole> _roles;
		partial void OnRolesReading();
		partial void OnRolesChanging(ref bool handled, ref System.Collections.Generic.IEnumerable<QuickNSmart.Contracts.Persistence.Account.IRole> _roles);
		partial void OnRolesChanged();
		public void CopyProperties(QuickNSmart.Contracts.Business.Account.ILoginUser other)
		{
			if (other == null)
			{
				throw new System.ArgumentNullException(nameof(other));
			}
			bool handled = false;
			BeforeCopyProperties(other, ref handled);
			if (handled == false)
			{
				Id = other.Id;
				Timestamp = other.Timestamp;
				User = other.User;
				Roles = other.Roles;
			}
			AfterCopyProperties(other);
		}
		partial void BeforeCopyProperties(QuickNSmart.Contracts.Business.Account.ILoginUser other, ref bool handled);
		partial void AfterCopyProperties(QuickNSmart.Contracts.Business.Account.ILoginUser other);
	}
}
namespace QuickNSmart.Transfer.Business.Account
{
	partial class LoginUser : TransferObject
	{
	}
}
