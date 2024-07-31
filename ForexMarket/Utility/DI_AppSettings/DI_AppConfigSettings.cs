using ForexMarket.Utility.AppSettingsClasses;

namespace ForexMarket.Utility.DI_AppSettings {
    public static class DI_AppConfigSettings {
        public static IServiceCollection AddServicesToConfig(this IServiceCollection services, IConfiguration configuration) {
            services.Configure<WazeForcastSettings>(configuration.GetSection("WazeForecast"));
            services.Configure<StripeSettings>(configuration.GetSection("Stripe"));
            services.Configure<TwilioSettings>(configuration.GetSection("Twilio"));
            services.Configure<SendGridSettings>(configuration.GetSection("SendGrid"));
            return services;
        }
    }
}
