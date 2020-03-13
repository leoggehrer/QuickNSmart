namespace QuickNSmart.Transfer.Modules.Account
{
	using System.Text.Json.Serialization;
	public partial class Login : QuickNSmart.Contracts.Modules.Account.ILogin
	{
		static Login()
		{
			ClassConstructing();
			ClassConstructed();
		}
		static partial void ClassConstructing();
		static partial void ClassConstructed();
		public Login()
		{
			Constructing();
			Constructed();
		}
		partial void Constructing();
		partial void Constructed();
		public System.Int32 IdentityId
		{
			get
			{
				OnIdentityIdReading();
				return _identityId;
			}
			set
			{
				bool handled = false;
				OnIdentityIdChanging(ref handled, ref _identityId);
				if (handled == false)
				{
					this._identityId = value;
				}
				OnIdentityIdChanged();
			}
		}
		private System.Int32 _identityId;
		partial void OnIdentityIdReading();
		partial void OnIdentityIdChanging(ref bool handled, ref System.Int32 _identityId);
		partial void OnIdentityIdChanged();
		public System.String Guid
		{
			get
			{
				OnGuidReading();
				return _guid;
			}
			set
			{
				bool handled = false;
				OnGuidChanging(ref handled, ref _guid);
				if (handled == false)
				{
					this._guid = value;
				}
				OnGuidChanged();
			}
		}
		private System.String _guid;
		partial void OnGuidReading();
		partial void OnGuidChanging(ref bool handled, ref System.String _guid);
		partial void OnGuidChanged();
		public System.String Name
		{
			get
			{
				OnNameReading();
				return _name;
			}
			set
			{
				bool handled = false;
				OnNameChanging(ref handled, ref _name);
				if (handled == false)
				{
					this._name = value;
				}
				OnNameChanged();
			}
		}
		private System.String _name;
		partial void OnNameReading();
		partial void OnNameChanging(ref bool handled, ref System.String _name);
		partial void OnNameChanged();
		public System.String Email
		{
			get
			{
				OnEmailReading();
				return _email;
			}
			set
			{
				bool handled = false;
				OnEmailChanging(ref handled, ref _email);
				if (handled == false)
				{
					this._email = value;
				}
				OnEmailChanged();
			}
		}
		private System.String _email;
		partial void OnEmailReading();
		partial void OnEmailChanging(ref bool handled, ref System.String _email);
		partial void OnEmailChanged();
		public System.DateTime LoginTime
		{
			get
			{
				OnLoginTimeReading();
				return _loginTime;
			}
			set
			{
				bool handled = false;
				OnLoginTimeChanging(ref handled, ref _loginTime);
				if (handled == false)
				{
					this._loginTime = value;
				}
				OnLoginTimeChanged();
			}
		}
		private System.DateTime _loginTime;
		partial void OnLoginTimeReading();
		partial void OnLoginTimeChanging(ref bool handled, ref System.DateTime _loginTime);
		partial void OnLoginTimeChanged();
		public System.String AuthenticationToken
		{
			get
			{
				OnAuthenticationTokenReading();
				return _authenticationToken;
			}
			set
			{
				bool handled = false;
				OnAuthenticationTokenChanging(ref handled, ref _authenticationToken);
				if (handled == false)
				{
					this._authenticationToken = value;
				}
				OnAuthenticationTokenChanged();
			}
		}
		private System.String _authenticationToken;
		partial void OnAuthenticationTokenReading();
		partial void OnAuthenticationTokenChanging(ref bool handled, ref System.String _authenticationToken);
		partial void OnAuthenticationTokenChanged();
		public void CopyProperties(QuickNSmart.Contracts.Modules.Account.ILogin other)
		{
			base.CopyProperties(other);
			bool handled = false;
			BeforeCopyProperties(other, ref handled);
			if (handled == false)
			{
				IdentityId = other.IdentityId;
				Guid = other.Guid;
				Name = other.Name;
				Email = other.Email;
				LoginTime = other.LoginTime;
				AuthenticationToken = other.AuthenticationToken;
			}
			AfterCopyProperties(other);
		}
		partial void BeforeCopyProperties(QuickNSmart.Contracts.Modules.Account.ILogin other, ref bool handled);
		partial void AfterCopyProperties(QuickNSmart.Contracts.Modules.Account.ILogin other);
	}
}
namespace QuickNSmart.Transfer.Modules.Account
{
	partial class Login : ModuleObject
	{
	}
}
