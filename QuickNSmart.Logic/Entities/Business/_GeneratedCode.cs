namespace QuickNSmart.Logic.Entities.Business.TestOneToMany
{
	using System;
	partial class InvoiceDetails : QuickNSmart.Contracts.Business.TestOneToMany.IInvoiceDetails
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
		public override bool Equals(object obj)
		{
			if (!(obj is QuickNSmart.Contracts.Business.TestOneToMany.IInvoiceDetails instance))
			{
				return false;
			}
			return base.Equals(instance) && Equals(instance);
		}
		protected bool Equals(QuickNSmart.Contracts.Business.TestOneToMany.IInvoiceDetails other)
		{
			if (other == null)
			{
				return false;
			}
			return Id == other.Id && IsEqualsWith(Timestamp, other.Timestamp) && IsEqualsWith(FirstItem, other.FirstItem) && IsEqualsWith(SecondItems, other.SecondItems);
		}
		public override int GetHashCode()
		{
			return HashCode.Combine(Id, Timestamp, FirstItem, SecondItems);
		}
	}
}
namespace QuickNSmart.Logic.Entities.Business.TestOneToMany
{
	partial class InvoiceDetails : OneToManyObject<QuickNSmart.Contracts.Persistence.TestOneToMany.IInvoice, QuickNSmart.Logic.Entities.Persistence.TestOneToMany.Invoice, QuickNSmart.Contracts.Persistence.TestOneToMany.IInvoiceDetail, QuickNSmart.Logic.Entities.Persistence.TestOneToMany.InvoiceDetail>
	{
	}
}
namespace QuickNSmart.Logic.Entities.Business.Account
{
	using System;
	partial class AppAccess : QuickNSmart.Contracts.Business.Account.IAppAccess
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
		public override bool Equals(object obj)
		{
			if (!(obj is QuickNSmart.Contracts.Business.Account.IAppAccess instance))
			{
				return false;
			}
			return base.Equals(instance) && Equals(instance);
		}
		protected bool Equals(QuickNSmart.Contracts.Business.Account.IAppAccess other)
		{
			if (other == null)
			{
				return false;
			}
			return Id == other.Id && IsEqualsWith(Timestamp, other.Timestamp) && IsEqualsWith(Identity, other.Identity) && IsEqualsWith(Roles, other.Roles);
		}
		public override int GetHashCode()
		{
			return HashCode.Combine(Id, Timestamp, Identity, Roles);
		}
	}
}
namespace QuickNSmart.Logic.Entities.Business.Account
{
	partial class AppAccess : IdentityObject
	{
	}
}
namespace QuickNSmart.Logic.Entities.Business.Account
{
	using System;
	partial class IdentityUser : QuickNSmart.Contracts.Business.Account.IIdentityUser
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
		public override bool Equals(object obj)
		{
			if (!(obj is QuickNSmart.Contracts.Business.Account.IIdentityUser instance))
			{
				return false;
			}
			return base.Equals(instance) && Equals(instance);
		}
		protected bool Equals(QuickNSmart.Contracts.Business.Account.IIdentityUser other)
		{
			if (other == null)
			{
				return false;
			}
			return Id == other.Id && IsEqualsWith(Timestamp, other.Timestamp) && IsEqualsWith(FirstItem, other.FirstItem) && IsEqualsWith(SecondItem, other.SecondItem);
		}
		public override int GetHashCode()
		{
			return HashCode.Combine(Id, Timestamp, FirstItem, SecondItem);
		}
	}
}
namespace QuickNSmart.Logic.Entities.Business.Account
{
	partial class IdentityUser : OneToOneObject<QuickNSmart.Contracts.Persistence.Account.IIdentity, QuickNSmart.Logic.Entities.Persistence.Account.Identity, QuickNSmart.Contracts.Persistence.Account.IUser, QuickNSmart.Logic.Entities.Persistence.Account.User>
	{
	}
}
