using CORE.Helpers;
using CORE.Internal;
using CORE.Models;
using CORE.Tables;
using CORE.Views;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CORE.Services
{
    public class SYS_USERFactory
    {
        public SYS_USERFactory() { }

        public USER_INFOR CheckLogin(string username, string password)
        {
            try
            {
                string ecode, edesc;
                List<USER_LOGIN_INFOR> user = new USER_LOGIN_INFORSql()
                    .SelectFromStoreOutEcode(out ecode, out edesc, AppSettingKeys.SP_CHECK_LOGIN, username, Encrypt.GetMd5Hash2(password));
                if (ecode == "000" && user.Count > 0)
                    return new USER_INFOR
                    {
                        USER_ID = user[0].USER_ID,
                        USER_LOGIN = user[0].USER_LOGIN,
                        USER_FULLNAME = user[0].USER_FULLNAME,
                        USER_FULLNAME_RM = user[0].USER_FULLNAME_RM,
                        USER_SHOPLIST = user.Select(x => new SHOP_INFOR
                        {
                            SHOP_ID = x.SHOP_ID,
                            SHOP_NAME = x.SHOP_NAME,
                            SHOP_CURRENT = x.SHOP_CURRENT
                        }).ToList()
                    };
                else
                    return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<SYS_USERS> GetStaff(int userId)
        {
            List<SYS_USERS> result = new List<SYS_USERS>();
            result.Add(new SYS_USERSql().SelectByPrimaryKey(userId));

            return result;
        }
    }
}
