using System.Web.Hosting;

using Microsoft.Owin;

using Owin;

[assembly: OwinStartup(typeof(Lab.TestService.Startup))]

namespace Lab.TestService
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfig.Register();
        }
    }

    public static class GlobalConfig
    {
        public static void Register()
        {
            GlobalParameter.PATH_LOG = HostingEnvironment.MapPath(@"/App_Data/Logs");
            
            GlobalLogger.Logger = new Logger.Services.LogService();
        }
    }

    public static class GlobalLogger
    {
        public static Logger.Services.LogService Logger;
    }

    public static class GlobalParameter
    {
        public static string PATH_LOG;
        public static string SERVER_IP;
    }
}
