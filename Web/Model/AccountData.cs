using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccountSystem.Model
{
    public struct AccountData
    {
        public string Login { get; set; }
        public string Password { get; set;}
        public static AccountData ByRawPassword(string Login, string RawPassword) => new AccountData() { Login = Login, Password = RawPassword.MD5Hash() };
    }
}