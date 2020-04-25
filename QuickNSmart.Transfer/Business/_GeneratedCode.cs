namespace QuickNSmart.Transfer.Business.TestOneToMany
{
	using System.Text.Json.Serialization;
	public partial class InvoiceDetails : QuickNSmart.Contracts.Business.TestOneToMany.IInvoiceDetails
	{
		static InvoiceDetails()
		{
			ClassConstructing();
			ClassConstructed();
		}
		static partial void ClassConstructing();
		static partial void ClassConstructed();
		public InvoiceDetails()
		{
			Constructing();
			Constructed();
		}
		partial void Constructing();
		partial void Constructed();
		public void CopyProperties(QuickNSmart.Contracts.Business.TestOneToMany.IInvoiceDetails other)
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
				FirstItem.CopyProperties(other.FirstItem);
				ClearSecondItems();
				foreach (var item in other.SecondItems)
				{
					AddSecondItem(item);
				}
			}
			AfterCopyProperties(other);
		}
		partial void BeforeCopyProperties(QuickNSmart.Contracts.Business.TestOneToMany.IInvoiceDetails other, ref bool handled);
		partial void AfterCopyProperties(QuickNSmart.Contracts.Business.TestOneToMany.IInvoiceDetails other);
	}
}
namespace QuickNSmart.Transfer.Business.TestOneToMany
{
	partial class InvoiceDetails : OneToManyModel<QuickNSmart.Contracts.Persistence.TestOneToMany.IInvoice, QuickNSmart.Transfer.Persistence.TestOneToMany.Invoice, QuickNSmart.Contracts.Persistence.TestOneToMany.IInvoiceDetail, QuickNSmart.Transfer.Persistence.TestOneToMany.InvoiceDetail>
	{
	}
}
namespace QuickNSmart.Transfer.Business.Account
{
	using System.Text.Json.Serialization;
	public partial class AppAccess : QuickNSmart.Contracts.Business.Account.IAppAccess
	{
		static AppAccess()
		{
			ClassConstructing();
			ClassConstructed();
		}
		static partial void ClassConstructing();
		static partial void ClassConstructed();
		public AppAccess()
		{
			Constructing();
			Constructed();
		}
		partial void Constructing();
		partial void Constructed();
		[JsonIgnore]
		public QuickNSmart.Contracts.Persistence.Account.IIdentity Identity
		{
			get
			{
				OnIdentityReading();
				return _identity;
			}
			set
			{
				bool handled = false;
				OnIdentityChanging(ref handled, ref _identity);
				if (handled == false)
				{
					this._identity = value;
				}
				OnIdentityChanged();
			}
		}
		private QuickNSmart.Contracts.Persistence.Account.IIdentity _identity;
		partial void OnIdentityReading();
		partial void OnIdentityChanging(ref bool handled, ref QuickNSmart.Contracts.Persistence.Account.IIdentity _identity);
		partial void OnIdentityChanged();
		[JsonIgnore]
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
		public void CopyProperties(QuickNSmart.Contracts.Business.Account.IAppAccess other)
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
				Identity = other.Identity;
				Roles = other.Roles;
			}
			AfterCopyProperties(other);
		}
		partial void BeforeCopyProperties(QuickNSmart.Contracts.Business.Account.IAppAccess other, ref bool handled);
		partial void AfterCopyProperties(QuickNSmart.Contracts.Business.Account.IAppAccess other);
	}
}
namespace QuickNSmart.Transfer.Business.Account
{
	partial class AppAccess : IdentityModel
	{
	}
}
namespace QuickNSmart.Transfer.Business.Account
{
	using System.Text.Json.Serialization;
	public partial class IdentityUser : QuickNSmart.Contracts.Business.Account.IIdentityUser
	{
		static IdentityUser()
		{
			ClassConstructing();
			ClassConstructed();
		}
		static partial void ClassConstructing();
		static partial void ClassConstructed();
		public IdentityUser()
		{
			Constructing();
			Constructed();
		}
		partial void Constructing();
		partial void Constructed();
		public void CopyProperties(QuickNSmart.Contracts.Business.Account.IIdentityUser other)
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
				FirstItem.CopyProperties(other.FirstItem);
				SecondItem.CopyProperties(other.SecondItem);
			}
			AfterCopyProperties(other);
		}
		partial void BeforeCopyProperties(QuickNSmart.Contracts.Business.Account.IIdentityUser other, ref bool handled);
		partial void AfterCopyProperties(QuickNSmart.Contracts.Business.Account.IIdentityUser other);
	}
}
namespace QuickNSmart.Transfer.Business.Account
{
	partial class IdentityUser : OneToOneModel<QuickNSmart.Contracts.Persistence.Account.IIdentity, QuickNSmart.Transfer.Persistence.Account.Identity, QuickNSmart.Contracts.Persistence.Account.IUser, QuickNSmart.Transfer.Persistence.Account.User>
	{
	}
}
