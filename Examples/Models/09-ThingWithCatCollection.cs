using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Examples.Models
{
   public class ThingWithCatCollection
   {
      private IList<Cat> _cats = new List<Cat>();

      public virtual  Guid id { get; set; }

      public virtual IList<Cat> Cats
      {
         get { return _cats; }
         set { _cats = value; }
      }

      public virtual string Name { get; set; }
   }

   public class Cat
   {
      public virtual Guid id { get; set; }
      public virtual string Name { get; set; }
      // public virtual ThingWithCatCollection Thing {get; set;};
   }
}
