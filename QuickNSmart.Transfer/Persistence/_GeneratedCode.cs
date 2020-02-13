namespace QuickNSmart.Transfer.Persistence.Account
{
	using System.Text.Json.Serialization;
	public partial class LoginSession : QuickNSmart.Contracts.Persistence.Account.ILoginSession
	{
		static LoginSession()
		{
			ClassConstructing();
			ClassConstructed();
		}
		static partial void ClassConstructing();
		static partial void ClassConstructed();
		public LoginSession()
		{
			Constructing();
			Constructed();
		}
		partial void Constructing();
		partial void Constructed();
		public System.Int32 UserId
		{
			get
			{
				OnUserIdReading();
				return _userId;
			}
			set
			{
				bool handled = false;
				OnUserIdChanging(ref handled, ref _userId);
				if (handled == false)
				{
					this._userId = value;
				}
				OnUserIdChanged();
			}
		}
		private System.Int32 _userId;
		partial void OnUserIdReading();
		partial void OnUserIdChanging(ref bool handled, ref System.Int32 _userId);
		partial void OnUserIdChanged();
		public System.String SessionToken
		{
			get
			{
				OnSessionTokenReading();
				return _sessionToken;
			}
			set
			{
				bool handled = false;
				OnSessionTokenChanging(ref handled, ref _sessionToken);
				if (handled == false)
				{
					this._sessionToken = value;
				}
				OnSessionTokenChanged();
			}
		}
		private System.String _sessionToken;
		partial void OnSessionTokenReading();
		partial void OnSessionTokenChanging(ref bool handled, ref System.String _sessionToken);
		partial void OnSessionTokenChanged();
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
		public System.DateTime LastAccess
		{
			get
			{
				OnLastAccessReading();
				return _lastAccess;
			}
			set
			{
				bool handled = false;
				OnLastAccessChanging(ref handled, ref _lastAccess);
				if (handled == false)
				{
					this._lastAccess = value;
				}
				OnLastAccessChanged();
			}
		}
		private System.DateTime _lastAccess;
		partial void OnLastAccessReading();
		partial void OnLastAccessChanging(ref bool handled, ref System.DateTime _lastAccess);
		partial void OnLastAccessChanged();
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
		public void CopyProperties(QuickNSmart.Contracts.Persistence.Account.ILoginSession other)
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
				UserId = other.UserId;
				SessionToken = other.SessionToken;
				LoginTime = other.LoginTime;
				LastAccess = other.LastAccess;
				LogoutTime = other.LogoutTime;
			}
			AfterCopyProperties(other);
		}
		partial void BeforeCopyProperties(QuickNSmart.Contracts.Persistence.Account.ILoginSession other, ref bool handled);
		partial void AfterCopyProperties(QuickNSmart.Contracts.Persistence.Account.ILoginSession other);
	}
}
namespace QuickNSmart.Transfer.Persistence.Account
{
	partial class LoginSession : TransferObject
	{
	}
}
namespace QuickNSmart.Transfer.Persistence.Account
{
	using System.Text.Json.Serialization;
	public partial class Role : QuickNSmart.Contracts.Persistence.Account.IRole
	{
		static Role()
		{
			ClassConstructing();
			ClassConstructed();
		}
		static partial void ClassConstructing();
		static partial void ClassConstructed();
		public Role()
		{
			Constructing();
			Constructed();
		}
		partial void Constructing();
		partial void Constructed();
		public System.String Designation
		{
			get
			{
				OnDesignationReading();
				return _designation;
			}
			set
			{
				bool handled = false;
				OnDesignationChanging(ref handled, ref _designation);
				if (handled == false)
				{
					this._designation = value;
				}
				OnDesignationChanged();
			}
		}
		private System.String _designation;
		partial void OnDesignationReading();
		partial void OnDesignationChanging(ref bool handled, ref System.String _designation);
		partial void OnDesignationChanged();
		public System.String Description
		{
			get
			{
				OnDescriptionReading();
				return _description;
			}
			set
			{
				bool handled = false;
				OnDescriptionChanging(ref handled, ref _description);
				if (handled == false)
				{
					this._description = value;
				}
				OnDescriptionChanged();
			}
		}
		private System.String _description;
		partial void OnDescriptionReading();
		partial void OnDescriptionChanging(ref bool handled, ref System.String _description);
		partial void OnDescriptionChanged();
		public void CopyProperties(QuickNSmart.Contracts.Persistence.Account.IRole other)
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
				Designation = other.Designation;
				Description = other.Description;
			}
			AfterCopyProperties(other);
		}
		partial void BeforeCopyProperties(QuickNSmart.Contracts.Persistence.Account.IRole other, ref bool handled);
		partial void AfterCopyProperties(QuickNSmart.Contracts.Persistence.Account.IRole other);
	}
}
namespace QuickNSmart.Transfer.Persistence.Account
{
	partial class Role : TransferObject
	{
	}
}
namespace QuickNSmart.Transfer.Persistence.Account
{
	using System.Text.Json.Serialization;
	public partial class User : QuickNSmart.Contracts.Persistence.Account.IUser
	{
		static User()
		{
			ClassConstructing();
			ClassConstructed();
		}
		static partial void ClassConstructing();
		static partial void ClassConstructed();
		public User()
		{
			Constructing();
			Constructed();
		}
		partial void Constructing();
		partial void Constructed();
		public System.String UserName
		{
			get
			{
				OnUserNameReading();
				return _userName;
			}
			set
			{
				bool handled = false;
				OnUserNameChanging(ref handled, ref _userName);
				if (handled == false)
				{
					this._userName = value;
				}
				OnUserNameChanged();
			}
		}
		private System.String _userName;
		partial void OnUserNameReading();
		partial void OnUserNameChanging(ref bool handled, ref System.String _userName);
		partial void OnUserNameChanged();
		public System.String Password
		{
			get
			{
				OnPasswordReading();
				return _password;
			}
			set
			{
				bool handled = false;
				OnPasswordChanging(ref handled, ref _password);
				if (handled == false)
				{
					this._password = value;
				}
				OnPasswordChanged();
			}
		}
		private System.String _password;
		partial void OnPasswordReading();
		partial void OnPasswordChanging(ref bool handled, ref System.String _password);
		partial void OnPasswordChanged();
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
		public System.String FirstName
		{
			get
			{
				OnFirstNameReading();
				return _firstName;
			}
			set
			{
				bool handled = false;
				OnFirstNameChanging(ref handled, ref _firstName);
				if (handled == false)
				{
					this._firstName = value;
				}
				OnFirstNameChanged();
			}
		}
		private System.String _firstName;
		partial void OnFirstNameReading();
		partial void OnFirstNameChanging(ref bool handled, ref System.String _firstName);
		partial void OnFirstNameChanged();
		public System.String LastName
		{
			get
			{
				OnLastNameReading();
				return _lastName;
			}
			set
			{
				bool handled = false;
				OnLastNameChanging(ref handled, ref _lastName);
				if (handled == false)
				{
					this._lastName = value;
				}
				OnLastNameChanged();
			}
		}
		private System.String _lastName;
		partial void OnLastNameReading();
		partial void OnLastNameChanging(ref bool handled, ref System.String _lastName);
		partial void OnLastNameChanged();
		public System.String PhoneNumber
		{
			get
			{
				OnPhoneNumberReading();
				return _phoneNumber;
			}
			set
			{
				bool handled = false;
				OnPhoneNumberChanging(ref handled, ref _phoneNumber);
				if (handled == false)
				{
					this._phoneNumber = value;
				}
				OnPhoneNumberChanged();
			}
		}
		private System.String _phoneNumber;
		partial void OnPhoneNumberReading();
		partial void OnPhoneNumberChanging(ref bool handled, ref System.String _phoneNumber);
		partial void OnPhoneNumberChanged();
		public System.Byte[] Avatar
		{
			get
			{
				OnAvatarReading();
				return _avatar;
			}
			set
			{
				bool handled = false;
				OnAvatarChanging(ref handled, ref _avatar);
				if (handled == false)
				{
					this._avatar = value;
				}
				OnAvatarChanged();
			}
		}
		private System.Byte[] _avatar;
		partial void OnAvatarReading();
		partial void OnAvatarChanging(ref bool handled, ref System.Byte[] _avatar);
		partial void OnAvatarChanged();
		public System.String AvatarMimeType
		{
			get
			{
				OnAvatarMimeTypeReading();
				return _avatarMimeType;
			}
			set
			{
				bool handled = false;
				OnAvatarMimeTypeChanging(ref handled, ref _avatarMimeType);
				if (handled == false)
				{
					this._avatarMimeType = value;
				}
				OnAvatarMimeTypeChanged();
			}
		}
		private System.String _avatarMimeType;
		partial void OnAvatarMimeTypeReading();
		partial void OnAvatarMimeTypeChanging(ref bool handled, ref System.String _avatarMimeType);
		partial void OnAvatarMimeTypeChanged();
		public QuickNSmart.Contracts.State State
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
		private QuickNSmart.Contracts.State _state;
		partial void OnStateReading();
		partial void OnStateChanging(ref bool handled, ref QuickNSmart.Contracts.State _state);
		partial void OnStateChanged();
		public void CopyProperties(QuickNSmart.Contracts.Persistence.Account.IUser other)
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
				UserName = other.UserName;
				Password = other.Password;
				Email = other.Email;
				FirstName = other.FirstName;
				LastName = other.LastName;
				PhoneNumber = other.PhoneNumber;
				Avatar = other.Avatar;
				AvatarMimeType = other.AvatarMimeType;
				State = other.State;
			}
			AfterCopyProperties(other);
		}
		partial void BeforeCopyProperties(QuickNSmart.Contracts.Persistence.Account.IUser other, ref bool handled);
		partial void AfterCopyProperties(QuickNSmart.Contracts.Persistence.Account.IUser other);
	}
}
namespace QuickNSmart.Transfer.Persistence.Account
{
	partial class User : TransferObject
	{
	}
}
namespace QuickNSmart.Transfer.Persistence.Account
{
	using System.Text.Json.Serialization;
	public partial class UserXRole : QuickNSmart.Contracts.Persistence.Account.IUserXRole
	{
		static UserXRole()
		{
			ClassConstructing();
			ClassConstructed();
		}
		static partial void ClassConstructing();
		static partial void ClassConstructed();
		public UserXRole()
		{
			Constructing();
			Constructed();
		}
		partial void Constructing();
		partial void Constructed();
		public System.Int32 UserId
		{
			get
			{
				OnUserIdReading();
				return _userId;
			}
			set
			{
				bool handled = false;
				OnUserIdChanging(ref handled, ref _userId);
				if (handled == false)
				{
					this._userId = value;
				}
				OnUserIdChanged();
			}
		}
		private System.Int32 _userId;
		partial void OnUserIdReading();
		partial void OnUserIdChanging(ref bool handled, ref System.Int32 _userId);
		partial void OnUserIdChanged();
		public System.Int32 RoleId
		{
			get
			{
				OnRoleIdReading();
				return _roleId;
			}
			set
			{
				bool handled = false;
				OnRoleIdChanging(ref handled, ref _roleId);
				if (handled == false)
				{
					this._roleId = value;
				}
				OnRoleIdChanged();
			}
		}
		private System.Int32 _roleId;
		partial void OnRoleIdReading();
		partial void OnRoleIdChanging(ref bool handled, ref System.Int32 _roleId);
		partial void OnRoleIdChanged();
		public void CopyProperties(QuickNSmart.Contracts.Persistence.Account.IUserXRole other)
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
				UserId = other.UserId;
				RoleId = other.RoleId;
			}
			AfterCopyProperties(other);
		}
		partial void BeforeCopyProperties(QuickNSmart.Contracts.Persistence.Account.IUserXRole other, ref bool handled);
		partial void AfterCopyProperties(QuickNSmart.Contracts.Persistence.Account.IUserXRole other);
	}
}
namespace QuickNSmart.Transfer.Persistence.Account
{
	partial class UserXRole : TransferObject
	{
	}
}
