using CORE.Base;

namespace CORE.Views
{
    public class INSTALLMENT_INFOR : BusinessObject
    {
        [PrimaryKey]
        public int INSTALLMENT_ID { get; set; }

        public int INSTALLMENT_CUSTOMERID { get; set; }

        public string INSTALLMENT_CUSTOMER_NAME { get; set; }

        public decimal INSTALLMENT_TOTAL_MONEY { get; set; }

        public decimal INSTALLMENT_MONEY_RECEIVED { get; set; }

        public int INSTALLMENT_TIME { get; set; }

        public decimal INSTALLMENT_PAY_NEED { get; set; }

        public string INSTALLMENT_FROM_DATE { get; set; }

        public int INSTALLMENT_STATE { get; set; }

        public decimal TIEN_DA_DONG { get; set; }
        public decimal NO_CU { get; set; }
        public decimal CON_PHAI_DONG { get; set; }
        public string NGAY_PHAI_DONG { get; set; }
    }
}
