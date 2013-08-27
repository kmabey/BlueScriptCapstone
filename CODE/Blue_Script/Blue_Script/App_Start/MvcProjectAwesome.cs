using System.Web.Mvc;
using Omu.Awesome.Mvc;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Blue_Script.App_Start.MvcProjectAwesome), "Start")]
namespace Blue_Script.App_Start
{    
    public static class MvcProjectAwesome
    {
        public static void Start()
        {
            ModelMetadataProviders.Current = new AwesomeModelMetadataProvider();
        }
    }
}