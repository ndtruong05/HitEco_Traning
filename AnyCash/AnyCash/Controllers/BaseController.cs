using AnyCash.Configuration;
using CORE.Services;
using System.Web.Mvc;

namespace AnyCash.Controllers
{
    [AuthorizeBusiness]
    public class BaseController : Controller
    {
        protected SYS_USERFactory SYS_USER_Service = new SYS_USERFactory();
        protected SYS_ACTIONFactory SYS_ACTION_Service = new SYS_ACTIONFactory();

        protected CASH_SHOPFactory CASH_SHOP_Service = new CASH_SHOPFactory();
        protected CASH_CUSTOMERFactory CASH_CUSTOMER_Service = new CASH_CUSTOMERFactory();
        protected CASH_INSTALLMENTFactory CASH_INSTALLMENT_Service = new CASH_INSTALLMENTFactory();
    }
}