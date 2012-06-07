using NHibernate;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using log4net;
using log4net.Config;

namespace Examples.Initialization
{
   public class Bootstrapper
   {
      public static void Bootstrap()
      {
         LoadLogging();
         LoadStructureMapRegistries();
      }

      private static void LoadLogging()
      {
         XmlConfigurator.Configure();
         LogManager.GetLogger(typeof(Bootstrapper)).Info("Logging Initialized");
      }


      public static void LoadStructureMapRegistries()
      {
         ObjectFactory.Initialize(x => x.AddRegistry<AppRegistry>());


      }

   }

   public class AppRegistry : Registry
   {
      public AppRegistry()
      {
         Scan(FindAllStructureMapPlugins);

         For<ISession>().HybridHttpOrThreadLocalScoped().Use(() => ObjectFactory.GetInstance<ISessionBuilder>().GetSession(typeof(Bootstrapper).Assembly));
      }

      private static void FindAllStructureMapPlugins(IAssemblyScanner x)
      {
         x.AssemblyContainingType<Bootstrapper>();
         x.TheCallingAssembly();
         x.WithDefaultConventions();

      }
   }
}