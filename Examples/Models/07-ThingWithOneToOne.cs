using System;

namespace Examples.Models
{
   public class ThingWithOneToOneLeftHalf
   {
      public virtual Guid id { get; set; }
      public virtual string Name { get; set; }
      public virtual int LeftArmLength { get; set; }
      public virtual int LeftLegLength { get; set; }
      public virtual ThingWithOneToOneRightHalf RightHalf { get; set; }
   }

   public class ThingWithOneToOneRightHalf
   {
      public virtual Guid id { get; set; }
      public virtual ThingWithOneToOneLeftHalf LeftHalf { get; set; }
      public virtual int RightArmLength { get; set; }
      public virtual int RightLegLength { get; set; }
   }
}