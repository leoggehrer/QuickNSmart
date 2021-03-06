namespace QuickNSmart.Transfer.Persistence.TestOneToMany
{
	using System.Text.Json.Serialization;
	public partial class Invoice : QuickNSmart.Contracts.Persistence.TestOneToMany.IInvoice
	{
		static Invoice()
		{
			ClassConstructing();
			ClassConstructed();
		}
		static partial void ClassConstructing();
		static partial void ClassConstructed();
		public Invoice()
		{
			Constructing();
			Constructed();
		}
		partial void Constructing();
		partial void Constructed();
		public System.DateTime Date
		{
			get
			{
				OnDateReading();
				return _date;
			}
			set
			{
				bool handled = false;
				OnDateChanging(ref handled, ref _date);
				if (handled == false)
				{
					this._date = value;
				}
				OnDateChanged();
			}
		}
		private System.DateTime _date;
		partial void OnDateReading();
		partial void OnDateChanging(ref bool handled, ref System.DateTime _date);
		partial void OnDateChanged();
		public System.String Subject
		{
			get
			{
				OnSubjectReading();
				return _subject;
			}
			set
			{
				bool handled = false;
				OnSubjectChanging(ref handled, ref _subject);
				if (handled == false)
				{
					this._subject = value;
				}
				OnSubjectChanged();
			}
		}
		private System.String _subject;
		partial void OnSubjectReading();
		partial void OnSubjectChanging(ref bool handled, ref System.String _subject);
		partial void OnSubjectChanged();
		public System.String Street
		{
			get
			{
				OnStreetReading();
				return _street;
			}
			set
			{
				bool handled = false;
				OnStreetChanging(ref handled, ref _street);
				if (handled == false)
				{
					this._street = value;
				}
				OnStreetChanged();
			}
		}
		private System.String _street;
		partial void OnStreetReading();
		partial void OnStreetChanging(ref bool handled, ref System.String _street);
		partial void OnStreetChanged();
		public System.String ZipCode
		{
			get
			{
				OnZipCodeReading();
				return _zipCode;
			}
			set
			{
				bool handled = false;
				OnZipCodeChanging(ref handled, ref _zipCode);
				if (handled == false)
				{
					this._zipCode = value;
				}
				OnZipCodeChanged();
			}
		}
		private System.String _zipCode;
		partial void OnZipCodeReading();
		partial void OnZipCodeChanging(ref bool handled, ref System.String _zipCode);
		partial void OnZipCodeChanged();
		public System.String City
		{
			get
			{
				OnCityReading();
				return _city;
			}
			set
			{
				bool handled = false;
				OnCityChanging(ref handled, ref _city);
				if (handled == false)
				{
					this._city = value;
				}
				OnCityChanged();
			}
		}
		private System.String _city;
		partial void OnCityReading();
		partial void OnCityChanging(ref bool handled, ref System.String _city);
		partial void OnCityChanged();
		public void CopyProperties(QuickNSmart.Contracts.Persistence.TestOneToMany.IInvoice other)
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
				Date = other.Date;
				Subject = other.Subject;
				Street = other.Street;
				ZipCode = other.ZipCode;
				City = other.City;
			}
			AfterCopyProperties(other);
		}
		partial void BeforeCopyProperties(QuickNSmart.Contracts.Persistence.TestOneToMany.IInvoice other, ref bool handled);
		partial void AfterCopyProperties(QuickNSmart.Contracts.Persistence.TestOneToMany.IInvoice other);
	}
}
namespace QuickNSmart.Transfer.Persistence.TestOneToMany
{
	partial class Invoice : IdentityModel
	{
	}
}
namespace QuickNSmart.Transfer.Persistence.TestOneToMany
{
	using System.Text.Json.Serialization;
	public partial class InvoiceDetail : QuickNSmart.Contracts.Persistence.TestOneToMany.IInvoiceDetail
	{
		static InvoiceDetail()
		{
			ClassConstructing();
			ClassConstructed();
		}
		static partial void ClassConstructing();
		static partial void ClassConstructed();
		public InvoiceDetail()
		{
			Constructing();
			Constructed();
		}
		partial void Constructing();
		partial void Constructed();
		public System.Int32 InvoiceId
		{
			get
			{
				OnInvoiceIdReading();
				return _invoiceId;
			}
			set
			{
				bool handled = false;
				OnInvoiceIdChanging(ref handled, ref _invoiceId);
				if (handled == false)
				{
					this._invoiceId = value;
				}
				OnInvoiceIdChanged();
			}
		}
		private System.Int32 _invoiceId;
		partial void OnInvoiceIdReading();
		partial void OnInvoiceIdChanging(ref bool handled, ref System.Int32 _invoiceId);
		partial void OnInvoiceIdChanged();
		public System.Int32 Order
		{
			get
			{
				OnOrderReading();
				return _order;
			}
			set
			{
				bool handled = false;
				OnOrderChanging(ref handled, ref _order);
				if (handled == false)
				{
					this._order = value;
				}
				OnOrderChanged();
			}
		}
		private System.Int32 _order;
		partial void OnOrderReading();
		partial void OnOrderChanging(ref bool handled, ref System.Int32 _order);
		partial void OnOrderChanged();
		public System.String Text
		{
			get
			{
				OnTextReading();
				return _text;
			}
			set
			{
				bool handled = false;
				OnTextChanging(ref handled, ref _text);
				if (handled == false)
				{
					this._text = value;
				}
				OnTextChanged();
			}
		}
		private System.String _text;
		partial void OnTextReading();
		partial void OnTextChanging(ref bool handled, ref System.String _text);
		partial void OnTextChanged();
		public System.Double Quantity
		{
			get
			{
				OnQuantityReading();
				return _quantity;
			}
			set
			{
				bool handled = false;
				OnQuantityChanging(ref handled, ref _quantity);
				if (handled == false)
				{
					this._quantity = value;
				}
				OnQuantityChanged();
			}
		}
		private System.Double _quantity;
		partial void OnQuantityReading();
		partial void OnQuantityChanging(ref bool handled, ref System.Double _quantity);
		partial void OnQuantityChanged();
		public System.Double Tax
		{
			get
			{
				OnTaxReading();
				return _tax;
			}
			set
			{
				bool handled = false;
				OnTaxChanging(ref handled, ref _tax);
				if (handled == false)
				{
					this._tax = value;
				}
				OnTaxChanged();
			}
		}
		private System.Double _tax;
		partial void OnTaxReading();
		partial void OnTaxChanging(ref bool handled, ref System.Double _tax);
		partial void OnTaxChanged();
		public System.Double Price
		{
			get
			{
				OnPriceReading();
				return _price;
			}
			set
			{
				bool handled = false;
				OnPriceChanging(ref handled, ref _price);
				if (handled == false)
				{
					this._price = value;
				}
				OnPriceChanged();
			}
		}
		private System.Double _price;
		partial void OnPriceReading();
		partial void OnPriceChanging(ref bool handled, ref System.Double _price);
		partial void OnPriceChanged();
		public void CopyProperties(QuickNSmart.Contracts.Persistence.TestOneToMany.IInvoiceDetail other)
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
				InvoiceId = other.InvoiceId;
				Order = other.Order;
				Text = other.Text;
				Quantity = other.Quantity;
				Tax = other.Tax;
				Price = other.Price;
			}
			AfterCopyProperties(other);
		}
		partial void BeforeCopyProperties(QuickNSmart.Contracts.Persistence.TestOneToMany.IInvoiceDetail other, ref bool handled);
		partial void AfterCopyProperties(QuickNSmart.Contracts.Persistence.TestOneToMany.IInvoiceDetail other);
	}
}
namespace QuickNSmart.Transfer.Persistence.TestOneToMany
{
	partial class InvoiceDetail : IdentityModel
	{
	}
}
namespace QuickNSmart.Transfer.Persistence.Account
{
	using System.Text.Json.Serialization;
	public partial class ActionLog : QuickNSmart.Contracts.Persistence.Account.IActionLog
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
		public System.DateTime Time
		{
			get
			{
				OnTimeReading();
				return _time;
			}
			set
			{
				bool handled = false;
				OnTimeChanging(ref handled, ref _time);
				if (handled == false)
				{
					this._time = value;
				}
				OnTimeChanged();
			}
		}
		private System.DateTime _time;
		partial void OnTimeReading();
		partial void OnTimeChanging(ref bool handled, ref System.DateTime _time);
		partial void OnTimeChanged();
		public System.String Subject
		{
			get
			{
				OnSubjectReading();
				return _subject;
			}
			set
			{
				bool handled = false;
				OnSubjectChanging(ref handled, ref _subject);
				if (handled == false)
				{
					this._subject = value;
				}
				OnSubjectChanged();
			}
		}
		private System.String _subject;
		partial void OnSubjectReading();
		partial void OnSubjectChanging(ref bool handled, ref System.String _subject);
		partial void OnSubjectChanged();
		public System.String Action
		{
			get
			{
				OnActionReading();
				return _action;
			}
			set
			{
				bool handled = false;
				OnActionChanging(ref handled, ref _action);
				if (handled == false)
				{
					this._action = value;
				}
				OnActionChanged();
			}
		}
		private System.String _action;
		partial void OnActionReading();
		partial void OnActionChanging(ref bool handled, ref System.String _action);
		partial void OnActionChanged();
		public System.String Info
		{
			get
			{
				OnInfoReading();
				return _info;
			}
			set
			{
				bool handled = false;
				OnInfoChanging(ref handled, ref _info);
				if (handled == false)
				{
					this._info = value;
				}
				OnInfoChanged();
			}
		}
		private System.String _info;
		partial void OnInfoReading();
		partial void OnInfoChanging(ref bool handled, ref System.String _info);
		partial void OnInfoChanged();
		public void CopyProperties(QuickNSmart.Contracts.Persistence.Account.IActionLog other)
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
				IdentityId = other.IdentityId;
				Time = other.Time;
				Subject = other.Subject;
				Action = other.Action;
				Info = other.Info;
			}
			AfterCopyProperties(other);
		}
		partial void BeforeCopyProperties(QuickNSmart.Contracts.Persistence.Account.IActionLog other, ref bool handled);
		partial void AfterCopyProperties(QuickNSmart.Contracts.Persistence.Account.IActionLog other);
	}
}
namespace QuickNSmart.Transfer.Persistence.Account
{
	partial class ActionLog : IdentityModel
	{
	}
}
namespace QuickNSmart.Transfer.Persistence.Account
{
	using System.Text.Json.Serialization;
	public partial class Identity : QuickNSmart.Contracts.Persistence.Account.IIdentity
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
		public System.Boolean EnableJwtAuth
		{
			get
			{
				OnEnableJwtAuthReading();
				return _enableJwtAuth;
			}
			set
			{
				bool handled = false;
				OnEnableJwtAuthChanging(ref handled, ref _enableJwtAuth);
				if (handled == false)
				{
					this._enableJwtAuth = value;
				}
				OnEnableJwtAuthChanged();
			}
		}
		private System.Boolean _enableJwtAuth;
		partial void OnEnableJwtAuthReading();
		partial void OnEnableJwtAuthChanging(ref bool handled, ref System.Boolean _enableJwtAuth);
		partial void OnEnableJwtAuthChanged();
		public System.Int32 AccessFailedCount
		{
			get
			{
				OnAccessFailedCountReading();
				return _accessFailedCount;
			}
			set
			{
				bool handled = false;
				OnAccessFailedCountChanging(ref handled, ref _accessFailedCount);
				if (handled == false)
				{
					this._accessFailedCount = value;
				}
				OnAccessFailedCountChanged();
			}
		}
		private System.Int32 _accessFailedCount;
		partial void OnAccessFailedCountReading();
		partial void OnAccessFailedCountChanging(ref bool handled, ref System.Int32 _accessFailedCount);
		partial void OnAccessFailedCountChanged();
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
				RowVersion = other.RowVersion;
				Guid = other.Guid;
				Name = other.Name;
				Email = other.Email;
				Password = other.Password;
				EnableJwtAuth = other.EnableJwtAuth;
				AccessFailedCount = other.AccessFailedCount;
				State = other.State;
			}
			AfterCopyProperties(other);
		}
		partial void BeforeCopyProperties(QuickNSmart.Contracts.Persistence.Account.IIdentity other, ref bool handled);
		partial void AfterCopyProperties(QuickNSmart.Contracts.Persistence.Account.IIdentity other);
	}
}
namespace QuickNSmart.Transfer.Persistence.Account
{
	partial class Identity : IdentityModel
	{
	}
}
namespace QuickNSmart.Transfer.Persistence.Account
{
	using System.Text.Json.Serialization;
	public partial class IdentityXRole : QuickNSmart.Contracts.Persistence.Account.IIdentityXRole
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
				RowVersion = other.RowVersion;
				IdentityId = other.IdentityId;
				RoleId = other.RoleId;
			}
			AfterCopyProperties(other);
		}
		partial void BeforeCopyProperties(QuickNSmart.Contracts.Persistence.Account.IIdentityXRole other, ref bool handled);
		partial void AfterCopyProperties(QuickNSmart.Contracts.Persistence.Account.IIdentityXRole other);
	}
}
namespace QuickNSmart.Transfer.Persistence.Account
{
	partial class IdentityXRole : IdentityModel
	{
	}
}
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
		public System.Boolean IsRemoteAuth
		{
			get
			{
				OnIsRemoteAuthReading();
				return _isRemoteAuth;
			}
			set
			{
				bool handled = false;
				OnIsRemoteAuthChanging(ref handled, ref _isRemoteAuth);
				if (handled == false)
				{
					this._isRemoteAuth = value;
				}
				OnIsRemoteAuthChanged();
			}
		}
		private System.Boolean _isRemoteAuth;
		partial void OnIsRemoteAuthReading();
		partial void OnIsRemoteAuthChanging(ref bool handled, ref System.Boolean _isRemoteAuth);
		partial void OnIsRemoteAuthChanged();
		public System.String Origin
		{
			get
			{
				OnOriginReading();
				return _origin;
			}
			set
			{
				bool handled = false;
				OnOriginChanging(ref handled, ref _origin);
				if (handled == false)
				{
					this._origin = value;
				}
				OnOriginChanged();
			}
		}
		private System.String _origin;
		partial void OnOriginReading();
		partial void OnOriginChanging(ref bool handled, ref System.String _origin);
		partial void OnOriginChanged();
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
		public System.String JsonWebToken
		{
			get
			{
				OnJsonWebTokenReading();
				return _jsonWebToken;
			}
			set
			{
				bool handled = false;
				OnJsonWebTokenChanging(ref handled, ref _jsonWebToken);
				if (handled == false)
				{
					this._jsonWebToken = value;
				}
				OnJsonWebTokenChanged();
			}
		}
		private System.String _jsonWebToken;
		partial void OnJsonWebTokenReading();
		partial void OnJsonWebTokenChanging(ref bool handled, ref System.String _jsonWebToken);
		partial void OnJsonWebTokenChanged();
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
				RowVersion = other.RowVersion;
				IdentityId = other.IdentityId;
				IsRemoteAuth = other.IsRemoteAuth;
				Origin = other.Origin;
				Name = other.Name;
				Email = other.Email;
				JsonWebToken = other.JsonWebToken;
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
	partial class LoginSession : IdentityModel
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
				RowVersion = other.RowVersion;
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
	partial class Role : IdentityModel
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
		public System.String Firstname
		{
			get
			{
				OnFirstnameReading();
				return _firstname;
			}
			set
			{
				bool handled = false;
				OnFirstnameChanging(ref handled, ref _firstname);
				if (handled == false)
				{
					this._firstname = value;
				}
				OnFirstnameChanged();
			}
		}
		private System.String _firstname;
		partial void OnFirstnameReading();
		partial void OnFirstnameChanging(ref bool handled, ref System.String _firstname);
		partial void OnFirstnameChanged();
		public System.String Lastname
		{
			get
			{
				OnLastnameReading();
				return _lastname;
			}
			set
			{
				bool handled = false;
				OnLastnameChanging(ref handled, ref _lastname);
				if (handled == false)
				{
					this._lastname = value;
				}
				OnLastnameChanged();
			}
		}
		private System.String _lastname;
		partial void OnLastnameReading();
		partial void OnLastnameChanging(ref bool handled, ref System.String _lastname);
		partial void OnLastnameChanged();
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
				RowVersion = other.RowVersion;
				IdentityId = other.IdentityId;
				Firstname = other.Firstname;
				Lastname = other.Lastname;
			}
			AfterCopyProperties(other);
		}
		partial void BeforeCopyProperties(QuickNSmart.Contracts.Persistence.Account.IUser other, ref bool handled);
		partial void AfterCopyProperties(QuickNSmart.Contracts.Persistence.Account.IUser other);
	}
}
namespace QuickNSmart.Transfer.Persistence.Account
{
	partial class User : IdentityModel
	{
	}
}
