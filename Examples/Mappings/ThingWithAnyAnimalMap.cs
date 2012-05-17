using System;
using Examples.Models;
using FluentNHibernate.Mapping;

namespace Examples.Mappings
{
   public class ThingWithAnyAnimalMap : ClassMap<ThingWithAnyAnimal>
   {
      public ThingWithAnyAnimalMap()
      {
         Table("AnyAnimalThings");
         Id(x => x.id).GeneratedBy.Guid();
         Map(x => x.Name);
         ReferencesAny(x => x.Pet)
            .EntityTypeColumn("PetType")
            .EntityIdentifierColumn("PetId")
            .AddMetaValue<Cow>("cow")
            .AddMetaValue<Duck>("duck") 
            .IdentityType<Guid>().Cascade.All();
      }
   }

   public class CowMap : ClassMap<Cow>
   {
      public CowMap()
      {
         Table("Cows");
         Id(x => x.id).GeneratedBy.Guid();
         Map(x => x.Name);
      }
   }

   public class DuckMap : ClassMap<Duck>
   {
      public DuckMap()
      {
         Table("Ducks");
         Id(x => x.id).GeneratedBy.Guid();
         Map(x => x.Name);
      }
   }
}