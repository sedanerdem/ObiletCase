using ObiletCase.Interface;
using ObiletCase.Services;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System.Web.Mvc;

namespace ObiletCase.App_Start
{
    public static class SimpleInjectorConfig
    {
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            container.Register<ICallApiService, CallApiService>(Lifestyle.Singleton);
            container.Register<IObiletApiService, ObiletApiService>(Lifestyle.Singleton);
            container.Register<ICacheService, RedisCacheService>(Lifestyle.Singleton);
            container.Register<ILogService, LogService>(Lifestyle.Singleton);
            container.RegisterMvcControllers(System.Reflection.Assembly.GetExecutingAssembly());
            container.Verify();
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}