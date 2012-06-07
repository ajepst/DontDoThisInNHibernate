using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace Examples.Initialization
{

   public interface ISessionFactoryBuilder
   {
      ISessionFactory GetFactory(Assembly assemblyContainingClassMaps);
   }

   public class SessionFactoryBuilder : ISessionFactoryBuilder
   {
      private static ISessionFactory _factory;
      private static readonly object _lock = new object();

      public ISessionFactory GetFactory(Assembly assemblyContainingClassMaps)
      {
         if (_factory == null)
         {
            lock (_lock)
            {
               if (_factory == null)
               {
                  var config = Fluently
                     .Configure()
                     .Database(MsSqlConfiguration.MsSql2005.ConnectionString(x => x.FromConnectionStringWithKey("dontDoThisDb"))) //.ShowSql()
                     .Mappings(x =>
                                  {
                                     x.FluentMappings.AddFromAssembly(assemblyContainingClassMaps).ExportTo(
                                        @"c:\mappings");
                                     x.HbmMappings.AddFromAssembly(assemblyContainingClassMaps);
                                  })
                     .BuildConfiguration();
                  _factory = config.BuildSessionFactory();
                  return _factory;
               }
            }
         }

         return _factory;
      }
   }
}