using System;
using System.IO;
using System.Reflection;
using FluentNHibernate.Cfg;
using NHibernate.Cfg;

namespace Examples.Initialization
{
   public class ConfigurationFactoryUnused
   {
      private readonly Assembly _assemblyContainingClassMaps;
      private const string ConfigurationFileName = "hibernate.cfg.xml";

      public ConfigurationFactoryUnused(Assembly assemblyContainingClassMaps)
      {
         _assemblyContainingClassMaps = assemblyContainingClassMaps;
      }

      public Configuration Build()
      {
         var cfgFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationFileName);
         if (!File.Exists(cfgFile))
         {
            cfgFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", ConfigurationFileName);
         }

         return Build(cfgFile);
      }

      public Configuration Build(string configurationFile)
      {
         var configuration = new Configuration();
         configuration.Configure(configurationFile);

         var fluentConfiguration = Fluently.Configure(configuration).Mappings(AddMappings).BuildConfiguration();
         return fluentConfiguration;
      }

      private void AddMappings(MappingConfiguration configuration)
      {
         AddHbmMappingFiles(configuration);
         AddFluentMappingClasses(configuration);
      }

      private void AddFluentMappingClasses(MappingConfiguration cfg)
      {
         var conventions = cfg.FluentMappings.AddFromAssembly(_assemblyContainingClassMaps).Conventions;
    //     conventions.Add<CollectionAccessConvention>();
     //    conventions.Add<ListItemTypeConvention>();
    //     conventions.Add<EnumTypeConvention>();
      }

      private void AddHbmMappingFiles(MappingConfiguration cfg)
      {
         cfg.HbmMappings.AddFromAssembly(_assemblyContainingClassMaps);
      }
   }
}