using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AccountServiceClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string Url = "http://localhost:51664/AccountService.svc";
            string httpUrl = string.Empty;
            Console.WriteLine("Enter login: ");
            string Login = Console.ReadLine();
            Console.WriteLine("Enter password: ");
            string Password = Console.ReadLine();

            try
            {
                using var client = new HttpClient();
                string HttpRes = string.Empty;
                var Request = new { Login = Login, Password = Password };
                string JsonData = JsonConvert.SerializeObject(Request);
                httpUrl = $"{Url}/web/Authorize?jsonData={JsonData}";
                HttpRes = await client.GetStringAsync(httpUrl);
                AccountResponse ParseHttpRes = JsonConvert.DeserializeObject<AccountResponse>(HttpRes);
                dynamic json = JsonConvert.DeserializeObject(ParseHttpRes.AuthorizeResult);
                if ((bool)json.IsOk)
                    Console.WriteLine((string)json.Msg);
                else
                    throw new Exception((string)json.Msg);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
            Console.ReadLine();
        }
        public class AccountResponse
        {
            public string AuthorizeResult { get; set; }
        }
    }
}
