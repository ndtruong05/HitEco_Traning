using CORE.Base;
using CORE.Tables;

namespace CORE.Internal
{
    internal class CASH_INSTALLMENT_HISTORIESql : DataAccessTable<CASH_INSTALLMENT_HISTORIES>
    {
        public CASH_INSTALLMENT_HISTORIESql() : base("AnyCash.ConnectionString")
        {
        }
    }
}
