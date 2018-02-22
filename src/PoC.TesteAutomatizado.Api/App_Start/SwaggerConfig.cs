using PoC.TesteAutomatizado.Api;
using Swashbuckle.Application;
using System.Web.Http;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace PoC.TesteAutomatizado.Api
{
    public static class SwaggerConfig
    {
        public static void Register()
        {
            GlobalConfiguration.Configuration
                .EnableSwagger(c => { c.SingleApiVersion("v1", "PoC.TesteAutomatizado.Api"); })
                .EnableSwaggerUi(c => { });
        }
    }
}
