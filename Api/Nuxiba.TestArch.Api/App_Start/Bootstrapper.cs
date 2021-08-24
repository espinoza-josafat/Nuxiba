using System.Web.Http;

namespace Nuxiba.TestArch.Web
{
    public static class Bootstrapper
    {
        public static void Run()
        {
            //Configure AutoFac  
            AutofacWebapiConfig.Initialize(GlobalConfiguration.Configuration);
        }
    }
}