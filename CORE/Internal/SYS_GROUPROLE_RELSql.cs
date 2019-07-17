using CORE.Base;
using CORE.Tables;

namespace CORE.Internal
{
    internal class SYS_GROUPROLE_RELSql : DataAccessTable<SYS_GROUP_ROLES>
    {
        #region Constructor

        public SYS_GROUPROLE_RELSql() : base("AnyCash.ConnectionString")
        {
            // Nothing for now.
        }

        #endregion
    }
}
