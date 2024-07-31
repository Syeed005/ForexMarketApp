using ForexMarket.Models;
using ForexMarket.Models.ViewModels;
using ForexMarket.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ForexMarket.Controllers {
    public class HomeController : Controller {
        

        public IActionResult Index() {
            HomeVM homeVM = new HomeVM();
            MarketForcaster marketForcaster = new MarketForcaster();
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

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
