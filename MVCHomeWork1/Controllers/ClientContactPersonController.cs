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
    public class ClientContactPersonController : Controller
    {
        //private 客戶資料Entities db = new 客戶資料Entities();
        客戶聯絡人Repository repo = RepositoryHelper.Get客戶聯絡人Repository();
        客戶資料Entities db = new 客戶資料Entities();

        // GET: ClientContactPerson
        public ActionResult Index()
        {
            List<string> SearchJobTitle = GetJobTitleItem();
            ViewBag.JobTitleItem = SearchJobTitle;
            var data = repo.All();
            return View(data);
            //var 客戶聯絡人=repo.Find()
            //var 客戶聯絡人 = db.客戶聯絡人.Where(e =>e.是否已刪除==false).Include(客 => 客.客戶資料);
            //return View(客戶聯絡人.ToList());
        }

        [HttpPost]
        public ActionResult Index(string JobTitleType)
        {
            //var data = repo.All();
            //var data = repo.Get超級複雜的資料集();

            //return View(data);
            //var data = db.客戶聯絡人.Where(t => t.是否已刪除 == false).Where(t => t.姓名.Contains(ContactPersonName)).ToList();
            object data = null;
            if (JobTitleType != "全部" && JobTitleType != "")
            {
                data = repo.KeywordFind(JobTitleType);
            }
            else 
            {
                data = repo.All().ToList();
            }
           
            List<string> SearchJobTitle = GetJobTitleItem();
            ViewBag.JobTitleItem = SearchJobTitle;
            return View(data);
        }
        //自客戶聯絡人資料表以Distinct方式取得職稱項目
        private List<string> GetJobTitleItem()
        {
            List<string> SearchJobTitle = new List<string>();
            SearchJobTitle.Add("全部");
            var GetJB = db.客戶聯絡人.Select(m => m.職稱).Distinct().ToList();
            foreach (var item in GetJB)
            {
                SearchJobTitle.Add(item);
            }
            return SearchJobTitle;
        }
        
        // GET: ClientContactPerson/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = repo.Find(id);
            //客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // GET: ClientContactPerson/Create
        public ActionResult Create()
        {
           
           ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱");
            return View();
        }

        // POST: ClientContactPerson/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需



        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話,是否已刪除")] 客戶聯絡人 客戶聯絡人)
        {
            客戶聯絡人 EmailSerachResult = new 客戶聯絡人();
            if (ModelState.IsValid)
            {
                //EmailSerachResult = db.客戶聯絡人.Where(e =>e.客戶Id == 客戶聯絡人.客戶Id).Where(e => e.Email == 客戶聯絡人.Email).FirstOrDefault();
                EmailSerachResult = repo.All().Where(e => e.客戶Id == 客戶聯絡人.客戶Id).Where(e => e.Email == 客戶聯絡人.Email).FirstOrDefault();
                if (EmailSerachResult == null) 
                {
                    //db.客戶聯絡人.Add(客戶聯絡人);
                    //db.SaveChanges();
                    repo.Add(客戶聯絡人);
                    repo.UnitOfWork.Commit();
                    return RedirectToAction("Index");
                }
            }

            if (EmailSerachResult != null) { ModelState.AddModelError(String.Empty, "Email已經存在"); }
           
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: ClientContactPerson/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // POST: ClientContactPerson/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                db.Entry(客戶聯絡人).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: ClientContactPerson/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            客戶聯絡人 客戶聯絡人 = repo.Find(id);

            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // POST: ClientContactPerson/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //var 客戶聯絡人 = db.客戶聯絡人.Find(id);
            var 客戶聯絡人 = repo.Find(id);
            //db.客戶聯絡人.Remove(客戶聯絡人);
            客戶聯絡人.是否已刪除 = true;
            repo.UnitOfWork.Commit();
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
