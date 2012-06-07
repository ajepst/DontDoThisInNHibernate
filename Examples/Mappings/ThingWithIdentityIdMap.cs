using Examples.Models;
using FluentNHibernate.Mapping;

namespace Examples.Mappings
{
   internal class ThingWithIdentityIdMap : ClassMap<ThingWithIdentityId>
   {
      public ThingWithIdentityIdMap()
      {
         Table("IdentityIdThings");
         Id(x => x.id).GeneratedBy.Identity();
         Map(x => x.Name);
         Map(x => x.Age);
      }
   }
}