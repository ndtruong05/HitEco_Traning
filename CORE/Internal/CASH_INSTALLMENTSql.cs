using CORE.Base;
using CORE.Tables;

namespace CORE.Internal
{
    internal class CASH_INSTALLMENTSql : DataAccessTable<CASH_INSTALLMENTS>
    {
        public CASH_INSTALLMENTSql() : base("AnyCash.ConnectionString")
        {
        }
    }
}
