using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Examples.Models
{
   public class ThingWithAnyAnimal
   {
      public virtual string Name { get; set; }
      public virtual IAnimal Pet { get; set; }

      public virtual Guid id { get; set; }
   }

   public interface IAnimal
   {
      string Name { get; set; }
   }

   public class Cow : IAnimal
   {
      public virtual Guid id { get; set; }
      public virtual string Name { get; set; }
   }

   public class Duck : IAnimal
   {
      public virtual Guid id { get; set; }
      public virtual string Name { get; set; }
   }
}
