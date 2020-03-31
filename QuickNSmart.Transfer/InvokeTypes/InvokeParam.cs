//@QnSBaseCode
//MdStart
using System.Linq;

namespace QuickNSmart.Transfer.InvokeTypes
{
    public partial class InvokeParam
    {
        public string MethodName { get; set; }
        public string Separator { get; set; } = ";";
        public string Parameters { get; set; }

        public string[] GetParameters()
        {
            string[] result;

            if (string.IsNullOrEmpty(Parameters))
            {
                result = new string[0];
            }
            else
            {
                result = Parameters.Split(Separator.ToCharArray()).ToArray();
            }
            return result;
        }
    }
}
//MdEnd