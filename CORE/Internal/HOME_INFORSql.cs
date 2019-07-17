using CORE.Base;
using CORE.Views;

namespace CORE.Internal
{
    internal class HOME_INFORSql : DataAccessObject<HOME_INFOR>
    {
        public HOME_INFORSql() : base("AnyCash.ConnectionString")
        {
        }
    }
}
