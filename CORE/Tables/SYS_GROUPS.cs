using CORE.Base;
using System;

namespace CORE.Tables
{
    public class SYS_GROUPS : BusinessObject
    {
        #region InnerClass

        public enum SYS_GROUPFields
        {
            GROUP_ID,
            GROUP_NAME,
            GROUP_ACTIVE,
            GROUP_DESCRIPTION,
            GROUP_CREATEBY,
            GROUP_CREATETIME,
            GROUP_BOXID
        }

        #endregion

        #region Properties

        [PrimaryKey(Sequence = "SYS_GROUP_SEQ")]
        [NotNull(ErrorMessage = "Required")]
        [MaxLength(50, ErrorMessage = "MaxLength")]
        public string GROUP_ID { get; set; }

        [NotNull(ErrorMessage = "Required")]
        [MaxLength(100, ErrorMessage = "MaxLength")]
        public string GROUP_NAME { get; set; }

        [NotNull(ErrorMessage = "Required")]
        public bool GROUP_ACTIVE { get; set; }

        [MaxLength(50, ErrorMessage = "MaxLength")]
        public string GROUP_DESCRIPTION { get; set; }

        [MaxLength(50, ErrorMessage = "MaxLength")]
        public string GROUP_CREATEBY { get; set; }

        [NotNull(ErrorMessage = "Required")]
        public DateTime GROUP_CREATETIME { get; set; }

        [NotNull(ErrorMessage = "Required")]
        [MaxLength(50, ErrorMessage = "MaxLength")]
        public string GROUP_BOXID { get; set; }

        #endregion

        public SYS_GROUPS() { }
    }
}