using MVCHomeWork1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCHomeWork1.Controllers
{
    public class ClientInfoController : Controller
    {

        private 客戶資料Entities db = new 客戶資料Entities();
        // GET: ClientInfo
        public ActionResult Index()
        {
            return View(db.客戶資訊檢視表.ToList());
        }
    }
}