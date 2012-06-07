using Examples.Models;
using FluentNHibernate.Mapping;

namespace Examples.Mappings
{
   internal class ThingWithGhostsMap : ClassMap<ThingWithGhosts>
   {
      public ThingWithGhostsMap()
      {
         Table("GhostThings");
         Id(x => x.id).GeneratedBy.Guid();
         Map(x => x.Name);
         Map(x => x.Age);
      }
   }
}