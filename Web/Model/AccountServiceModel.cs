using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AccountSystem.Model
{
    public class AccountServiceModel
    {
        public static AccountServiceModel LoadAccounts()
        {
            if (!File.Exists(fileName))
                GenerateAccounts();
            AccountServiceModel model = new AccountServiceModel();
            model.AccountList = Functions.ReadFromXmlFile<List<AccountData>>(fileName);
            return model;
        }
        private static readonly string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AccountData.xml");
        public static void GenerateAccounts()
        {
            List<AccountData> GenerateData = new List<AccountData>();
            for (int i = 1; i < 10; i++)
                GenerateData.Add(AccountData.ByRawPassword($"Nick{i}", (i * 111).ToString()));
            Functions.WriteToXmlFile(fileName, GenerateData);
        }
        private List<AccountData> AccountList;
        public bool CheckIsExists(AccountData data, out string msg)
        {
            if(string.IsNullOrEmpty(data.Login) || string.IsNullOrEmpty(data.Password))
            {
                msg = "Пустой логин или пароль!";
                return false;
            }
            if(AccountList.Count(i=> i.Login == data.Login && i.Password == data.Password) == 1)
            {
                msg = $"Добрый день, {data.Login}!";
                return true;
            }
            msg = "Не найдено пары логин / пароль!";
            return false;
        }
    }
}