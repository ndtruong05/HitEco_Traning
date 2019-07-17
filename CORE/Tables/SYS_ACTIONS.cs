using CORE.Base;

namespace CORE.Tables
{
    public class SYS_ACTIONS : BusinessObject
    {
        #region InnerClass

        public enum SYS_ACTIONFields
        {
            ACTION_ID,
            ACTION_NAME,
            ACTION_ISMODULE,
            ACTION_ISROOT,
            ACTION_ISSHOW,
            ACTION_ORDER,
            ACTION_CONTROLPATH,
            ACTION_DESCRIPTION,
            ACTION_ICON_CLASS,
            ACTION_PARENT_ID
        }

        #endregion

        #region Properties

        [PrimaryKey(Sequence = "SYS_ACTIONS_SEQ")]
        [NotNull(ErrorMessage = "Required")]
        [MaxLength(50, ErrorMessage = "MaxLength")]
        public string ACTION_ID { get; set; }

        [NotNull(ErrorMessage = "Required")]
        [MaxLength(100, ErrorMessage = "MaxLength")]
        public string ACTION_NAME { get; set; }

        [NotNull(ErrorMessage = "Required")]
        public bool ACTION_ISMODULE { get; set; }

        [NotNull(ErrorMessage = "Required")]
        public bool ACTION_ISROOT { get; set; }

        [NotNull(ErrorMessage = "Required")]
        public bool ACTION_ISSHOW { get; set; }

        [NotNull(ErrorMessage = "Required")]
        public int ACTION_ORDER { get; set; }
        
        [MaxLength(255, ErrorMessage = "MaxLength")]
        public string ACTION_CONTROLPATH { get; set; }

        [MaxLength(255, ErrorMessage = "MaxLength")]
        public string ACTION_DESCRIPTION { get; set; }

        public string ACTION_ICON_CLASS { get; set; }

        [MaxLength(50, ErrorMessage = "MaxLength")]
        public string ACTION_PARENT_ID { get; set; }

        #endregion

        public SYS_ACTIONS() { }
    }
}