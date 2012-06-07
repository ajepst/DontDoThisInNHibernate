using System;

namespace Examples.Models
{
   public class ThingWithGhosts
   {
      public virtual Guid id { get; set; }
      public virtual string Name { get; set; }

      public virtual int Age { get; set; }
   }
}