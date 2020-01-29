namespace QuickNSmart.Logic.Entities.Persistence.Account
{
	partial class Application : QuickNSmart.Contracts.Persistence.Account.IApplication
	{
		static Application()
		{
			ClassConstructing();
			ClassConstructed();
		}
		static partial void ClassConstructing();
		static partial void ClassConstructed();
		public Application()
		{
			Constructing();
			Constructed();
		}
		partial void Constructing();
		partial void Constructed();
		private System.Byte[] _timestamp;
		public System.Byte[] Timestamp
		{
			get
			{
				OnTimestampReading();
				return _timestamp;
			}
			set
			{
				bool handled = false;
				OnTimestampChanging(ref handled, ref _timestamp);
				if (handled == false)
				{
					this._timestamp = value;
				}
				OnTimestampChanged();
			}
		}
		partial void OnTimestampReading();
		partial void OnTimestampChanging(ref bool handled, ref System.Byte[] _timestamp);
		partial void OnTimestampChanged();
		private System.String _name;
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
		partial void OnNameReading();
		partial void OnNameChanging(ref bool handled, ref System.String _name);
		partial void OnNameChanged();
		private System.String _token;
		public System.String Token
		{
			get
			{
				OnTokenReading();
				return _token;
			}
			set
			{
				bool handled = false;
				OnTokenChanging(ref handled, ref _token);
				if (handled == false)
				{
					this._token = value;
				}
				OnTokenChanged();
			}
		}
		partial void OnTokenReading();
		partial void OnTokenChanging(ref bool handled, ref System.String _token);
		partial void OnTokenChanged();
		private QuickNSmart.Contracts.State _state;
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
		partial void OnStateReading();
		partial void OnStateChanging(ref bool handled, ref QuickNSmart.Contracts.State _state);
		partial void OnStateChanged();
		public void CopyProperties(QuickNSmart.Contracts.Persistence.Account.IApplication other)
		{
			if (other == null)
			{
				throw new System.ArgumentNullException(nameof(other));
			}
			BeforeCopyProperties(other);
			Id = other.Id;
			Timestamp = other.Timestamp;
			Name = other.Name;
			Token = other.Token;
			State = other.State;
			AfterCopyProperties(other);
		}
		partial void BeforeCopyProperties(QuickNSmart.Contracts.Persistence.Account.IApplication other);
		partial void AfterCopyProperties(QuickNSmart.Contracts.Persistence.Account.IApplication other);
	}
}
namespace QuickNSmart.Logic.Entities.Persistence.Account
{
	partial class Application : IdentityObject
	{
	}
}
namespace QuickNSmart.Logic.Entities.Persistence.Account
{
	partial class Application
	{
	}
}
namespace QuickNSmart.Logic.Entities.Persistence.Account
{
	partial class User : QuickNSmart.Contracts.Persistence.Account.IUser
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
		private System.Byte[] _timestamp;
		public System.Byte[] Timestamp
		{
			get
			{
				OnTimestampReading();
				return _timestamp;
			}
			set
			{
				bool handled = false;
				OnTimestampChanging(ref handled, ref _timestamp);
				if (handled == false)
				{
					this._timestamp = value;
				}
				OnTimestampChanged();
			}
		}
		partial void OnTimestampReading();
		partial void OnTimestampChanging(ref bool handled, ref System.Byte[] _timestamp);
		partial void OnTimestampChanged();
		private System.String _userName;
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
		partial void OnUserNameReading();
		partial void OnUserNameChanging(ref bool handled, ref System.String _userName);
		partial void OnUserNameChanged();
		private System.String _password;
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
		partial void OnPasswordReading();
		partial void OnPasswordChanging(ref bool handled, ref System.String _password);
		partial void OnPasswordChanged();
		private System.String _email;
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
		partial void OnEmailReading();
		partial void OnEmailChanging(ref bool handled, ref System.String _email);
		partial void OnEmailChanged();
		private System.String _firstName;
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
		partial void OnFirstNameReading();
		partial void OnFirstNameChanging(ref bool handled, ref System.String _firstName);
		partial void OnFirstNameChanged();
		private System.String _lastName;
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
		partial void OnLastNameReading();
		partial void OnLastNameChanging(ref bool handled, ref System.String _lastName);
		partial void OnLastNameChanged();
		private System.String _fullName;
		public System.String FullName
		{
			get
			{
				OnFullNameReading();
				return _fullName;
			}
			set
			{
				bool handled = false;
				OnFullNameChanging(ref handled, ref _fullName);
				if (handled == false)
				{
					this._fullName = value;
				}
				OnFullNameChanged();
			}
		}
		partial void OnFullNameReading();
		partial void OnFullNameChanging(ref bool handled, ref System.String _fullName);
		partial void OnFullNameChanged();
		private System.String _phoneNumber;
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
		partial void OnPhoneNumberReading();
		partial void OnPhoneNumberChanging(ref bool handled, ref System.String _phoneNumber);
		partial void OnPhoneNumberChanged();
		private System.Byte[] _avatar;
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
		partial void OnAvatarReading();
		partial void OnAvatarChanging(ref bool handled, ref System.Byte[] _avatar);
		partial void OnAvatarChanged();
		private System.String _avatarMimeType;
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
		partial void OnAvatarMimeTypeReading();
		partial void OnAvatarMimeTypeChanging(ref bool handled, ref System.String _avatarMimeType);
		partial void OnAvatarMimeTypeChanged();
		private QuickNSmart.Contracts.State _state;
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
		partial void OnStateReading();
		partial void OnStateChanging(ref bool handled, ref QuickNSmart.Contracts.State _state);
		partial void OnStateChanged();
		public void CopyProperties(QuickNSmart.Contracts.Persistence.Account.IUser other)
		{
			if (other == null)
			{
				throw new System.ArgumentNullException(nameof(other));
			}
			BeforeCopyProperties(other);
			Id = other.Id;
			Timestamp = other.Timestamp;
			UserName = other.UserName;
			Password = other.Password;
			Email = other.Email;
			FirstName = other.FirstName;
			LastName = other.LastName;
			FullName = other.FullName;
			PhoneNumber = other.PhoneNumber;
			Avatar = other.Avatar;
			AvatarMimeType = other.AvatarMimeType;
			State = other.State;
			AfterCopyProperties(other);
		}
		partial void BeforeCopyProperties(QuickNSmart.Contracts.Persistence.Account.IUser other);
		partial void AfterCopyProperties(QuickNSmart.Contracts.Persistence.Account.IUser other);
	}
}
namespace QuickNSmart.Logic.Entities.Persistence.Account
{
	partial class User : IdentityObject
	{
	}
}
namespace QuickNSmart.Logic.Entities.Persistence.Account
{
	partial class User
	{
	}
}
