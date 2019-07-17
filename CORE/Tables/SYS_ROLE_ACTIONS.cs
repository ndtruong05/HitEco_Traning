using CORE.Base;
using System;

namespace CORE.Tables
{
    public class SYS_ROLE_ACTIONS : BusinessObject
    {
        #region InnerClass

        public enum SYS_ROLEACTION_RELFields
        {
            REL_ID,
            REL_ROLEID,
            REL_ACTIONID,
            REL_CREATEBY,
            REL_CREATETIME
        }

        #endregion

        #region Properties

        [PrimaryKey(Sequence = "SYS_ROLE_ACTIONS_SEQ")]
        [NotNull(ErrorMessage = "Required")]
        public int REL_ID { get; set; }

        [MaxLength(50, ErrorMessage = "MaxLength")]
        public string REL_ROLEID { get; set; }

        [MaxLength(50, ErrorMessage = "MaxLength")]
        public string REL_ACTIONID { get; set; }

        [MaxLength(50, ErrorMessage = "MaxLength")]
        public string REL_CREATEBY { get; set; }

        public DateTime? REL_CREATETIME { get; set; }

        #endregion

        public SYS_ROLE_ACTIONS() { }
    }
}