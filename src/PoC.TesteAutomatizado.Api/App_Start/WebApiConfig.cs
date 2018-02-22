using Newtonsoft.Json.Serialization;
using SimpleInjector.Integration.WebApi;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PoC.TesteAutomatizado.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Enable Cors
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // Errors
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.LocalOnly;

            // Camel case
            var jsonConfig = config.Formatters.JsonFormatter;
            jsonConfig.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Injetor de dependências
            InjetorDependencias.InjetorDependencias.Iniciar();
            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(InjetorDependencias.InjetorDependencias.Container);
        }
    }
}
