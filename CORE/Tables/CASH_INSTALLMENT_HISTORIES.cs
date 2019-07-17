using CORE.Base;

namespace CORE.Tables
{
    public class CASH_INSTALLMENT_HISTORIES : BusinessObject
    {
        [PrimaryKey]
        public int INH_ID { get; set; }

        public string INH_DATE_FROM { get; set; }

        public string INH_DATE_TO { get; set; }

        public decimal INH_MONEY_NEED { get; set; }

        public decimal INH_MONEY { get; set; }

        public string INH_DATE { get; set; }

        public int INH_ACTION { get; set; }

        public int INH_USERID { get; set; }

        public int INH_INSTALLMENTID { get; set; }
    }
}
