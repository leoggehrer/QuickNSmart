//@DomainCode
//MdStart
using System;
using QuickNSmart.Contracts.Client;

namespace QuickNSmart.Logic
{
    public static partial class Factory
    {
        public enum PersistenceType
        {
            Db,
            //Csv,
            //Ser,
        }
        public static PersistenceType Persistence { get; set; } = Factory.PersistenceType.Db;
        internal static DataContext.IContext CreateContext()
        {
            DataContext.IContext result = null;

            if (Persistence == PersistenceType.Db)
            {
                result = new DataContext.Db.DbQuickNSmartContext();
            }
            return result;
        }
    }
}
//MdEnd
