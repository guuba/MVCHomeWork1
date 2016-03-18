using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVCHomeWork1.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{
        public override IQueryable<客戶聯絡人> All()
        {
            return base.All().Where(p => !p.是否已刪除);
        }

        public IQueryable<客戶聯絡人> All(bool isAll)
        {
            if (isAll)
            {
                return base.All();
            }
            else
            {
                return this.All();
            }
        }

        public 客戶聯絡人 Find(int? id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }

        public IList<客戶聯絡人> KeywordFind(string keyword)
        {
            //return this.All().Where(p => p.姓名.Contains(keyword)).ToList();
            return this.All().Where(p => p.職稱==keyword).ToList();
        }

        ////也可以用IQueryable1、IEnumerable
        //public IQueryable<客戶聯絡人> KeywordFind(string keyword)
        //{

        //    return this.All().Where(p => p.姓名.Contains(keyword));
        //}
    }

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}