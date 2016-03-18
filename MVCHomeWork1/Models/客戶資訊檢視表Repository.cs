using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVCHomeWork1.Models
{   
	public  class 客戶資訊檢視表Repository : EFRepository<客戶資訊檢視表>, I客戶資訊檢視表Repository
	{

	}

	public  interface I客戶資訊檢視表Repository : IRepository<客戶資訊檢視表>
	{

	}
}