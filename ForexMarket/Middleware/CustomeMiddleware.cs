using ForexMarket.Services.LifeTimeServices;

namespace ForexMarket.Middleware {
    public class CustomeMiddleware {
        private readonly RequestDelegate next;

        public CustomeMiddleware(RequestDelegate next) {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, TransientService transientService, ScopedService scopedService, SingletonService singletonService) {
            context.Items.Add("CustomeMiddlewareTransient", $"Transient Middleware: {transientService.GetGuid()}");
            context.Items.Add("CustomeMiddlewareScoped", $"Scoped Middleware: {scopedService.GetGuid()}");
            context.Items.Add("CustomeMiddlewareSingleton", $"Singleton Middleware: {singletonService.GetGuid()}");

            //passing the context to next delegate
            await next(context);
        }
    }
}
