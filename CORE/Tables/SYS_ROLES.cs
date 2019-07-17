using CORE.Base;
using System;

namespace CORE.Tables
{
    public class SYS_ROLES : BusinessObject
    {
        #region InnerClass

        public enum SYS_ROLEFields
        {
            ROLE_ID,
            ROLE_NAME,
            ROLE_ACTIVE,
            ROLE_DESCRIPTION,
            ROLE_CREATEBY,
            ROLE_CREATETIME
        }

        #endregion

        #region Properties

        [PrimaryKey(Sequence = "SYS_ROLES_SEQ")]
        [NotNull(ErrorMessage = "Required")]
        [MaxLength(50, ErrorMessage = "MaxLength")]
        [RegularExpression("^[^<>]*$", ErrorMessage = "RegularExpression")]
        public string ROLE_ID { get; set; }

        [NotNull(ErrorMessage = "Required")]
        [MaxLength(100, ErrorMessage = "MaxLength")]
        [RegularExpression("^[^<>]*$", ErrorMessage = "RegularExpression")]
        public string ROLE_NAME { get; set; }

        public bool ROLE_ACTIVE { get; set; }

        [MaxLength(2000, ErrorMessage = "MaxLength")]
        [RegularExpression("^[^<>]*$", ErrorMessage = "RegularExpression")]
        public string ROLE_DESCRIPTION { get; set; }

        [MaxLength(2000, ErrorMessage = "MaxLength")]
        [RegularExpression("^[^<>]*$", ErrorMessage = "RegularExpression")]
        public string ROLE_CREATEBY { get; set; }

        public DateTime ROLE_CREATETIME { get; set; }

        #endregion

        public SYS_ROLES() { }
    }
}