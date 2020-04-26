//@QnSBaseCode
//MdStart

using System.Collections.Generic;

namespace QuickNSmart.AspMvc.Models.Modules.Export
{
    public class ImportProtocol : ModelObject
    {
        public string FilePath { get; set; }
        public string BackAction { get; set; } = "Index";
        public string BackController { get; set; } = "Home";
        public IEnumerable<ImportLog> LogInfos { get; set; } = new ImportLog[0];
    }
}
//MdEnd