using CORE.Base;
using CORE.Views;

namespace CORE.Internal
{
    internal class USER_LOGIN_INFORSql : DataAccessObject<USER_LOGIN_INFOR>
    {
        public USER_LOGIN_INFORSql() : base("AnyCash.ConnectionString")
        {
        }
    }
}
