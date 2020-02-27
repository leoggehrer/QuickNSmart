namespace QuickNSmart.Logic.Entities.Persistence.Account
{
	using System;
	partial class Client : QuickNSmart.Contracts.Persistence.Account.IClient
	{
		static Client()
		{
			ClassConstructing();
			ClassConstructed();
		}
		static partial void ClassConstructing();
		static partial void ClassConstructed();
		public Client()
		{
			Constructing();
			Constructed();
		}
		partial void Constructing();
		partial void Constructed();
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
		public void CopyProperties(QuickNSmart.Contracts.Persistence.Account.IClient other)
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
				Guid = other.Guid;
				Name = other.Name;
				Key = other.Key;
				State = other.State;
			}
			AfterCopyProperties(other);
		}
		partial void BeforeCopyProperties(QuickNSmart.Contracts.Persistence.Account.IClient other, ref bool handled);
		partial void AfterCopyProperties(QuickNSmart.Contracts.Persistence.Account.IClient other);
		public override bool Equals(object obj)
		{
			if (!(obj is QuickNSmart.Contracts.Persistence.Account.IClient instance))
			{
				return false;
			}
			return base.Equals(instance) && Equals(instance);
		}
		protected bool Equals(QuickNSmart.Contracts.Persistence.Account.IClient other)
		{
			if (other == null)
			{
				return false;
			}
			return Id == other.Id && IsEqualsWith(Timestamp, other.Timestamp) && IsEqualsWith(Guid, other.Guid) && IsEqualsWith(Name, other.Name) && IsEqualsWith(Key, other.Key) && State == other.State;
		}
		public override int GetHashCode()
		{
			return HashCode.Combine(Id, Timestamp, Guid, Name, Key, State);
		}
	}
}
namespace QuickNSmart.Logic.Entities.Persistence.Account
{
	partial class Client : IdentityObject
	{
	}
}
namespace QuickNSmart.Logic.Entities.Persistence.Account
{
	partial class Client
	{
	}
}
namespace QuickNSmart.Logic.Entities.Persistence.Account
{
	using System;
	partial class Identity : QuickNSmart.Contracts.Persistence.Account.IIdentity
	{
		static Identity()
		{
			ClassConstructing();
			ClassConstructed();
		}
		static partial void ClassConstructing();
		static partial void ClassConstructed();
		public Identity()
		{
			Constructing();
			Constructed();
		}
		partial void Constructing();
		partial void Constructed();
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
		public void CopyProperties(QuickNSmart.Contracts.Persistence.Account.IIdentity other)
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
				Guid = other.Guid;
				Name = other.Name;
				Email = other.Email;
				Password = other.Password;
				State = other.State;
			}
			AfterCopyProperties(other);
		}
		partial void BeforeCopyProperties(QuickNSmart.Contracts.Persistence.Account.IIdentity other, ref bool handled);
		partial void AfterCopyProperties(QuickNSmart.Contracts.Persistence.Account.IIdentity other);
		public override bool Equals(object obj)
		{
			if (!(obj is QuickNSmart.Contracts.Persistence.Account.IIdentity instance))
			{
				return false;
			}
			return base.Equals(instance) && Equals(instance);
		}
		protected bool Equals(QuickNSmart.Contracts.Persistence.Account.IIdentity other)
		{
			if (other == null)
			{
				return false;
			}
			return Id == other.Id && IsEqualsWith(Timestamp, other.Timestamp) && IsEqualsWith(Guid, other.Guid) && IsEqualsWith(Name, other.Name) && IsEqualsWith(Email, other.Email) && IsEqualsWith(Password, other.Password) && State == other.State;
		}
		public override int GetHashCode()
		{
			return HashCode.Combine(Id, Timestamp, Guid, Name, Email, Password, HashCode.Combine(State));
		}
	}
}
namespace QuickNSmart.Logic.Entities.Persistence.Account
{
	partial class Identity : IdentityObject
	{
	}
}
namespace QuickNSmart.Logic.Entities.Persistence.Account
{
	partial class Identity
	{
		public System.Collections.Generic.ICollection<QuickNSmart.Logic.Entities.Persistence.Account.IdentityXApplication> IdentityXApplications
		{
			get;
			set;
		}
		public System.Collections.Generic.ICollection<QuickNSmart.Logic.Entities.Persistence.Account.IdentityXRole> IdentityXRoles
		{
			get;
			set;
		}
	}
}
namespace QuickNSmart.Logic.Entities.Persistence.Account
{
	using System;
	partial class IdentityXApplication : QuickNSmart.Contracts.Persistence.Account.IIdentityXApplication
	{
		static IdentityXApplication()
		{
			ClassConstructing();
			ClassConstructed();
		}
		static partial void ClassConstructing();
		static partial void ClassConstructed();
		public IdentityXApplication()
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
		public System.Int32 ApplicationId
		{
			get
			{
				OnApplicationIdReading();
				return _applicationId;
			}
			set
			{
				bool handled = false;
				OnApplicationIdChanging(ref handled, ref _applicationId);
				if (handled == false)
				{
					this._applicationId = value;
				}
				OnApplicationIdChanged();
			}
		}
		private System.Int32 _applicationId;
		partial void OnApplicationIdReading();
		partial void OnApplicationIdChanging(ref bool handled, ref System.Int32 _applicationId);
		partial void OnApplicationIdChanged();
		public void CopyProperties(QuickNSmart.Contracts.Persistence.Account.IIdentityXApplication other)
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
				IdentityId = other.IdentityId;
				ApplicationId = other.ApplicationId;
			}
			AfterCopyProperties(other);
		}
		partial void BeforeCopyProperties(QuickNSmart.Contracts.Persistence.Account.IIdentityXApplication other, ref bool handled);
		partial void AfterCopyProperties(QuickNSmart.Contracts.Persistence.Account.IIdentityXApplication other);
		public override bool Equals(object obj)
		{
			if (!(obj is QuickNSmart.Contracts.Persistence.Account.IIdentityXApplication instance))
			{
				return false;
			}
			return base.Equals(instance) && Equals(instance);
		}
		protected bool Equals(QuickNSmart.Contracts.Persistence.Account.IIdentityXApplication other)
		{
			if (other == null)
			{
				return false;
			}
			return Id == other.Id && IsEqualsWith(Timestamp, other.Timestamp) && IdentityId == other.IdentityId && ApplicationId == other.ApplicationId;
		}
		public override int GetHashCode()
		{
			return HashCode.Combine(Id, Timestamp, IdentityId, ApplicationId);
		}
	}
}
namespace QuickNSmart.Logic.Entities.Persistence.Account
{
	partial class IdentityXApplication : IdentityObject
	{
	}
}
namespace QuickNSmart.Logic.Entities.Persistence.Account
{
	partial class IdentityXApplication
	{
		public QuickNSmart.Logic.Entities.Persistence.Account.Identity Identity
		{
			get;
			set;
		}
	}
}
namespace QuickNSmart.Logic.Entities.Persistence.Account
{
	using System;
	partial class IdentityXRole : QuickNSmart.Contracts.Persistence.Account.IIdentityXRole
	{
		static IdentityXRole()
		{
			ClassConstructing();
			ClassConstructed();
		}
		static partial void ClassConstructing();
		static partial void ClassConstructed();
		public IdentityXRole()
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
		public void CopyProperties(QuickNSmart.Contracts.Persistence.Account.IIdentityXRole other)
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
				IdentityId = other.IdentityId;
				RoleId = other.RoleId;
			}
			AfterCopyProperties(other);
		}
		partial void BeforeCopyProperties(QuickNSmart.Contracts.Persistence.Account.IIdentityXRole other, ref bool handled);
		partial void AfterCopyProperties(QuickNSmart.Contracts.Persistence.Account.IIdentityXRole other);
		public override bool Equals(object obj)
		{
			if (!(obj is QuickNSmart.Contracts.Persistence.Account.IIdentityXRole instance))
			{
				return false;
			}
			return base.Equals(instance) && Equals(instance);
		}
		protected bool Equals(QuickNSmart.Contracts.Persistence.Account.IIdentityXRole other)
		{
			if (other == null)
			{
				return false;
			}
			return Id == other.Id && IsEqualsWith(Timestamp, other.Timestamp) && IdentityId == other.IdentityId && RoleId == other.RoleId;
		}
		public override int GetHashCode()
		{
			return HashCode.Combine(Id, Timestamp, IdentityId, RoleId);
		}
	}
}
namespace QuickNSmart.Logic.Entities.Persistence.Account
{
	partial class IdentityXRole : IdentityObject
	{
	}
}
namespace QuickNSmart.Logic.Entities.Persistence.Account
{
	partial class IdentityXRole
	{
		public QuickNSmart.Logic.Entities.Persistence.Account.Identity Identity
		{
			get;
			set;
		}
		public QuickNSmart.Logic.Entities.Persistence.Account.Role Role
		{
			get;
			set;
		}
	}
}
namespace QuickNSmart.Logic.Entities.Persistence.Account
{
	using System;
	partial class LoginSession : QuickNSmart.Contracts.Persistence.Account.ILoginSession
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
		public override bool Equals(object obj)
		{
			if (!(obj is QuickNSmart.Contracts.Persistence.Account.ILoginSession instance))
			{
				return false;
			}
			return base.Equals(instance) && Equals(instance);
		}
		protected bool Equals(QuickNSmart.Contracts.Persistence.Account.ILoginSession other)
		{
			if (other == null)
			{
				return false;
			}
			return Id == other.Id && IsEqualsWith(Timestamp, other.Timestamp) && UserId == other.UserId && IsEqualsWith(SessionToken, other.SessionToken) && LoginTime == other.LoginTime && LastAccess == other.LastAccess && LogoutTime == other.LogoutTime;
		}
		public override int GetHashCode()
		{
			return HashCode.Combine(Id, Timestamp, UserId, SessionToken, LoginTime, LastAccess, HashCode.Combine(LogoutTime));
		}
	}
}
namespace QuickNSmart.Logic.Entities.Persistence.Account
{
	partial class LoginSession : IdentityObject
	{
	}
}
namespace QuickNSmart.Logic.Entities.Persistence.Account
{
	partial class LoginSession
	{
		public QuickNSmart.Logic.Entities.Persistence.Account.User User
		{
			get;
			set;
		}
	}
}
namespace QuickNSmart.Logic.Entities.Persistence.Account
{
	using System;
	partial class Role : QuickNSmart.Contracts.Persistence.Account.IRole
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
		public override bool Equals(object obj)
		{
			if (!(obj is QuickNSmart.Contracts.Persistence.Account.IRole instance))
			{
				return false;
			}
			return base.Equals(instance) && Equals(instance);
		}
		protected bool Equals(QuickNSmart.Contracts.Persistence.Account.IRole other)
		{
			if (other == null)
			{
				return false;
			}
			return Id == other.Id && IsEqualsWith(Timestamp, other.Timestamp) && IsEqualsWith(Designation, other.Designation) && IsEqualsWith(Description, other.Description);
		}
		public override int GetHashCode()
		{
			return HashCode.Combine(Id, Timestamp, Designation, Description);
		}
	}
}
namespace QuickNSmart.Logic.Entities.Persistence.Account
{
	partial class Role : IdentityObject
	{
	}
}
namespace QuickNSmart.Logic.Entities.Persistence.Account
{
	partial class Role
	{
		public System.Collections.Generic.ICollection<QuickNSmart.Logic.Entities.Persistence.Account.IdentityXRole> IdentityXRoles
		{
			get;
			set;
		}
		public System.Collections.Generic.ICollection<QuickNSmart.Logic.Entities.Persistence.Account.UserXRole> UserXRoles
		{
			get;
			set;
		}
	}
}
namespace QuickNSmart.Logic.Entities.Persistence.Account
{
	using System;
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
		public override bool Equals(object obj)
		{
			if (!(obj is QuickNSmart.Contracts.Persistence.Account.IUser instance))
			{
				return false;
			}
			return base.Equals(instance) && Equals(instance);
		}
		protected bool Equals(QuickNSmart.Contracts.Persistence.Account.IUser other)
		{
			if (other == null)
			{
				return false;
			}
			return Id == other.Id && IsEqualsWith(Timestamp, other.Timestamp) && IsEqualsWith(UserName, other.UserName) && IsEqualsWith(Password, other.Password) && IsEqualsWith(Email, other.Email) && IsEqualsWith(FirstName, other.FirstName) && IsEqualsWith(LastName, other.LastName) && IsEqualsWith(PhoneNumber, other.PhoneNumber) && IsEqualsWith(Avatar, other.Avatar) && IsEqualsWith(AvatarMimeType, other.AvatarMimeType) && State == other.State;
		}
		public override int GetHashCode()
		{
			return HashCode.Combine(Id, Timestamp, UserName, Password, Email, FirstName, HashCode.Combine(LastName, PhoneNumber, Avatar, AvatarMimeType, State));
		}
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
		public System.Collections.Generic.ICollection<QuickNSmart.Logic.Entities.Persistence.Account.LoginSession> LoginSessions
		{
			get;
			set;
		}
		public System.Collections.Generic.ICollection<QuickNSmart.Logic.Entities.Persistence.Account.UserXRole> UserXRoles
		{
			get;
			set;
		}
	}
}
namespace QuickNSmart.Logic.Entities.Persistence.Account
{
	using System;
	partial class UserXRole : QuickNSmart.Contracts.Persistence.Account.IUserXRole
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
		public override bool Equals(object obj)
		{
			if (!(obj is QuickNSmart.Contracts.Persistence.Account.IUserXRole instance))
			{
				return false;
			}
			return base.Equals(instance) && Equals(instance);
		}
		protected bool Equals(QuickNSmart.Contracts.Persistence.Account.IUserXRole other)
		{
			if (other == null)
			{
				return false;
			}
			return Id == other.Id && IsEqualsWith(Timestamp, other.Timestamp) && UserId == other.UserId && RoleId == other.RoleId;
		}
		public override int GetHashCode()
		{
			return HashCode.Combine(Id, Timestamp, UserId, RoleId);
		}
	}
}
namespace QuickNSmart.Logic.Entities.Persistence.Account
{
	partial class UserXRole : IdentityObject
	{
	}
}
namespace QuickNSmart.Logic.Entities.Persistence.Account
{
	partial class UserXRole
	{
		public QuickNSmart.Logic.Entities.Persistence.Account.User User
		{
			get;
			set;
		}
		public QuickNSmart.Logic.Entities.Persistence.Account.Role Role
		{
			get;
			set;
		}
	}
}
