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
            var t = AccountServiceModel.LoadAccounts();
            return null;
        }

        public void AddTestData()
        {
            AccountServiceModel.GenerateAccounts();
        }
    }
}