using CORE.Base;

namespace CORE.Tables
{
    public class CASH_CUSTOMERS : BusinessObject
    {
        [PrimaryKey]
        public int CUSTOMER_ID { get; set; }

        public string CUSTOMER_NAME { get; set; }

        public string CUSTOMER_PHONE { get; set; }

        public string CUSTOMER_IDENTITY { get; set; }

        public string CUSTOMER_DATE { get; set; }

        public string CUSTOMER_PLACE { get; set; }

        public string CUSTOMER_ADDRESS { get; set; }

        public bool CUSTOMER_ACTIVE { get; set; }

        public string CUSTOMER_CREATETIME { get; set; }

        public int CUSTOMER_SHOPID { get; set; }
    }
}
