using CORE.Models;
using CORE.Tables;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace AnyCash.Controllers
{
    public class ShopController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult _List(string keyText = "", string stt = "", int pageNumber = 1, int pageSize = 10)
        {
            ViewBag.pageNumber = pageNumber;
            ViewBag.pageSize = pageSize;
            int count = 0;
            List<CASH_SHOPS> list = new List<CASH_SHOPS>();
            try
            {
                int userId = ((USER_INFOR)Session[AppSession.AppSessionKeys.USER_INFO]).USER_ID;
                list = CASH_SHOP_Service.GetByUser(userId, keyText, stt, pageNumber, pageSize, out count);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            ViewBag.maxNumber = Math.Ceiling((double)count / pageSize);

            return PartialView(list);
        }

        public ActionResult Create(int id = 0)
        {
            CASH_SHOPS shop = CASH_SHOP_Service.GetByKey(id);
            if (shop == null)
                shop = new CASH_SHOPS();

            return View(shop);
        }
    }
}