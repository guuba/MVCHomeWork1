using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MVCHomeWork1.Models
{
    public class 手機號碼格式驗證Attribute : DataTypeAttribute
    {
        public 手機號碼格式驗證Attribute() : base(DataType.Text)
        {
        }

        public override bool IsValid(object value)
        {
            Regex MobilePhoneRex = new Regex(@"\d{4}-\d{6}");
            var str = (string)value;
            return MobilePhoneRex.IsMatch(str);
        }
    }
}

