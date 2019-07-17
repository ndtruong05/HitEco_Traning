using AnyCash.AppSession;
using AnyCash.Cookie;
using CORE.Models;
using CORE.Tables;
using CORE.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AnyCash.Controllers
{
    public class HomeController : BaseController
    {
        #region Login

        public ActionResult Login(string url)
        {
            ViewBag.url = url;

            return LoginCheck(url);
        }

        [HttpPost]
        public ActionResult Login(string username, string password, string url, string remember)
        {
            ViewBag.url = url;

            USER_INFOR user = SYS_USER_Service.CheckLogin(username, password);

            if (user != null)
            {
                Session[AppSessionKeys.USER_INFO] = user;
                Session[AppSessionKeys.SHOP_CURRENT] = user.USER_SHOPLIST.Where(x => x.SHOP_CURRENT).FirstOrDefault();
                Session[AppSessionKeys.USER_LIST_ACTION] = SYS_ACTION_Service.GetAll().Where(x => x.ACTION_ISSHOW).ToList();

                if (remember == "on")
                {
                    AppCookieInfo.UserID = username;
                    AppCookieInfo.HashedPassword = password;
                }
                else
                {
                    AppCookieInfo.UserID = "";
                    AppCookieInfo.HashedPassword = "";
                }
            }
            else
            {
                ViewBag.error = "Đăng nhập không thành công";
                AppCookieInfo.UserID = "";
                AppCookieInfo.HashedPassword = "";
            }

            return LoginCheck(url);
        }

        private ActionResult LoginCheck(string url)
        {
            if (Session[AppSessionKeys.USER_INFO] != null)
            {
                //Thêm cửa hàng nếu chưa có
                if (Session[AppSessionKeys.SHOP_CURRENT] == null)
                {
                    return RedirectToAction("Create", "Shop");
                }

                if (string.IsNullOrEmpty(url))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectPermanent(url);
                }
            }
            else
            {
                ViewBag.username = AppCookieInfo.UserID;
                ViewBag.password = AppCookieInfo.HashedPassword;
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();

            return RedirectToAction("Index");
        }

        #endregion

        // GET: Home
        public ActionResult Index()
        {
            HOME_INFOR info = new HOME_INFOR();
            List<CASH_SHOPS> list = new List<CASH_SHOPS>();
            try
            {
                int userId = ((USER_INFOR)Session[AppSession.AppSessionKeys.USER_INFO]).USER_ID;
                int shopId = 0;
                if (Session[AppSession.AppSessionKeys.SHOP_CURRENT] != null)
                    shopId = ((SHOP_INFOR)Session[AppSession.AppSessionKeys.SHOP_CURRENT]).SHOP_ID;

                info = CASH_INSTALLMENT_Service.GetHomeInfo(userId, shopId);

                int count = 0;
                list = CASH_SHOP_Service.GetByUser(userId, "", "A", 1, short.MaxValue, out count);
            }
            catch (Exception ex)
            {
                string startUpPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
                CORE.Helpers.IOHelper.WriteLog(startUpPath, "Home/Index :", ex.Message, ex.ToString());
            }
            ViewBag.ShopList = list;

            return View(info);
        }

        [ChildActionOnly]
        public PartialViewResult _HomeHeader()
        {
            USER_INFOR user = (USER_INFOR)Session[AppSession.AppSessionKeys.USER_INFO];
            ViewBag.userName = user.USER_FULLNAME;
            if (string.IsNullOrEmpty(ViewBag.userName))
            {
                ViewBag.userName = user.USER_LOGIN;
            }
            ViewBag.shopCurrent = user.USER_SHOPLIST.Where(x => x.SHOP_CURRENT).FirstOrDefault();
            return PartialView(user.USER_SHOPLIST.Where(x => x != ViewBag.shopCurrent).ToList());
        }

        [ChildActionOnly]
        public PartialViewResult _HomeFooter()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public PartialViewResult _HomeMenuLeft()
        {
            List<SYS_ACTIONS> actions = (List<SYS_ACTIONS>)Session[AppSessionKeys.USER_LIST_ACTION];
            return PartialView(actions);
        }

        [ChildActionOnly]
        public PartialViewResult _Pagination(int maxNumber, int pageNumber)
        {
            ViewBag.maxNumber = maxNumber;
            ViewBag.pageNumber = pageNumber;
            return PartialView();
        }
    }
}