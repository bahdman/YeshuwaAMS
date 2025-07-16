using Microsoft.AspNetCore.Mvc;

namespace src.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ApplicatonDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly string token;
        private PayStackApi PayStack { get; set; }//The gods of paystack...
        public DonateController(ApplicatonDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            token = _configuration["Payment:PaystackSK"];
            PayStack = new PayStackApi(token);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(DonateViewModel donate)
        {
            //To initialize transaction...
            TransactionInitializeRequest request = new TransactionInitializeRequest()
            {
                AmountInKobo = donate.Amount * 100,
                Email = donate.Email,
                Reference = Generate().ToString(),
                Currency = "NGN",
                CallbackUrl = "http://localhost:30478/donate/verify"
            };

            //To verify successful transaction...
            TransactionInitializeResponse response = PayStack.Transactions.Initialize(request);
            if (response.Status)
            {
                var transaction = new Transaction()
                {
                    Amount = donate.Amount,
                    Email = donate.Email,
                    TranRef = request.Reference,
                    Name = donate.Name,
                };
                await _context.Transactions.AddAsync(transaction);
                await _context.SaveChangesAsync();
                //Redirect to payment url page after url generated... 
                return Redirect(response.Data.AuthorizationUrl);
            }
            ViewData["error"] = response.Message;
            return View();
        }

        public IActionResult Donations()
        {
            var transactions = _context.Transactions.Where(x => x.Status == true).ToList();
            ViewData["transactions"] = transactions;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Verify(string reference)
        {
            //To verify transaction...
            TransactionVerifyResponse response = PayStack.Transactions.Verify(reference);
            if (response.Data.Status == "success")
            {
                var transaction = _context.Transactions.Where(x => x.TranRef == reference).FirstOrDefault();
                if (transaction != null)
                {
                    transaction.Status = true;
                    _context.Transactions.Update(transaction);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Donations");
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
}
