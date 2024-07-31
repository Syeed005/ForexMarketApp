using ForexMarket.Services.LifeTimeServices;
using Microsoft.AspNetCore.Mvc;

namespace ForexMarket.Controllers {
    public class LifeTimeController : Controller {
        private readonly TransientService transientService;
        private readonly ScopedService scopedService;
        private readonly SingletonService singletonService;

        public LifeTimeController(TransientService transientService, ScopedService scopedService, SingletonService singletonService)
        {
            this.transientService = transientService;
            this.scopedService = scopedService;
            this.singletonService = singletonService;
        }

        public IActionResult Index() {
            var message = new List<string>() {
                HttpContext.Items["CustomeMiddlewareTransient"].ToString(), $"Transient Controller: {transientService.GetGuid()}",
                HttpContext.Items["CustomeMiddlewareScoped"].ToString(), $"Scoped Controller: {scopedService.GetGuid()}",
                HttpContext.Items["CustomeMiddlewareSingleton"].ToString(), $"Singleton Controller: {singletonService.GetGuid()}"
            };
            return View(message);
        }
    }
}
