using CORE.Base;
using System;

namespace CORE.Tables
{
    public class SYS_USERS : BusinessObject
    {
        #region InnerClass

        public enum SYS_USERFields
        {
            USER_ID,
            USER_LOGIN,
            USER_PASSWORD,
            USER_FULLNAME,
            USER_EMAIL,
            USER_PHONE,
            USER_ISLOCKED,
            USER_ISSUPER,
            USER_CREATEBY,
            USER_CREATETIME
        }

        #endregion

        #region Properties

        [PrimaryKey]
        public int USER_ID { get; set; }

        [MaxLength(50, ErrorMessage = "MaxLength")]
        public string USER_LOGIN { get; set; }

        [NotNull(ErrorMessage = "Required")]
        [MaxLength(50, ErrorMessage = "MaxLength")]
        public string USER_PASSWORD { get; set; }

        public string USER_EMAIL { get; set; }
        public string USER_PHONE { get; set; }

        [NotNull(ErrorMessage = "Required")]
        public bool USER_ACTIVE { get; set; }

        [NotNull(ErrorMessage = "Required")]
        public bool USER_ISLOCKED { get; set; }

        [NotNull(ErrorMessage = "Required")]
        public bool USER_ISSUPER { get; set; }

        [MaxLength(50, ErrorMessage = "MaxLength")]
        public string USER_CREATEBY { get; set; }

        [NotNull(ErrorMessage = "Required")]
        public DateTime USER_CREATETIME { get; set; }

        public string USER_AVATAR { get; set; }

        [MaxLength(100, ErrorMessage = "MaxLength")]
        public string USER_FULLNAME { get; set; }

        [MaxLength(100, ErrorMessage = "MaxLength")]
        public string USER_FULLNAME_RM { get; set; }

        public bool USER_GENDER { get; set; }

        public DateTime USER_BIRTHDAY { get; set; }

        #endregion

        public SYS_USERS() { }
    }
}