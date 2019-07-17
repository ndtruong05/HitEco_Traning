using System.Collections.Generic;

namespace CORE.Models
{
    public class USER_INFOR
    {
        public int USER_ID { get; set; }

        public string USER_LOGIN { get; set; }

        public string USER_FULLNAME { get; set; }

        public string USER_FULLNAME_RM { get; set; }

        public List<SHOP_INFOR> USER_SHOPLIST { get; set; }
    }

    public class SHOP_INFOR
    {
        public int SHOP_ID { get; set; }

        public string SHOP_NAME { get; set; }

        public bool SHOP_CURRENT { get; set; }
    }
}
