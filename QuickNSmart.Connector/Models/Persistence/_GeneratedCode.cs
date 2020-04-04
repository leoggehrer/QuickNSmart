namespace QuickNSmart.Connector.Models.Persistence.Account
{
	partial class ActionLog : QuickNSmart.Connector.Contracts.Persistence.Account.IActionLog
	{
		static ActionLog()
		{
			ClassConstructing();
			ClassConstructed();
		}
		static partial void ClassConstructing();
		static partial void ClassConstructed();
		public ActionLog()
		{
			Constructing();
			Constructed();
		}
		partial void Constructing();
		partial void Constructed();
		public QuickNSmart.Contracts.Persistence.Account.IActionLog DelegateObject
		{
			get;
			set;
		}
		public System.Int32 IdentityId
		{
			get
			{
				OnIdentityIdReading();
				return DelegateObject.IdentityId;
			}
			set
			{
				DelegateObject.IdentityId = value;
				OnIdentityIdChanged();
			}
		}
		partial void OnIdentityIdReading();
		partial void OnIdentityIdChanged();
		public System.DateTime Time
		{
			get
			{
				OnTimeReading();
				return DelegateObject.Time;
			}
			set
			{
				DelegateObject.Time = value;
				OnTimeChanged();
			}
		}
		partial void OnTimeReading();
		partial void OnTimeChanged();
		public System.String Subject
		{
			get
			{
				OnSubjectReading();
				return DelegateObject.Subject;
			}
			set
			{
				DelegateObject.Subject = value;
				OnSubjectChanged();
			}
		}
		partial void OnSubjectReading();
		partial void OnSubjectChanged();
		public System.String Action
		{
			get
			{
				OnActionReading();
				return DelegateObject.Action;
			}
			set
			{
				DelegateObject.Action = value;
				OnActionChanged();
			}
		}
		partial void OnActionReading();
		partial void OnActionChanged();
		public System.String Info
		{
			get
			{
				OnInfoReading();
				return DelegateObject.Info;
			}
			set
			{
				DelegateObject.Info = value;
				OnInfoChanged();
			}
		}
		partial void OnInfoReading();
		partial void OnInfoChanged();
		public void CopyProperties(QuickNSmart.Connector.Contracts.Persistence.Account.IActionLog other)
		{
			if (other == null)
			{
				throw new System.ArgumentNullException(nameof(other));
			}
			bool handled = false;
			BeforeCopyProperties(other, ref handled);
			if (handled == false)
			{
				DelegateObject.CopyProperties(other as QuickNSmart.Contracts.Persistence.Account.IActionLog);
			}
			AfterCopyProperties(other);
		}
		partial void BeforeCopyProperties(QuickNSmart.Connector.Contracts.Persistence.Account.IActionLog other, ref bool handled);
		partial void AfterCopyProperties(QuickNSmart.Connector.Contracts.Persistence.Account.IActionLog other);
	}
	partial class ActionLog : Models.IdentityModel
	{
	}
}
namespace QuickNSmart.Connector.Models.Persistence.Account
{
	partial class Identity : QuickNSmart.Connector.Contracts.Persistence.Account.IIdentity
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
		public QuickNSmart.Contracts.Persistence.Account.IIdentity DelegateObject
		{
			get;
			set;
		}
		public System.String Guid
		{
			get
			{
				OnGuidReading();
				return DelegateObject.Guid;
			}
		}
		partial void OnGuidReading();
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
		public System.String Password
		{
			get
			{
				OnPasswordReading();
				return DelegateObject.Password;
			}
			set
			{
				DelegateObject.Password = value;
				OnPasswordChanged();
			}
		}
		partial void OnPasswordReading();
		partial void OnPasswordChanged();
		public System.Boolean EnableJwtAuth
		{
			get
			{
				OnEnableJwtAuthReading();
				return DelegateObject.EnableJwtAuth;
			}
			set
			{
				DelegateObject.EnableJwtAuth = value;
				OnEnableJwtAuthChanged();
			}
		}
		partial void OnEnableJwtAuthReading();
		partial void OnEnableJwtAuthChanged();
		public System.Int32 AccessFailedCount
		{
			get
			{
				OnAccessFailedCountReading();
				return DelegateObject.AccessFailedCount;
			}
			set
			{
				DelegateObject.AccessFailedCount = value;
				OnAccessFailedCountChanged();
			}
		}
		partial void OnAccessFailedCountReading();
		partial void OnAccessFailedCountChanged();
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
		public void CopyProperties(QuickNSmart.Connector.Contracts.Persistence.Account.IIdentity other)
		{
			if (other == null)
			{
				throw new System.ArgumentNullException(nameof(other));
			}
			bool handled = false;
			BeforeCopyProperties(other, ref handled);
			if (handled == false)
			{
				DelegateObject.CopyProperties(other as QuickNSmart.Contracts.Persistence.Account.IIdentity);
			}
			AfterCopyProperties(other);
		}
		partial void BeforeCopyProperties(QuickNSmart.Connector.Contracts.Persistence.Account.IIdentity other, ref bool handled);
		partial void AfterCopyProperties(QuickNSmart.Connector.Contracts.Persistence.Account.IIdentity other);
	}
	partial class Identity : Models.IdentityModel
	{
	}
}
namespace QuickNSmart.Connector.Models.Persistence.Account
{
	partial class IdentityXRole : QuickNSmart.Connector.Contracts.Persistence.Account.IIdentityXRole
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
		public QuickNSmart.Contracts.Persistence.Account.IIdentityXRole DelegateObject
		{
			get;
			set;
		}
		public System.Int32 IdentityId
		{
			get
			{
				OnIdentityIdReading();
				return DelegateObject.IdentityId;
			}
			set
			{
				DelegateObject.IdentityId = value;
				OnIdentityIdChanged();
			}
		}
		partial void OnIdentityIdReading();
		partial void OnIdentityIdChanged();
		public System.Int32 RoleId
		{
			get
			{
				OnRoleIdReading();
				return DelegateObject.RoleId;
			}
			set
			{
				DelegateObject.RoleId = value;
				OnRoleIdChanged();
			}
		}
		partial void OnRoleIdReading();
		partial void OnRoleIdChanged();
		public void CopyProperties(QuickNSmart.Connector.Contracts.Persistence.Account.IIdentityXRole other)
		{
			if (other == null)
			{
				throw new System.ArgumentNullException(nameof(other));
			}
			bool handled = false;
			BeforeCopyProperties(other, ref handled);
			if (handled == false)
			{
				DelegateObject.CopyProperties(other as QuickNSmart.Contracts.Persistence.Account.IIdentityXRole);
			}
			AfterCopyProperties(other);
		}
		partial void BeforeCopyProperties(QuickNSmart.Connector.Contracts.Persistence.Account.IIdentityXRole other, ref bool handled);
		partial void AfterCopyProperties(QuickNSmart.Connector.Contracts.Persistence.Account.IIdentityXRole other);
	}
	partial class IdentityXRole : Models.IdentityModel
	{
	}
}
namespace QuickNSmart.Connector.Models.Persistence.Account
{
	partial class LoginSession : QuickNSmart.Connector.Contracts.Persistence.Account.ILoginSession
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
		public QuickNSmart.Contracts.Persistence.Account.ILoginSession DelegateObject
		{
			get;
			set;
		}
		public System.Int32 IdentityId
		{
			get
			{
				OnIdentityIdReading();
				return DelegateObject.IdentityId;
			}
		}
		partial void OnIdentityIdReading();
		public System.Boolean IsRemoteAuth
		{
			get
			{
				OnIsRemoteAuthReading();
				return DelegateObject.IsRemoteAuth;
			}
		}
		partial void OnIsRemoteAuthReading();
		public System.String Origin
		{
			get
			{
				OnOriginReading();
				return DelegateObject.Origin;
			}
		}
		partial void OnOriginReading();
		public System.String Name
		{
			get
			{
				OnNameReading();
				return DelegateObject.Name;
			}
		}
		partial void OnNameReading();
		public System.String Email
		{
			get
			{
				OnEmailReading();
				return DelegateObject.Email;
			}
		}
		partial void OnEmailReading();
		public System.String JsonWebToken
		{
			get
			{
				OnJsonWebTokenReading();
				return DelegateObject.JsonWebToken;
			}
		}
		partial void OnJsonWebTokenReading();
		public System.String SessionToken
		{
			get
			{
				OnSessionTokenReading();
				return DelegateObject.SessionToken;
			}
		}
		partial void OnSessionTokenReading();
		public System.DateTime LoginTime
		{
			get
			{
				OnLoginTimeReading();
				return DelegateObject.LoginTime;
			}
		}
		partial void OnLoginTimeReading();
		public System.DateTime LastAccess
		{
			get
			{
				OnLastAccessReading();
				return DelegateObject.LastAccess;
			}
		}
		partial void OnLastAccessReading();
		public System.DateTime? LogoutTime
		{
			get
			{
				OnLogoutTimeReading();
				return DelegateObject.LogoutTime;
			}
		}
		partial void OnLogoutTimeReading();
		public void CopyProperties(QuickNSmart.Connector.Contracts.Persistence.Account.ILoginSession other)
		{
			if (other == null)
			{
				throw new System.ArgumentNullException(nameof(other));
			}
			bool handled = false;
			BeforeCopyProperties(other, ref handled);
			if (handled == false)
			{
				DelegateObject.CopyProperties(other as QuickNSmart.Contracts.Persistence.Account.ILoginSession);
			}
			AfterCopyProperties(other);
		}
		partial void BeforeCopyProperties(QuickNSmart.Connector.Contracts.Persistence.Account.ILoginSession other, ref bool handled);
		partial void AfterCopyProperties(QuickNSmart.Connector.Contracts.Persistence.Account.ILoginSession other);
	}
	partial class LoginSession : Models.IdentityModel
	{
	}
}
namespace QuickNSmart.Connector.Models.Persistence.Account
{
	partial class Role : QuickNSmart.Connector.Contracts.Persistence.Account.IRole
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
		public QuickNSmart.Contracts.Persistence.Account.IRole DelegateObject
		{
			get;
			set;
		}
		public System.String Designation
		{
			get
			{
				OnDesignationReading();
				return DelegateObject.Designation;
			}
			set
			{
				DelegateObject.Designation = value;
				OnDesignationChanged();
			}
		}
		partial void OnDesignationReading();
		partial void OnDesignationChanged();
		public System.String Description
		{
			get
			{
				OnDescriptionReading();
				return DelegateObject.Description;
			}
			set
			{
				DelegateObject.Description = value;
				OnDescriptionChanged();
			}
		}
		partial void OnDescriptionReading();
		partial void OnDescriptionChanged();
		public void CopyProperties(QuickNSmart.Connector.Contracts.Persistence.Account.IRole other)
		{
			if (other == null)
			{
				throw new System.ArgumentNullException(nameof(other));
			}
			bool handled = false;
			BeforeCopyProperties(other, ref handled);
			if (handled == false)
			{
				DelegateObject.CopyProperties(other as QuickNSmart.Contracts.Persistence.Account.IRole);
			}
			AfterCopyProperties(other);
		}
		partial void BeforeCopyProperties(QuickNSmart.Connector.Contracts.Persistence.Account.IRole other, ref bool handled);
		partial void AfterCopyProperties(QuickNSmart.Connector.Contracts.Persistence.Account.IRole other);
	}
	partial class Role : Models.IdentityModel
	{
	}
}
