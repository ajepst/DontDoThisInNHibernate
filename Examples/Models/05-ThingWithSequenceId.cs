using System;

namespace Examples.Models
{
   public class ThingWithSequenceId
   {
      public virtual int id { get; set; }
      public virtual string Name { get; set; }

      public virtual int Age { get; set; }
   }
}