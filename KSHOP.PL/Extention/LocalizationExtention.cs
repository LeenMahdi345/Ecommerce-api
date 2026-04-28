using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace KSHOP.PL.Extention
{
    public static class LocalizationExtention
    {
        public static IServiceCollection AddLocalizationServices(this IServiceCollection Services)
            {
      Services.AddLocalization(options => options.ResourcesPath = "");
            const string defaultCulture = "en";

            var supportedCultures = new[]
            {
    new CultureInfo(defaultCulture),
    new CultureInfo("ar")
};
           Services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture(defaultCulture);
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;

                options.RequestCultureProviders.Clear();

                options.RequestCultureProviders.Add(new AcceptLanguageHeaderRequestCultureProvider());
                options.RequestCultureProviders.Add(new CookieRequestCultureProvider());
            });
            return Services;
        }

    }
}
