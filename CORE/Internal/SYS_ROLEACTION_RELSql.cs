using CORE.Base;
using CORE.Tables;

namespace CORE.Internal
{
    internal class SYS_ROLEACTION_RELSql : DataAccessTable<SYS_ROLE_ACTIONS>
    {
        #region Constructor

        public SYS_ROLEACTION_RELSql() : base("AnyCash.ConnectionString")
        {
            // Nothing for now.
        }

        #endregion
    }
}
