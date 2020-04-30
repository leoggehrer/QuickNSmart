//@QnSBaseCode
//MdStart

using System.ComponentModel.DataAnnotations;

namespace QuickNSmart.AspMvc.Models
{
	public partial class IdentityModel : ModelObject, Contracts.IIdentifiable
    {
		private System.Int32 _id;
		public virtual System.Int32 Id
		{
			get
			{
				OnIdReading();
				return _id;
			}
			set
			{
				bool handled = false;
				OnIdChanging(ref handled, ref _id);
				if (handled == false)
				{
					this._id = value;
				}
				OnIdChanged();
			}
		}
		partial void OnIdReading();
		partial void OnIdChanging(ref bool handled, ref System.Int32 _id);
		partial void OnIdChanged();

		private byte[] _rowVersion;
		[ScaffoldColumn(false)]
		public virtual byte[] RowVersion
		{
			get
			{
				OnRowVersionReading();
				return _rowVersion;
			}
			set
			{
				bool handled = false;
				OnRowVersionChanging(ref handled, ref _rowVersion);
				if (handled == false)
				{
					this._rowVersion = value;
				}
				OnRowVersionChanged();
			}
		}
		partial void OnRowVersionReading();
		partial void OnRowVersionChanging(ref bool handled, ref byte[] _rowVersion);
		partial void OnRowVersionChanged();

	}
}
//MdEnd