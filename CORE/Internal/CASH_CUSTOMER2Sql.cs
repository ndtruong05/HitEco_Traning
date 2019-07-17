using CORE.Base;
using CORE.Views;

namespace CORE.Internal
{
    internal class CASH_CUSTOMER2Sql : DataAccessTable<CASH_CUSTOMERS2>
    {
        public CASH_CUSTOMER2Sql() : base("AnyCash.ConnectionString")
        {
        }
    }
}
