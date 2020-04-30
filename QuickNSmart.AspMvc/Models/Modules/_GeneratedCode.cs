namespace QuickNSmart.AspMvc.Models.Modules.TestInheritance
{
	public partial class Login : QuickNSmart.Contracts.Modules.TestInheritance.ILogin
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
		public void CopyProperties(QuickNSmart.Contracts.Modules.TestInheritance.ILogin other)
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
		partial void BeforeCopyProperties(QuickNSmart.Contracts.Modules.TestInheritance.ILogin other, ref bool handled);
		partial void AfterCopyProperties(QuickNSmart.Contracts.Modules.TestInheritance.ILogin other);
	}
}
namespace QuickNSmart.AspMvc.Models.Modules.TestInheritance
{
	partial class Login : ModelObject
	{
	}
}
namespace QuickNSmart.AspMvc.Models.Modules.TestInheritance
{
	public partial class LoginUser : QuickNSmart.Contracts.Modules.TestInheritance.ILoginUser
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
		public void CopyProperties(QuickNSmart.Contracts.Modules.TestInheritance.ILoginUser other)
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
		partial void BeforeCopyProperties(QuickNSmart.Contracts.Modules.TestInheritance.ILoginUser other, ref bool handled);
		partial void AfterCopyProperties(QuickNSmart.Contracts.Modules.TestInheritance.ILoginUser other);
	}
}
namespace QuickNSmart.AspMvc.Models.Modules.TestInheritance
{
	partial class LoginUser : Login
	{
	}
}
namespace QuickNSmart.AspMvc.Models.Modules.Language
{
	public partial class Translation : QuickNSmart.Contracts.Modules.Language.ITranslation
	{
		static Translation()
		{
			ClassConstructing();
			ClassConstructed();
		}
		static partial void ClassConstructing();
		static partial void ClassConstructed();
		public Translation()
		{
			Constructing();
			Constructed();
		}
		partial void Constructing();
		partial void Constructed();
		public System.String AppName
		{
			get
			{
				OnAppNameReading();
				return _appName;
			}
			set
			{
				bool handled = false;
				OnAppNameChanging(ref handled, ref _appName);
				if (handled == false)
				{
					this._appName = value;
				}
				OnAppNameChanged();
			}
		}
		private System.String _appName;
		partial void OnAppNameReading();
		partial void OnAppNameChanging(ref bool handled, ref System.String _appName);
		partial void OnAppNameChanged();
		public QuickNSmart.Contracts.Modules.Language.LanguageCode KeyLanguage
		{
			get
			{
				OnKeyLanguageReading();
				return _keyLanguage;
			}
			set
			{
				bool handled = false;
				OnKeyLanguageChanging(ref handled, ref _keyLanguage);
				if (handled == false)
				{
					this._keyLanguage = value;
				}
				OnKeyLanguageChanged();
			}
		}
		private QuickNSmart.Contracts.Modules.Language.LanguageCode _keyLanguage;
		partial void OnKeyLanguageReading();
		partial void OnKeyLanguageChanging(ref bool handled, ref QuickNSmart.Contracts.Modules.Language.LanguageCode _keyLanguage);
		partial void OnKeyLanguageChanged();
		public System.String Key
		{
			get
			{
				OnKeyReading();
				return _key;
			}
			set
			{
				bool handled = false;
				OnKeyChanging(ref handled, ref _key);
				if (handled == false)
				{
					this._key = value;
				}
				OnKeyChanged();
			}
		}
		private System.String _key;
		partial void OnKeyReading();
		partial void OnKeyChanging(ref bool handled, ref System.String _key);
		partial void OnKeyChanged();
		public QuickNSmart.Contracts.Modules.Language.LanguageCode ValueLanguage
		{
			get
			{
				OnValueLanguageReading();
				return _valueLanguage;
			}
			set
			{
				bool handled = false;
				OnValueLanguageChanging(ref handled, ref _valueLanguage);
				if (handled == false)
				{
					this._valueLanguage = value;
				}
				OnValueLanguageChanged();
			}
		}
		private QuickNSmart.Contracts.Modules.Language.LanguageCode _valueLanguage;
		partial void OnValueLanguageReading();
		partial void OnValueLanguageChanging(ref bool handled, ref QuickNSmart.Contracts.Modules.Language.LanguageCode _valueLanguage);
		partial void OnValueLanguageChanged();
		public System.String Value
		{
			get
			{
				OnValueReading();
				return _value;
			}
			set
			{
				bool handled = false;
				OnValueChanging(ref handled, ref _value);
				if (handled == false)
				{
					this._value = value;
				}
				OnValueChanged();
			}
		}
		private System.String _value;
		partial void OnValueReading();
		partial void OnValueChanging(ref bool handled, ref System.String _value);
		partial void OnValueChanged();
		public void CopyProperties(QuickNSmart.Contracts.Modules.Language.ITranslation other)
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
				RowVersion = other.RowVersion;
				AppName = other.AppName;
				KeyLanguage = other.KeyLanguage;
				Key = other.Key;
				ValueLanguage = other.ValueLanguage;
				Value = other.Value;
			}
			AfterCopyProperties(other);
		}
		partial void BeforeCopyProperties(QuickNSmart.Contracts.Modules.Language.ITranslation other, ref bool handled);
		partial void AfterCopyProperties(QuickNSmart.Contracts.Modules.Language.ITranslation other);
	}
}
namespace QuickNSmart.AspMvc.Models.Modules.Language
{
	partial class Translation : IdentityModel
	{
	}
}
