using Examples.Models;
using FluentNHibernate.Mapping;

namespace Examples.Mappings
{
   internal class ThingWithPropertyRefParentMap : ClassMap<ThingWithPropertyRefParent>
   {
      public ThingWithPropertyRefParentMap()
      {
         Table("PropertyRefParentThings");
         Id(x => x.id).GeneratedBy.Guid();
         Map(x => x.Name);
         Map(x => x.Age);
         References(x => x.ParentThing).PropertyRef(y => y.Name);
      }
   }
}