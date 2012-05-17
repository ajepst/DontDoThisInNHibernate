using Examples.Models;
using FluentNHibernate.Mapping;

namespace Examples.Mappings
{
   public class ThingWithOneToOneLeftHalfMap : ClassMap<ThingWithOneToOneLeftHalf>
   {
      public ThingWithOneToOneLeftHalfMap()
      {
         Table("OneToOneLeftHalf");
         Id(x => x.id).GeneratedBy.Guid();
         Map(x => x.Name);
         Map(x => x.LeftArmLength);
         Map(x => x.LeftLegLength);
         HasOne(x => x.RightHalf).Cascade.All();
      }
   }

   public class ThingWithOneToOneRightHalfMap : ClassMap<ThingWithOneToOneRightHalf>
   {
      public ThingWithOneToOneRightHalfMap()
      {
         Table("OneToOneRightHalf");
         Id(x => x.id).GeneratedBy.Foreign("LeftHalf");
         Map(x => x.RightArmLength);
         Map(x => x.RightLegLength);
         HasOne(x => x.LeftHalf);
      }
   }
}