using CORE.Base;
using System;

namespace CORE.Tables
{
    public class SYS_GROUP_USERS : BusinessObject
    {
        #region InnerClass

        public enum SYS_GROUPUSER_RELFields
        {
            REL_ID,
            REL_USERID,
            REL_GROUPID,
            REL_CREATEBY,
            REL_CREATETIME
        }

        #endregion

        #region Properties

        [PrimaryKey(Sequence = "SYS_GROUP_USER_SEQ")]
        [NotNull(ErrorMessage = "Required")]
        public int REL_ID { get; set; }

        [NotNull(ErrorMessage = "Required")]
        [MaxLength(50, ErrorMessage = "MaxLength")]
        public string REL_USERID { get; set; }

        [NotNull(ErrorMessage = "Required")]
        public string REL_GROUPID { get; set; }

        [MaxLength(50, ErrorMessage = "MaxLength")]
        public string REL_CREATEBY { get; set; }

        public DateTime? REL_CREATETIME { get; set; }

        #endregion

        public SYS_GROUP_USERS() { }
    }
}