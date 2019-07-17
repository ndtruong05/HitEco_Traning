using CORE.Internal;
using CORE.Tables;
using CORE.Views;
using System.Collections.Generic;
using System.Linq;

namespace CORE.Services
{
    public class CASH_CUSTOMERFactory
    {
        public CASH_CUSTOMERFactory() { }

        public List<CASH_CUSTOMERS2> GetByUser(int userId, int shop, string keyText, string stt, int pageNumber, int pageSize, out int count)
        {
            count = 0;
            object obj;
            List<CASH_CUSTOMERS2> result = new CASH_CUSTOMER2Sql()
                .SelectFromStoreOutParam(AppSettingKeys.SP_GET_CUSTOMER_BY, out obj, userId, shop, keyText, stt, pageNumber, pageSize);
            try
            {
                count = (int)obj;
            }
            catch (System.Exception)
            {
                int.TryParse(obj.ToString(), out count);
            }
            return result;
        }

        public List<CASH_CUSTOMERS> GetByShop(int shopId)
        {
            return new CASH_CUSTOMERSql().FilterByField("CUSTOMER_SHOPID", shopId);//.Where(x => x.CUSTOMER_ACTIVE).ToList();
        }

        public CASH_CUSTOMERS GetByKey(string key)
        {
            return new CASH_CUSTOMERSql().SelectByPrimaryKey(key);
        }

        public bool Insert(CASH_CUSTOMERS value)
        {
            return new CASH_CUSTOMERSql().Insert(value, false);
        }

        public bool Update(CASH_CUSTOMERS value)
        {
            return new CASH_CUSTOMERSql().Update(value);
        }

        public bool Delete(int customerId)
        {
            return new CASH_CUSTOMERSql().Delete(customerId);
        }
    }
}
