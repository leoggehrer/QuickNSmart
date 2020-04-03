//@QnSBaseCode
//MdStart

using System.Collections.Generic;

namespace QuickNSmart.AspMvc.Models.Modules.Export
{
    public class ImportProtocol : ModelObject
    {
        public IEnumerable<ImportLog> LogInfos { get; set; } = new ImportLog[0];
    }
}
//MdEnd