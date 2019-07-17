using CORE.Base;
using System;

namespace CORE.Tables
{
    public class SYS_GROUP_ROLES : BusinessObject
    {
        #region InnerClass

        public enum SYS_GROUPROLE_RELFields
        {
            REL_ID,
            REL_ROLEID,
            REL_GROUPID,
            REL_CREATEBY,
            REL_CREATETIME
        }

        #endregion

        #region Properties

        [PrimaryKey]
        [NotNull(ErrorMessage = "Required")]
        public int REL_ID { get; set; }

        [MaxLength(50, ErrorMessage = "MaxLength")]
        [NotNull(ErrorMessage = "Required")]
        public string REL_ROLEID { get; set; }
        [NotNull(ErrorMessage = "Required")]
        public string REL_GROUPID { get; set; }

        [MaxLength(50, ErrorMessage = "MaxLength")]
        public string REL_CREATEBY { get; set; }

        public DateTime REL_CREATETIME { get; set; }


        #endregion

        public SYS_GROUP_ROLES() { }
    }
}