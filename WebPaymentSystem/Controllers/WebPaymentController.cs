using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebPaymentSystem.Db;

namespace WebPaymentSystem.Controllers
{
    public class WebPaymentController : Controller
    {
        public WebPaymentController(IPaymentDataBase paymentDataBase)
        {
            pdb = paymentDataBase;
        }
        public IActionResult Index()
        {
            return View();
        }
        private IPaymentDataBase pdb;

        [HttpGet("GetPayments")]
        [Produces("text/json")]
        public IActionResult GetPayments(int PageNumber, int PageLenght, DateTime? PaymentDate, string CustomerName, string SortString)
        {
            try
            {
                var data = pdb.GetPayments(PageNumber, PageLenght, PaymentDate, CustomerName, SortString);
                return Ok(data);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(ex.Message);
            }
        }
        [HttpGet("AuthorizeAccountService")]
        public async Task<IActionResult> AuthorizeAccountService(string Url, string Login, string Password)
        {
            Url = "http://localhost:51664/AccountService.svc";
            try
            {
                using var client = new HttpClient();
                string HttpRes = string.Empty;
                var Request = new { Login = Login, Password = Password };
                string JsonData = JsonConvert.SerializeObject(Request);
                var httpUrl = $"{Url}/web/Authorize/?jsonData={JsonData}";
                HttpRes = await client.GetStringAsync(httpUrl);
                dynamic ParseHttpRes = JsonConvert.DeserializeObject(HttpRes);
                var t = ParseHttpRes.IsOk;
                var tt = ParseHttpRes.Msg;
                return Ok();
                //var data = pdb.GetPayments(PageNumber, PageLenght, PaymentDate, CustomerName, SortString);
                //return Ok(data);

            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(ex.Message);
            }

        }
    }
}
