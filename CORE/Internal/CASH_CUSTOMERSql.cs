using CORE.Base;
using CORE.Tables;

namespace CORE.Internal
{
    internal class CASH_CUSTOMERSql : DataAccessTable<CASH_CUSTOMERS>
    {
        public CASH_CUSTOMERSql() : base("AnyCash.ConnectionString")
        {
        }
    }
}
