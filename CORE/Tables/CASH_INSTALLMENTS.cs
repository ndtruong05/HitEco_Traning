using CORE.Base;

namespace CORE.Tables
{
    public class CASH_INSTALLMENTS : BusinessObject
    {
        [PrimaryKey]
        public int INSTALLMENT_ID { get; set; }

        public decimal INSTALLMENT_TOTAL_MONEY { get; set; }

        public decimal INSTALLMENT_MONEY_RECEIVED { get; set; }

        public decimal INSTALLMENT_MONEY_PAY { get; set; }

        public int INSTALLMENT_TIME { get; set; }

        public int INSTALLMENT_FREQUENCY { get; set; }

        public decimal INSTALLMENT_PAY_NEED { get; set; }

        public string INSTALLMENT_FROM_DATE { get; set; }

        public string INSTALLMENT_NOTE { get; set; }

        public int INSTALLMENT_STATE { get; set; }

        public int INSTALLMENT_CUSTOMERID { get; set; }

        public int INSTALLMENT_USERID { get; set; }

        public string INSTALLMENT_CREATETIME { get; set; }

        public int INSTALLMENT_CREATEBY { get; set; }
    }
}
