using CORE.Base;

namespace CORE.Views
{
    public class INSTALLMENT_DETAIL : BusinessObject
    {
        [PrimaryKey]
        public int INH_ID { get; set; }

        public string INH_FROM_DATE { get; set; }

        public string INH_TO_DATE { get; set; }

        public decimal INH_MONEY_NEED { get; set; }

        public decimal INH_MONEY { get; set; }

        public string INH_DATE { get; set; }

        public int INH_ACTION { get; set; }

        public string INH_USERID { get; set; }

        public int INH_INSTALLMENTID { get; set; }
    }
}
