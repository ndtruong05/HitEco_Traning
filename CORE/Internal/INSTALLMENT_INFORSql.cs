using CORE.Base;
using CORE.Views;

namespace CORE.Internal
{
    internal class INSTALLMENT_INFORSql : DataAccessObject<INSTALLMENT_INFOR>
    {
        public INSTALLMENT_INFORSql() : base("AnyCash.ConnectionString")
        {
        }
    }
}
