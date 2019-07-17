using CORE.Internal;
using CORE.Tables;
using System.Collections.Generic;
using System.Linq;
using System;

namespace CORE.Services
{
    public class CASH_SHOPFactory
    {
        public CASH_SHOPFactory() { }

        public List<CASH_SHOPS> GetByUser(int userId, string keyText, string stt, int pageNumber, int pageSize, out int count)
        {
            count = 0;
            try
            {
                object obj;
                List<CASH_SHOPS> result = new CASH_SHOPSql()
                    .SelectFromStoreOutParam(AppSettingKeys.SP_GET_SHOP_BY, out obj, userId, keyText, stt, pageNumber, pageSize);

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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CASH_SHOPS GetByKey(int key)
        {
            return new CASH_SHOPSql().SelectByPrimaryKey(key);
        }

        public bool Insert(CASH_SHOPS value)
        {
            return new CASH_SHOPSql().Insert(value, false);
        }

        public bool Update(CASH_SHOPS value)
        {
            return new CASH_SHOPSql().Update(value);
        }

        public bool Delete(int shopId)
        {
            return new CASH_SHOPSql().Delete(shopId);
        }

        public bool ChangeShop(int userId, int shopId)
        {
            string ecode, edesc;
            new CASH_SHOPSql().SelectFromStore(out ecode, out edesc, AppSettingKeys.SP_CHANGE_SHOP, userId, shopId);
            if (ecode == "000")
                return true;
            else
                return false;
        }
    }
}
