//@QnSBaseCode
//MdStart

namespace QuickNSmart.AspMvc.Models
{
    public class SubmitCmd
    {
        public bool RightAlign { get; set; } = false;
        public string SubmitText { get; set; } = "Save";
        public string SubmitCss { get; set; } = "btn btn-primary";
        public string SubmitStyle { get; set; } = "min-width: 8em;";
    }
}
//MdEnd