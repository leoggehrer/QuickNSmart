namespace QuickNSmart.AspMvc.Models.Business.TestOneToMany
{
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
namespace QuickNSmart.AspMvc.Models.Business.TestOneToMany
{
	partial class InvoiceDetails : OneToManyModel<QuickNSmart.Contracts.Persistence.TestOneToMany.IInvoice, QuickNSmart.AspMvc.Models.Persistence.TestOneToMany.Invoice, QuickNSmart.Contracts.Persistence.TestOneToMany.IInvoiceDetail, QuickNSmart.AspMvc.Models.Persistence.TestOneToMany.InvoiceDetail>
	{
	}
}
namespace QuickNSmart.AspMvc.Models.Business.Account
{
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
	}
}
namespace QuickNSmart.AspMvc.Models.Business.Account
{
	partial class AppAccess : OneToManyModel<QuickNSmart.Contracts.Persistence.Account.IIdentity, QuickNSmart.AspMvc.Models.Persistence.Account.Identity, QuickNSmart.Contracts.Persistence.Account.IRole, QuickNSmart.AspMvc.Models.Persistence.Account.Role>
	{
	}
}
namespace QuickNSmart.AspMvc.Models.Business.Account
{
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
namespace QuickNSmart.AspMvc.Models.Business.Account
{
	partial class IdentityUser : OneToOneModel<QuickNSmart.Contracts.Persistence.Account.IIdentity, QuickNSmart.AspMvc.Models.Persistence.Account.Identity, QuickNSmart.Contracts.Persistence.Account.IUser, QuickNSmart.AspMvc.Models.Persistence.Account.User>
	{
	}
}
