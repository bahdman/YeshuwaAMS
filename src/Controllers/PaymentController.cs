using Microsoft.AspNetCore.Mvc;
using PayStack.Net;
using src.Data;
using src.Models;
using src.ViewModels;

namespace src.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly string token;
        private PayStackApi PayStack { get; set; }
        public PaymentController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            token = _configuration["Payment:PaystackSK"];
            PayStack = new PayStackApi(token);
        }

        [HttpPost]
        public async Task<IActionResult> Index(PaymentViewModel pay)
        {
            //To initialize transaction...
            TransactionInitializeRequest request = new TransactionInitializeRequest()
            {
                AmountInKobo = pay.Amount * 100,
                Email = pay.Email,
                Reference = Generate().ToString(),
                Currency = "NGN",
                CallbackUrl = "http://localhost:30478/Payment/verify"
            };

            //To verify successful transaction...
            TransactionInitializeResponse response = PayStack.Transactions.Initialize(request);
            if (response.Status)
            {
                var payment = new Payment()
                {
                    Amount = pay.Amount,
                    Email = pay.Email,
                    TranRef = request.Reference,
                };
                await _context.Payments.AddAsync(payment);
                await _context.SaveChangesAsync();
                //Redirect to payment url page after url generated... 
                return Redirect(response.Data.AuthorizationUrl);
            }
            ViewData["error"] = response.Message;
            return View();
        }

        public IActionResult Donations()
        {
            var payments = _context.Payments.Where(x => x.Status == true).ToList();
            ViewData["payments"] = payments;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Verify(string reference)
        {
            //To verify transaction...
            TransactionVerifyResponse response = PayStack.Transactions.Verify(reference);
            if (response.Data.Status == "success")
            {
                var payment = _context.Payments.Where(x => x.TranRef == reference).FirstOrDefault();
                if (payment != null)
                {
                    payment.Status = true;
                    _context.Payments.Update(payment);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            ViewData["error"] = response.Data.GatewayResponse;
            return RedirectToAction("Index");
        }

        public static int Generate()
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            return random.Next(100000000, 999999999);
        }
    }
}
