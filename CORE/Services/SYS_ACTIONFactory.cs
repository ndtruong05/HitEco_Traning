using CORE.Internal;
using CORE.Tables;
using System.Collections.Generic;

namespace CORE.Services
{
    public class SYS_ACTIONFactory
    {
        private SYS_ACTIONSql MainAction;

        public SYS_ACTIONFactory()
        {
            MainAction = new SYS_ACTIONSql();
        }

        public List<SYS_ACTIONS> GetAll()
        {
            return MainAction.SelectAll();
        }
    }
}