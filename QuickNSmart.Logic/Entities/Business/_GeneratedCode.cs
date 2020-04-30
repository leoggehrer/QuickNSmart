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
				RowVersion = other.RowVersion;
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
			return Id == other.Id && IsEqualsWith(RowVersion, other.RowVersion) && IsEqualsWith(FirstItem, other.FirstItem) && IsEqualsWith(SecondItems, other.SecondItems);
		}
		public override int GetHashCode()
		{
			return HashCode.Combine(Id, RowVersion, FirstItem, SecondItems);
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
				RowVersion = other.RowVersion;
				FirstItem.CopyProperties(other.FirstItem);
				ClearSecondItems();
				foreach (var item in other.SecondItems)
				{
					AddSecondItem(item);
				}
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
			return Id == other.Id && IsEqualsWith(RowVersion, other.RowVersion) && IsEqualsWith(FirstItem, other.FirstItem) && IsEqualsWith(SecondItems, other.SecondItems);
		}
		public override int GetHashCode()
		{
			return HashCode.Combine(Id, RowVersion, FirstItem, SecondItems);
		}
	}
}
namespace QuickNSmart.Logic.Entities.Business.Account
{
	partial class AppAccess : OneToManyObject<QuickNSmart.Contracts.Persistence.Account.IIdentity, QuickNSmart.Logic.Entities.Persistence.Account.Identity, QuickNSmart.Contracts.Persistence.Account.IRole, QuickNSmart.Logic.Entities.Persistence.Account.Role>
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
				RowVersion = other.RowVersion;
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
			return Id == other.Id && IsEqualsWith(RowVersion, other.RowVersion) && IsEqualsWith(FirstItem, other.FirstItem) && IsEqualsWith(SecondItem, other.SecondItem);
		}
		public override int GetHashCode()
		{
			return HashCode.Combine(Id, RowVersion, FirstItem, SecondItem);
		}
	}
}
namespace QuickNSmart.Logic.Entities.Business.Account
{
	partial class IdentityUser : OneToOneObject<QuickNSmart.Contracts.Persistence.Account.IIdentity, QuickNSmart.Logic.Entities.Persistence.Account.Identity, QuickNSmart.Contracts.Persistence.Account.IUser, QuickNSmart.Logic.Entities.Persistence.Account.User>
	{
	}
}
