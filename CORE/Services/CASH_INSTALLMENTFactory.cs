using CORE.Internal;
using CORE.Tables;
using CORE.Views;
using System.Collections.Generic;
using System;

namespace CORE.Services
{
    public class CASH_INSTALLMENTFactory
    {
        public CASH_INSTALLMENTFactory() { }
        
        public CASH_INSTALLMENTS GetByKey(int key)
        {
            return new CASH_INSTALLMENTSql().SelectByPrimaryKey(key);
        }

        public bool Insert(CASH_INSTALLMENTS value)
        {
            string ecode, edesc;
            new CASH_SHOPSql().SelectFromStore(out ecode, out edesc, AppSettingKeys.SP_INSTALLMENT_INSERT,
                value.INSTALLMENT_ID,
                value.INSTALLMENT_TOTAL_MONEY,
                value.INSTALLMENT_MONEY_RECEIVED,
                value.INSTALLMENT_TIME,
                value.INSTALLMENT_FREQUENCY,
                value.INSTALLMENT_PAY_NEED,
                value.INSTALLMENT_FROM_DATE,
                value.INSTALLMENT_NOTE,
                value.INSTALLMENT_STATE,
                value.INSTALLMENT_CREATETIME,
                value.INSTALLMENT_CUSTOMERID,
                value.INSTALLMENT_USERID,
                value.INSTALLMENT_CREATEBY
                );
            if (ecode == "000")
                return true;
            else
                return false;
            //return new CASH_INSTALLMENTSql().Insert(value, false);
        }

        public bool Update(CASH_INSTALLMENTS value)
        {
            string ecode, edesc;
            new CASH_SHOPSql().SelectFromStore(out ecode, out edesc, AppSettingKeys.SP_INSTALLMENT_UPDATE,
                value.INSTALLMENT_ID,
                value.INSTALLMENT_TOTAL_MONEY,
                value.INSTALLMENT_MONEY_RECEIVED,
                value.INSTALLMENT_TIME,
                value.INSTALLMENT_FREQUENCY,
                value.INSTALLMENT_PAY_NEED,
                value.INSTALLMENT_FROM_DATE,
                value.INSTALLMENT_NOTE,
                value.INSTALLMENT_STATE,
                value.INSTALLMENT_CREATETIME,
                value.INSTALLMENT_CUSTOMERID,
                value.INSTALLMENT_USERID,
                value.INSTALLMENT_CREATEBY
                );
            if (ecode == "000")
                return true;
            else
                return false;
            //return new CASH_INSTALLMENTSql().Update(value);
        }

        public bool Delete(int customerId)
        {
            return new CASH_INSTALLMENTSql().Delete(customerId);
        }

        public bool UpdateDetail(int id, string date, decimal money)
        {
            CASH_INSTALLMENT_HISTORIESql sql = new CASH_INSTALLMENT_HISTORIESql();
            CASH_INSTALLMENT_HISTORIES his = sql.SelectByPrimaryKey(id);
            if (his.INH_ACTION == 0 || his.INH_ACTION == 2)
            {
                his.INH_ACTION = 1;
                his.INH_DATE = date;
                his.INH_MONEY = money;
            }
            else
            {
                his.INH_ACTION = 2;
                his.INH_DATE = "";
                his.INH_MONEY = 0;
            }

            return sql.Update(his);
        }

        public List<INSTALLMENT_INFOR> GetByUser(int userId, int shopId, string keyText, string fromDate, string toDate, string type, int pageNumber, int pageSize, out int count)
        {
            count = 0;
            object obj;
            List<INSTALLMENT_INFOR> result = new INSTALLMENT_INFORSql()
                .SelectFromStoreOutParam(AppSettingKeys.SP_GET_INSTALLMENT_BY, out obj, userId, shopId, keyText, fromDate, toDate, type, pageNumber, pageSize);

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

        public List<CASH_INSTALLMENT_HISTORIES> GetByInstallmentId(int installmentId)
        {
            return new CASH_INSTALLMENT_HISTORIESql().FilterByField("INH_INSTALLMENTID", installmentId);
        }

        public bool Close(int installmentId)
        {
            CASH_INSTALLMENTSql sql = new CASH_INSTALLMENTSql();
            CASH_INSTALLMENTS installment = sql.SelectByPrimaryKey(installmentId);
            if (installment.INSTALLMENT_STATE == 0)
                installment.INSTALLMENT_STATE = 1;
            else
                installment.INSTALLMENT_STATE = 0;

            return sql.Update(installment);
        }

        public HOME_INFOR GetHomeInfo(int userId, int shopId)
        {
            return new HOME_INFORSql().SelectFromStore(AppSettingKeys.SP_GET_HOME_INFO, userId, shopId)[0];
        }
    }
}
