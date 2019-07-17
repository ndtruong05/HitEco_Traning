using CORE.Base;

namespace CORE.Tables
{
    public class CASH_SHOPS : BusinessObject
    {
        [PrimaryKey]
        public int SHOP_ID { get; set; }

        public string SHOP_NAME { get; set; }

        public string SHOP_PHONE { get; set; }

        public string SHOP_CITY { get; set; }

        public string SHOP_DISTRICT { get; set; }

        public string SHOP_ADDRESS { get; set; }

        public string SHOP_REPRESENT { get; set; }

        public decimal SHOP_MONEY { get; set; }

        public bool SHOP_ACTIVE { get; set; }

        public bool SHOP_CURRENT { get; set; }

        public string SHOP_CREATETIME { get; set; }

        public int SHOP_USERID { get; set; }
    }
}
