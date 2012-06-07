using Examples.Models;
using FluentNHibernate.Mapping;

namespace Examples.Mappings
{
   internal class ThingWithPlainOldGuidIdMap : ClassMap<ThingWithPlainOldGuidId>
   {
      public ThingWithPlainOldGuidIdMap()
      {
         Table("PlainOldGuidIdThings");
         Id(x => x.id).GeneratedBy.Guid();
         Map(x => x.Name);
         Map(x => x.Age);
      }
   }
}