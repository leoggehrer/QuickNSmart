//@QnSBaseCode
//MdStart

namespace QuickNSmart.AspMvc.Models.Modules.Export
{
    public class ImportModel<T> : ModelObject
        where T : new()
    {
        public ImportAction Action { get; set; }
        public int Id { get; set; }
        public T Model { get; set; }
    }
}
//MdEnd