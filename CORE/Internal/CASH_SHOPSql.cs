using CORE.Base;
using CORE.Tables;

namespace CORE.Internal
{
    internal class CASH_SHOPSql : DataAccessTable<CASH_SHOPS>
    {
        public CASH_SHOPSql() : base("AnyCash.ConnectionString")
        {
        }
    }
}
