using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCHomeWork1.Models;

namespace MVCHomeWork1.Controllers
{
    public class ClientController : Controller
    {
        
        客戶資料Repository repo = RepositoryHelper.Get客戶資料Repository();
        private 客戶資料Entities db = new 客戶資料Entities();

        // GET: Client
        public ActionResult Index()
        {
            List<string> IndustryItem = GetIndustryItem();
            ViewBag.IndustryItem = IndustryItem;
            //return View(db.客戶資料.ToList());
            return View(repo.All().ToList());

        }

        //建構行業別選單資訊
        private static List<string> GetIndustryItem()
        {
            List<string> IndustryItem = new List<string>();
            IndustryItem.Add("全部行業別");
            IndustryItem.Add("士");
            IndustryItem.Add("農");
            IndustryItem.Add("工");
            IndustryItem.Add("商");
            return IndustryItem;
        }

        //客戶名稱關鍵字搜尋
        //[HttpPost]
        //public ActionResult Index(string clientName)
        //{
        //    var data = db.客戶資料.Where(t => t.客戶名稱.Contains(clientName)).ToList();
        //    return View(data);
        //}

        //客戶行業別搜尋
        [HttpPost]
        public ActionResult Index(string IndustryType)
        {
            if (IndustryType != null)
            {
                object data = null;
                if (IndustryType=="全部行業別")
                {
                    //data = db.客戶資料.ToList();
                    data = repo.All().ToList();

                }
                else
                {
                    data = repo.All().Where(t => t.行業別 == IndustryType).ToList();
                }
                List<string> IndustryItem = GetIndustryItem();
                ViewBag.IndustryItem = IndustryItem;
                return View(data);
            }
            return View();
        }

        // GET: Client/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = repo.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // GET: Client/Create
        public ActionResult Create()
        {
            List<string> IndustryItem = GetIndustryItem();
            ViewBag.IndustryItem = IndustryItem;
            return View();
        }

        // POST: Client/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,行業別")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                repo.Add(客戶資料);
                repo.UnitOfWork.Commit();
                //db.客戶資料.Add(客戶資料);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(客戶資料);
        }

        // GET: Client/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = repo.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: Client/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int Id)
        {
            客戶資料 ClientInfo = repo.Find(Id);
            if (TryUpdateModel<Models.客戶資料>(ClientInfo, new string[] { "Id", "客戶名稱", "統一編號", "電話", "傳真", "地址", "Email", "行業別" }))
            {
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            //if (ModelState.IsValid)
            //{
            //    db.Entry(客戶資料).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            return View(ClientInfo);
        }

        // GET: Client/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 =repo.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶資料 客戶資料 = repo.Find(id);

            客戶資料.是否已刪除 = true;
            repo.UnitOfWork.Commit();
            //db.客戶資料.Remove(客戶資料);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
