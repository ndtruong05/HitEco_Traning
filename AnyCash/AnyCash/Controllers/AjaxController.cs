using AnyCash.Models;
using CORE.Models;
using CORE.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AnyCash.Controllers
{
    public class AjaxController : BaseController
    {
        public JsonResult ShopInsert(CASH_SHOPS value)
        {
            AjaxResultModel Result = new AjaxResultModel();

            try
            {
                value.SHOP_CREATETIME = DateTime.Now.ToString("yyyyMMdd");
                int userId = ((USER_INFOR)Session[AppSession.AppSessionKeys.USER_INFO]).USER_ID;
                value.SHOP_USERID = userId;
                value.SHOP_CURRENT = (Session[AppSession.AppSessionKeys.SHOP_CURRENT] == null);

                if (CASH_SHOP_Service.Insert(value))
                {
                    int obj;
                    List<CASH_SHOPS> shop = CASH_SHOP_Service.GetByUser(userId, "", "A", 1, short.MaxValue, out obj);
                    if (Session[AppSession.AppSessionKeys.SHOP_CURRENT] == null)
                    {
                        Session[AppSession.AppSessionKeys.SHOP_CURRENT] = shop.Where(x => x.SHOP_CURRENT).Select(x => new SHOP_INFOR
                        {
                            SHOP_ID = x.SHOP_ID,
                            SHOP_NAME = x.SHOP_NAME,
                            SHOP_CURRENT = x.SHOP_CURRENT
                        }).FirstOrDefault();
                    }

                    ((USER_INFOR)Session[AppSession.AppSessionKeys.USER_INFO]).USER_SHOPLIST.AddRange(
                        shop.Where(x => x.SHOP_ACTIVE
                            && !((USER_INFOR)Session[AppSession.AppSessionKeys.USER_INFO]).USER_SHOPLIST.Any(y => x.SHOP_ID == y.SHOP_ID))
                        .Select(x => new SHOP_INFOR
                        {
                            SHOP_ID = x.SHOP_ID,
                            SHOP_NAME = x.SHOP_NAME,
                            SHOP_CURRENT = x.SHOP_CURRENT
                        }));

                    Result.Code = 0;
                    Result.Result = "Thêm cửa hàng thành công";
                }
                else
                {
                    Result.Code = 999;
                    Result.Result = "Thêm cửa hàng thất bại";
                }
            }
            catch (Exception Ex)
            {
                Result.Code = 2000;
                Result.Result = Ex.Message;
            }

            return Json(Result);
        }

        public JsonResult ShopUpdate(CASH_SHOPS value)
        {
            AjaxResultModel Result = new AjaxResultModel();

            try
            {
                //Không thể thay đổi trạng thái cửa hàng hiện tại về tạm dừng
                SHOP_INFOR current = (SHOP_INFOR)Session[AppSession.AppSessionKeys.SHOP_CURRENT];
                if (current != null && current.SHOP_ID == value.SHOP_ID && !value.SHOP_ACTIVE)
                {
                    Result.Code = 1;
                    Result.Result = "Không thể thay đổi trạng thái cửa hàng hiện tại về tạm dừng";
                    return Json(Result);
                }

                CASH_SHOPS update = CASH_SHOP_Service.GetByKey(value.SHOP_ID);
                update.SHOP_NAME = value.SHOP_NAME;
                update.SHOP_PHONE = value.SHOP_PHONE;
                update.SHOP_CITY = value.SHOP_CITY;
                update.SHOP_DISTRICT = value.SHOP_DISTRICT;
                update.SHOP_ADDRESS = value.SHOP_ADDRESS;
                update.SHOP_REPRESENT = value.SHOP_REPRESENT;
                update.SHOP_ACTIVE = value.SHOP_ACTIVE;
                if (CASH_SHOP_Service.Update(update))
                {
                    if (update.SHOP_CURRENT)
                    {
                        Session[AppSession.AppSessionKeys.SHOP_CURRENT] = new SHOP_INFOR
                        {
                            SHOP_ID = update.SHOP_ID,
                            SHOP_NAME = update.SHOP_NAME,
                            SHOP_CURRENT = update.SHOP_CURRENT
                        };
                    }

                    if (update.SHOP_ACTIVE
                        && !((USER_INFOR)Session[AppSession.AppSessionKeys.USER_INFO]).USER_SHOPLIST.Any(x => x.SHOP_ID == update.SHOP_ID))
                    {
                        ((USER_INFOR)Session[AppSession.AppSessionKeys.USER_INFO]).USER_SHOPLIST.Add(new SHOP_INFOR
                        {
                            SHOP_ID = update.SHOP_ID,
                            SHOP_NAME = update.SHOP_NAME,
                            SHOP_CURRENT = update.SHOP_CURRENT
                        });
                    }

                    Result.Code = 0;
                    Result.Result = "Cập nhật cửa hàng thành công";
                }
                else
                {
                    Result.Code = 999;
                    Result.Result = "Cập nhật cửa hàng thất bại";
                }
            }
            catch (Exception Ex)
            {
                Result.Code = 2000;
                Result.Result = Ex.Message;
            }

            return Json(Result);
        }

        public JsonResult ShopDelete(int shopId)
        {
            AjaxResultModel Result = new AjaxResultModel();

            try
            {
                //Không thể thay đổi trạng thái cửa hàng hiện tại về tạm dừng
                SHOP_INFOR current = (SHOP_INFOR)Session[AppSession.AppSessionKeys.SHOP_CURRENT];
                if (current != null && current.SHOP_ID == shopId)
                {
                    Result.Code = 1;
                    Result.Result = "Không thể xóa cửa hàng hiện tại";
                    return Json(Result);
                }

                if (CASH_SHOP_Service.Delete(shopId))
                {
                    ((USER_INFOR)Session[AppSession.AppSessionKeys.USER_INFO]).USER_SHOPLIST.RemoveAll(x => x.SHOP_ID == shopId);
                    Result.Code = 0;
                    Result.Result = "Xóa cửa hàng thành công";
                }
                else
                {
                    Result.Code = 999;
                    Result.Result = "Xóa cửa hàng thất bại";
                }
            }
            catch (Exception Ex)
            {
                Result.Code = 2000;
                Result.Result = Ex.Message;
            }

            return Json(Result);
        }

        public JsonResult ShopChange(int shopId)
        {
            AjaxResultModel Result = new AjaxResultModel();

            try
            {
                USER_INFOR user = (USER_INFOR)Session[AppSession.AppSessionKeys.USER_INFO];
                int userId = user.USER_ID;

                if (CASH_SHOP_Service.ChangeShop(userId, shopId))
                {
                    user.USER_SHOPLIST.ForEach(x =>
                    {
                        if (x.SHOP_ID == shopId)
                        {
                            x.SHOP_CURRENT = true;
                            Session[AppSession.AppSessionKeys.SHOP_CURRENT] = x;
                        }
                        else
                        {
                            x.SHOP_CURRENT = false;
                        }
                    });
                    Result.Code = 0;
                    Result.Result = "Chuyển cửa hàng thành công";
                }
                else
                {
                    Result.Code = 999;
                    Result.Result = "Chuyển cửa hàng thất bại";
                }
            }
            catch (Exception Ex)
            {
                Result.Code = 2000;
                Result.Result = Ex.Message;
            }

            return Json(Result);
        }

        public JsonResult CustomerInsert(CASH_CUSTOMERS value)
        {
            AjaxResultModel Result = new AjaxResultModel();

            try
            {
                value.CUSTOMER_CREATETIME = DateTime.Now.ToString("yyyyMMdd");

                if (CASH_CUSTOMER_Service.Insert(value))
                {
                    Result.Code = 0;
                    Result.Result = "Thêm khách hàng thành công";
                }
                else
                {
                    Result.Code = 999;
                    Result.Result = "Thêm khách hàng thất bại";
                }
            }
            catch (Exception Ex)
            {
                Result.Code = 2000;
                Result.Result = Ex.Message;
            }

            return Json(Result);
        }

        public JsonResult CustomerUpdate(CASH_CUSTOMERS value)
        {
            AjaxResultModel Result = new AjaxResultModel();

            try
            {
                value.CUSTOMER_CREATETIME = DateTime.Now.ToString("yyyyMMdd");

                if (CASH_CUSTOMER_Service.Update(value))
                {
                    Result.Code = 0;
                    Result.Result = "Cập nhật khách hàng thành công";
                }
                else
                {
                    Result.Code = 999;
                    Result.Result = "Cập nhật khách hàng thất bại";
                }
            }
            catch (Exception Ex)
            {
                Result.Code = 2000;
                Result.Result = Ex.Message;
            }

            return Json(Result);
        }

        public JsonResult CustomerDelete(int customerId)
        {
            AjaxResultModel Result = new AjaxResultModel();

            try
            {
                if (CASH_CUSTOMER_Service.Delete(customerId))
                {
                    Result.Code = 0;
                    Result.Result = "Xóa khách hàng thành công";
                }
                else
                {
                    Result.Code = 999;
                    Result.Result = "Xóa khách hàng thất bại";
                }
            }
            catch (Exception Ex)
            {
                Result.Code = 2000;
                Result.Result = Ex.Message;
            }

            return Json(Result);
        }

        public JsonResult InstallmentInsert(CASH_INSTALLMENTS value)
        {
            AjaxResultModel Result = new AjaxResultModel();

            try
            {
                value.INSTALLMENT_CREATETIME = DateTime.Now.ToString("yyyyMMdd");
                value.INSTALLMENT_CREATEBY = ((USER_INFOR)Session[AppSession.AppSessionKeys.USER_INFO]).USER_ID;
                value.INSTALLMENT_PAY_NEED = value.INSTALLMENT_TOTAL_MONEY / value.INSTALLMENT_TIME;
                value.INSTALLMENT_STATE = 0;

                if (CASH_INSTALLMENT_Service.Insert(value))
                {
                    Result.Code = 0;
                    Result.Result = "Thêm hợp đồng thành công";
                }
                else
                {
                    Result.Code = 999;
                    Result.Result = "Thêm hợp đồng thất bại";
                }
            }
            catch (Exception Ex)
            {
                Result.Code = 2000;
                Result.Result = Ex.Message;
            }

            return Json(Result);
        }

        public JsonResult InstallmentUpdate(CASH_INSTALLMENTS value)
        {
            AjaxResultModel Result = new AjaxResultModel();

            try
            {
                CASH_INSTALLMENTS update = CASH_INSTALLMENT_Service.GetByKey(value.INSTALLMENT_ID);
                if (update.INSTALLMENT_TOTAL_MONEY != value.INSTALLMENT_TOTAL_MONEY
                    || update.INSTALLMENT_MONEY_RECEIVED != value.INSTALLMENT_MONEY_RECEIVED
                    || update.INSTALLMENT_TIME != value.INSTALLMENT_TIME
                    || update.INSTALLMENT_FROM_DATE != value.INSTALLMENT_FROM_DATE)
                {
                    Result.Code = 1;
                    Result.Result = "Cập nhật hợp đồng thất bại";
                    return Json(Result);
                }
                value.INSTALLMENT_CREATETIME = DateTime.Now.ToString("yyyyMMdd");
                value.INSTALLMENT_CREATEBY = ((USER_INFOR)Session[AppSession.AppSessionKeys.USER_INFO]).USER_ID;
                value.INSTALLMENT_PAY_NEED = update.INSTALLMENT_PAY_NEED;
                value.INSTALLMENT_STATE = 0;

                if (CASH_INSTALLMENT_Service.Update(value))
                {
                    Result.Code = 0;
                    Result.Result = "Cập nhật hợp đồng thành công";
                }
                else
                {
                    Result.Code = 999;
                    Result.Result = "Cập nhật hợp đồng thất bại";
                }
            }
            catch (Exception Ex)
            {
                Result.Code = 2000;
                Result.Result = Ex.Message;
            }

            return Json(Result);
        }

        public JsonResult InstallmentDelete(int installmentId)
        {
            AjaxResultModel Result = new AjaxResultModel();

            try
            {
                if (CASH_INSTALLMENT_Service.Delete(installmentId))
                {
                    Result.Code = 0;
                    Result.Result = "Xóa hợp đồng thành công";
                }
                else
                {
                    Result.Code = 999;
                    Result.Result = "Xóa hợp đồng thất bại";
                }
            }
            catch (Exception Ex)
            {
                Result.Code = 2000;
                Result.Result = Ex.Message;
            }

            return Json(Result);
        }

        public JsonResult InstallmentClose(int installmentId, int stt)
        {
            AjaxResultModel Result = new AjaxResultModel();

            try
            {
                if (CASH_INSTALLMENT_Service.Close(installmentId))
                {
                    Result.Code = 0;
                    if (stt == 0)
                        Result.Result = "Đóng hợp đồng thành công";
                    else
                        Result.Result = "Mở lại hợp đồng thành công";
                }
                else
                {
                    Result.Code = 999;
                    if (stt == 0)
                        Result.Result = "Đóng hợp đồng thất bại";
                    else
                        Result.Result = "Mở lại hợp đồng thất bại";
                }
            }
            catch (Exception Ex)
            {
                Result.Code = 2000;
                Result.Result = Ex.Message;
            }

            return Json(Result);
        }

        public JsonResult InstallmentDetailUpdate(int id, string date, decimal money, bool action)
        {
            AjaxResultModel Result = new AjaxResultModel();

            try
            {
                if (CASH_INSTALLMENT_Service.UpdateDetail(id, date, money))
                {
                    Result.Code = 0;
                    if (action)
                        Result.Result = "Đóng họ thành công";
                    else
                        Result.Result = "Hủy đóng họ thành công";
                }
                else
                {
                    Result.Code = 999;
                    if (action)
                        Result.Result = "Đóng họ thất bại";
                    else
                        Result.Result = "Hủy đóng họ thất bại";
                }
            }
            catch (Exception Ex)
            {
                Result.Code = 2000;
                Result.Result = Ex.Message;
            }

            return Json(Result);
        }
    }
}