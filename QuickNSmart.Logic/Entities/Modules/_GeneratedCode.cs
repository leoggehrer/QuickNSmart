namespace QuickNSmart.Logic.Entities.Modules.Account
{
	using System;
	partial class Login : QuickNSmart.Contracts.Modules.Account.ILogin
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
		public System.DateTime? LogoutTime
		{
			get
			{
				OnLogoutTimeReading();
				return _logoutTime;
			}
			set
			{
				bool handled = false;
				OnLogoutTimeChanging(ref handled, ref _logoutTime);
				if (handled == false)
				{
					this._logoutTime = value;
				}
				OnLogoutTimeChanged();
			}
		}
		private System.DateTime? _logoutTime;
		partial void OnLogoutTimeReading();
		partial void OnLogoutTimeChanging(ref bool handled, ref System.DateTime? _logoutTime);
		partial void OnLogoutTimeChanged();
		public void CopyProperties(QuickNSmart.Contracts.Modules.Account.ILogin other)
		{
			if (other == null)
			{
				throw new System.ArgumentNullException(nameof(other));
			}
			bool handled = false;
			BeforeCopyProperties(other, ref handled);
			if (handled == false)
			{
				Email = other.Email;
				LoginTime = other.LoginTime;
				LogoutTime = other.LogoutTime;
			}
			AfterCopyProperties(other);
		}
		partial void BeforeCopyProperties(QuickNSmart.Contracts.Modules.Account.ILogin other, ref bool handled);
		partial void AfterCopyProperties(QuickNSmart.Contracts.Modules.Account.ILogin other);
		public override bool Equals(object obj)
		{
			if (!(obj is QuickNSmart.Contracts.Modules.Account.ILogin instance))
			{
				return false;
			}
			return base.Equals(instance) && Equals(instance);
		}
		protected bool Equals(QuickNSmart.Contracts.Modules.Account.ILogin other)
		{
			if (other == null)
			{
				return false;
			}
			return IsEqualsWith(Email, other.Email) && LoginTime == other.LoginTime && LogoutTime == other.LogoutTime;
		}
		public override int GetHashCode()
		{
			return HashCode.Combine(Email, LoginTime, LogoutTime);
		}
	}
}
namespace QuickNSmart.Logic.Entities.Modules.Account
{
	partial class Login : ModuleObject
	{
	}
}
namespace QuickNSmart.Logic.Entities.Modules.Account
{
	using System;
	partial class LoginUser : QuickNSmart.Contracts.Modules.Account.ILoginUser
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
		public QuickNSmart.Contracts.Modules.Common.State State
		{
			get
			{
				OnStateReading();
				return _state;
			}
			set
			{
				bool handled = false;
				OnStateChanging(ref handled, ref _state);
				if (handled == false)
				{
					this._state = value;
				}
				OnStateChanged();
			}
		}
		private QuickNSmart.Contracts.Modules.Common.State _state;
		partial void OnStateReading();
		partial void OnStateChanging(ref bool handled, ref QuickNSmart.Contracts.Modules.Common.State _state);
		partial void OnStateChanged();
		public void CopyProperties(QuickNSmart.Contracts.Modules.Account.ILoginUser other)
		{
			if (other == null)
			{
				throw new System.ArgumentNullException(nameof(other));
			}
			bool handled = false;
			BeforeCopyProperties(other, ref handled);
			if (handled == false)
			{
				Email = other.Email;
				LoginTime = other.LoginTime;
				LogoutTime = other.LogoutTime;
				Name = other.Name;
				State = other.State;
			}
			AfterCopyProperties(other);
		}
		partial void BeforeCopyProperties(QuickNSmart.Contracts.Modules.Account.ILoginUser other, ref bool handled);
		partial void AfterCopyProperties(QuickNSmart.Contracts.Modules.Account.ILoginUser other);
		public override bool Equals(object obj)
		{
			if (!(obj is QuickNSmart.Contracts.Modules.Account.ILoginUser instance))
			{
				return false;
			}
			return base.Equals(instance) && Equals(instance);
		}
		protected bool Equals(QuickNSmart.Contracts.Modules.Account.ILoginUser other)
		{
			if (other == null)
			{
				return false;
			}
			return IsEqualsWith(Email, other.Email) && LoginTime == other.LoginTime && LogoutTime == other.LogoutTime && IsEqualsWith(Name, other.Name) && State == other.State;
		}
		public override int GetHashCode()
		{
			return HashCode.Combine(Email, LoginTime, LogoutTime, Name, State);
		}
	}
}
namespace QuickNSmart.Logic.Entities.Modules.Account
{
	partial class LoginUser : Login
	{
	}
}
