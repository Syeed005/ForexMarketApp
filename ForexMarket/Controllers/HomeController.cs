using ForexMarket.Data;
using ForexMarket.Models;
using ForexMarket.Models.ViewModels;
using ForexMarket.Repository;
using ForexMarket.Repository.IRepository;
using ForexMarket.Services;
using ForexMarket.Utility.AppSettingsClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace ForexMarket.Controllers {
    public class HomeController : Controller {
        public HomeVM homeVM { get; set; }
        public readonly IMarketForcaster marketForcaster;
        public readonly WazeForcastSettings wazeForcastSettings;
        public readonly StripeSettings stripeSettings;
        public readonly TwilioSettings twilioSettings;
        public readonly SendGridSettings sendGridSettings;

        private readonly ICreditValidator creditValidator;
        private readonly IUnitOfWork unitOfWork;

        [BindProperty]
        public CreditApplication CreditModel { get; set; }
        public HomeController(IMarketForcaster marketForcaster, IOptions<WazeForcastSettings> wazeForcastSettings, ICreditValidator creditValidator, IUnitOfWork unitOfWork) {
            this.homeVM = new HomeVM();
            this.marketForcaster = marketForcaster;
            this.wazeForcastSettings = wazeForcastSettings.Value;
            this.creditValidator = creditValidator;
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index() {
            MarketResult marketResult = marketForcaster.GetMarketPrediction();

            switch (marketResult.MarketCondition) {
                case MarketCondition.StableDown:
                homeVM.MarketForecast = "Market shows signs to go down in a stable state! It is a not a good sign to apply for credit applications! But extra credit is always piece of mind if you have handy when you need it.";
                break;
                case MarketCondition.StableUp:
                homeVM.MarketForecast = "Market shows signs to go up in a stable state! It is a great sign to apply for credit applications!";
                break;
                case MarketCondition.Volatile:
                homeVM.MarketForecast = "Market shows signs of volatility. In uncertain times, it is good to have credit handy if you need extra funds!";
                break;
                default:
                homeVM.MarketForecast = "Apply for a credit card using our application!";
                break;

            }

            return View(homeVM);
        }

        public IActionResult AllConfigSettings([FromServices] IOptions<StripeSettings> stripeSettings, [FromServices] IOptions<TwilioSettings> twilioSettings, [FromServices] IOptions<SendGridSettings> sendGridSettings) {
            List<string> settings = new List<string>();
            settings.Add($"ForecastTrackerEnabled: {wazeForcastSettings.ForecastTrackerEnabled}");
            settings.Add($"Secret Key: {stripeSettings.Value.SecretKey}");
            settings.Add($"Publishable Key: {stripeSettings.Value.PublishableKey}");
            settings.Add($"Phone Number: {twilioSettings.Value.PhoneNumber}");
            settings.Add($"Auth token: {twilioSettings.Value.AuthToken}");
            settings.Add($"Account Sid: {twilioSettings.Value.AccountSid}");
            settings.Add($"Send grid key: {sendGridSettings.Value.SendGridKey}");
            return View(settings);
        }

        public IActionResult CreditApplication() {
            CreditModel = new CreditApplication();
            return View(CreditModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [ActionName("CreditApplication")]
        public async Task<IActionResult> CreditApplicationPost([FromServices] Func<CreditApprovedEnum, ICreditApproved> creditService) {
            if (ModelState.IsValid) {
                var (validationPass, errorMessages) = await creditValidator.PassAllValidations(CreditModel);
                CreditResult creditResult = new CreditResult() {
                    ErrorList = errorMessages,
                    CreditID = 0,
                    Success = validationPass
                };
                if (validationPass) {
                    //add record to db
                    CreditModel.CreditApproved = creditService(CreditModel.Salary > 5000 ? CreditApprovedEnum.High : CreditApprovedEnum.Low).GetCreditApproved(CreditModel);


                    //db.CreateApplicationModel.Add(CreditModel);
                    //db.SaveChanges();
                    unitOfWork.CreditApplication.Add(CreditModel);
                    unitOfWork.Save();

                    creditResult.CreditID = CreditModel.Id;
                    creditResult.CreditApproved = CreditModel.CreditApproved;
                    return RedirectToAction(nameof(CreditResult), creditResult);
                } else {
                    return RedirectToAction(nameof(CreditResult), creditResult);
                }
            }

            return View(CreditModel);
        }

        public IActionResult CreditResult(CreditResult creditResult) {
            return View(creditResult);
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
