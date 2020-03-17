//@QnSBaseCode
//MdStart
using System;

namespace QuickNSmart.AspMvc.Models
{
    public partial class ErrorViewModel : ModelObject
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
//MdEnd