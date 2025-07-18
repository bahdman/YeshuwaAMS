using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayStack.Net;
using src.Data;
using src.Models;
using src.ViewModels;

namespace src.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly string token;
        private PayStackApi PayStack { get; set; }
        public PaymentsController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            token = _configuration["Payment:PaystackSK"];
            PayStack = new PayStackApi(token);
        }

        [HttpGet]
        public IActionResult Fees()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> FeesBreakdown()
        {
            IEnumerable<Invoice> invoices = await _context.Invoices.ToListAsync();
            return View(invoices);
        }

        [HttpPost]
        public async Task<IActionResult> PayFees()
        {
            IEnumerable<Invoice> invoices = await _context.Invoices.ToListAsync();

            var userEmail = User.FindFirstValue(ClaimTypes.Email) ?? "ymsDev@yopmail.com";
            var totalAmount = invoices.Sum(m => m.Amount);

            string callbackUrl = Url.Action(
                action: "Verify",          // action name
                controller: "Payment",     // controller name
                values: null,              // route values (if any)
                protocol: Request.Scheme); // auto-detects http/https

            //To initialize transaction...
            TransactionInitializeRequest request = new TransactionInitializeRequest()
            {
                AmountInKobo = totalAmount * 100,
                Email = userEmail,
                Reference = Generate().ToString(),
                Currency = "NGN",
                CallbackUrl = callbackUrl
            };

            //To verify successful transaction...
            TransactionInitializeResponse response = PayStack.Transactions.Initialize(request);
            if (response.Status)
            {
                var payment = new Payment()
                {
                    Amount = totalAmount,
                    Email = userEmail,
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

        public IActionResult Payments()
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
                    return RedirectToAction("Index", "Student");
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
