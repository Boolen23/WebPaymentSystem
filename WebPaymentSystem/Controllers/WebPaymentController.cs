using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
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
    }
}
