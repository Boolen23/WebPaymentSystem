using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccountSystem.Model.Contracts
{
    public class DefaultAccountService : IAccountService
    {
        public string Authorize(string jsonData)
        {
            var model = AccountServiceModel.LoadAccounts();
            dynamic tmp = JsonConvert.DeserializeObject(jsonData);
            var Account = AccountData.ByRawPassword((string)tmp.Login, (string)tmp.Password);
            string MsgResult = string.Empty;
            var IsOk = model.CheckIsExists(Account, out MsgResult);
            var Response = new { IsOk = IsOk, Msg = MsgResult };
            return JsonConvert.SerializeObject(Response);
        }

        public void AddTestData()
        {
            AccountServiceModel.GenerateAccounts();
        }
    }
}