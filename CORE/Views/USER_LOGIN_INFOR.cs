using CORE.Base;

namespace CORE.Views
{
    public class USER_LOGIN_INFOR : BusinessObject
    {
        [PrimaryKey]
        public int USER_ID { get; set; }

        public string USER_LOGIN { get; set; }

        public string USER_FULLNAME { get; set; }

        public string USER_FULLNAME_RM { get; set; }

        public int SHOP_ID { get; set; }

        public string SHOP_NAME { get; set; }

        public bool SHOP_CURRENT { get; set; }
    }
}
