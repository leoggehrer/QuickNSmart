namespace QuickNSmart.Connector.Models.Modules.Account
{
	partial class Login : QuickNSmart.Connector.Contracts.Modules.Account.ILogin
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
		public QuickNSmart.Contracts.Modules.Account.ILogin DelegateObject
		{
			get;
			set;
		}
		public System.String Email
		{
			get
			{
				OnEmailReading();
				return DelegateObject.Email;
			}
			set
			{
				DelegateObject.Email = value;
				OnEmailChanged();
			}
		}
		partial void OnEmailReading();
		partial void OnEmailChanged();
		public System.DateTime LoginTime
		{
			get
			{
				OnLoginTimeReading();
				return DelegateObject.LoginTime;
			}
			set
			{
				DelegateObject.LoginTime = value;
				OnLoginTimeChanged();
			}
		}
		partial void OnLoginTimeReading();
		partial void OnLoginTimeChanged();
		public System.DateTime? LogoutTime
		{
			get
			{
				OnLogoutTimeReading();
				return DelegateObject.LogoutTime;
			}
			set
			{
				DelegateObject.LogoutTime = value;
				OnLogoutTimeChanged();
			}
		}
		partial void OnLogoutTimeReading();
		partial void OnLogoutTimeChanged();
		public void CopyProperties(QuickNSmart.Connector.Contracts.Modules.Account.ILogin other)
		{
			if (other == null)
			{
				throw new System.ArgumentNullException(nameof(other));
			}
			bool handled = false;
			BeforeCopyProperties(other, ref handled);
			if (handled == false)
			{
				DelegateObject.CopyProperties(other as QuickNSmart.Contracts.Modules.Account.ILogin);
			}
			AfterCopyProperties(other);
		}
		partial void BeforeCopyProperties(QuickNSmart.Connector.Contracts.Modules.Account.ILogin other, ref bool handled);
		partial void AfterCopyProperties(QuickNSmart.Connector.Contracts.Modules.Account.ILogin other);
	}
	partial class Login : Models.WrapperModel
	{
	}
}
namespace QuickNSmart.Connector.Models.Modules.Account
{
	partial class LoginUser : QuickNSmart.Connector.Contracts.Modules.Account.ILoginUser
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
		public QuickNSmart.Contracts.Modules.Account.ILoginUser DelegateObject
		{
			get;
			set;
		}
		public System.String Email
		{
			get
			{
				OnEmailReading();
				return DelegateObject.Email;
			}
			set
			{
				DelegateObject.Email = value;
				OnEmailChanged();
			}
		}
		partial void OnEmailReading();
		partial void OnEmailChanged();
		public System.DateTime LoginTime
		{
			get
			{
				OnLoginTimeReading();
				return DelegateObject.LoginTime;
			}
			set
			{
				DelegateObject.LoginTime = value;
				OnLoginTimeChanged();
			}
		}
		partial void OnLoginTimeReading();
		partial void OnLoginTimeChanged();
		public System.DateTime? LogoutTime
		{
			get
			{
				OnLogoutTimeReading();
				return DelegateObject.LogoutTime;
			}
			set
			{
				DelegateObject.LogoutTime = value;
				OnLogoutTimeChanged();
			}
		}
		partial void OnLogoutTimeReading();
		partial void OnLogoutTimeChanged();
		public System.String Name
		{
			get
			{
				OnNameReading();
				return DelegateObject.Name;
			}
			set
			{
				DelegateObject.Name = value;
				OnNameChanged();
			}
		}
		partial void OnNameReading();
		partial void OnNameChanged();
		public QuickNSmart.Contracts.Modules.Common.State State
		{
			get
			{
				OnStateReading();
				return DelegateObject.State;
			}
			set
			{
				DelegateObject.State = value;
				OnStateChanged();
			}
		}
		partial void OnStateReading();
		partial void OnStateChanged();
		public void CopyProperties(QuickNSmart.Connector.Contracts.Modules.Account.ILoginUser other)
		{
			if (other == null)
			{
				throw new System.ArgumentNullException(nameof(other));
			}
			bool handled = false;
			BeforeCopyProperties(other, ref handled);
			if (handled == false)
			{
				DelegateObject.CopyProperties(other as QuickNSmart.Contracts.Modules.Account.ILoginUser);
			}
			AfterCopyProperties(other);
		}
		partial void BeforeCopyProperties(QuickNSmart.Connector.Contracts.Modules.Account.ILoginUser other, ref bool handled);
		partial void AfterCopyProperties(QuickNSmart.Connector.Contracts.Modules.Account.ILoginUser other);
	}
	partial class LoginUser : Models.WrapperModel
	{
	}
}
