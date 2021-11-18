﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Web.Model;

namespace AccountSystem.Model
{
    public class AccountServiceModel
    {
        public static AccountServiceModel LoadAccounts()
        {
            AccountServiceModel model = new AccountServiceModel();
            model.AccountList = Functions.ReadFromXmlFile<List<AccountData>>(fileName);
            return model;
        }
        private static readonly string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AccountData.xml");
        public static void GenerateAccounts()
        {
            List<AccountData> GenerateData = new List<AccountData>();
            for (int i = 1; i < 10; i++)
                GenerateData.Add(new AccountData() { Login = $"Nick{i}", Password = (i * 111).ToString().MD5Hash() });
            Functions.WriteToXmlFile<List<AccountData>>(fileName, GenerateData);
        }
        private List<AccountData> AccountList;
    }
}