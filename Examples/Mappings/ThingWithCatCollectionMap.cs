using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Examples.Models;
using FluentNHibernate.Mapping;

namespace Examples.Mappings
{
  public class ThingWithCatCollectionMap :ClassMap<ThingWithCatCollection>
   {
      public ThingWithCatCollectionMap()
      {
         Table("ThingsWithCatCollections");
         Id(x => x.id).GeneratedBy.Guid();
         Map(x => x.Name);
         HasMany(x => x.Cats).Cascade.AllDeleteOrphan(); //.Inverse
      }
   }

   public class CatMap :ClassMap<Cat>
   {
      public CatMap()
      {
         Table("Cats");
         Id(x => x.id).GeneratedBy.Guid();
         Map(x => x.Name);
        // References(x => x.Thing);
      }
   }
}
