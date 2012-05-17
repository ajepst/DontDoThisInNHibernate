using Examples.Models;
using FluentNHibernate.Mapping;

namespace Examples.Mappings
{
   class ThingWithCompositeIdMap : ClassMap<ThingWithCompositeId>
   {
      public ThingWithCompositeIdMap()
      {
         Table("CompositeIdThings");
         CompositeId().KeyProperty(x => x.FirstName).KeyProperty(x => x.LastName);
         Map(x => x.Age);
      }
   }

   class ThingWithVersionedCompositeIdMap : ClassMap<ThingWithVersionedCompositeId>
   {
      public ThingWithVersionedCompositeIdMap()
      {
         Table("CompositeIdVersionedThings");
         CompositeId().KeyProperty(x => x.FirstName).KeyProperty(x => x.LastName);
         Version(x => x.LastModified);
         Map(x => x.Age);
      }
   }
}