//@QnSBaseCode
//MdStart
using System.Collections;
using System.Linq;

namespace QuickNSmart.Logic.Entities
{
	internal abstract partial class IdentityObject : Contracts.IIdentifiable
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

		protected static bool IsEqualsWith(object obj1, object obj2)
		{
			bool result = false;

			if (obj1 == null && obj2 == null)
			{
				result = true;
			}
			else if (obj1 != null && obj2 != null)
			{
				if (obj1 is IEnumerable && obj2 is IEnumerable)
				{
					var enumerable1 = ((IEnumerable)obj1).Cast<object>().ToList();
					var enumerable2 = ((IEnumerable)obj2).Cast<object>().ToList();

					result = enumerable1.SequenceEqual(enumerable2);
				}
				else
				{
					result = obj1.Equals(obj2);
				}
			}
			return result;
		}
	}
}
//MdEnd
