using System;

namespace Examples.Models
{
   public abstract class PersistentObject<T>
   {
      public virtual T Id { get; set; }

//      public override bool Equals(object obj)
//      {
//         if (!Equals(Id, Guid.Empty))
//         {
//            var persistentObject = obj as PersistentObject<T>;
//            return (persistentObject != null) && Equals(Id, persistentObject.Id);
//         }
//
//         return base.Equals(obj);
//      }
//
//      public override int GetHashCode()
//      {
//         return !Equals(Id, GetEmptyId()) ? Id.GetHashCode() : base.GetHashCode();
//      }

      protected abstract T GetEmptyId();
   }

   public class PersistantObjectString : PersistentObject<string>
   {
      protected override string GetEmptyId()
      {
         return "";
      }
   }
}