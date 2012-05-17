using System;

namespace Examples.Models
{
   public class ThingWithVersionedCompositeId
   {
      public virtual string FirstName { get; set; }
      public virtual string LastName { get; set; }

      public virtual int Age { get; set; }

      public virtual DateTime LastModified { get; set; }

      #region Equals
      public override bool Equals(object obj)
      {
         if (!Equals(FirstName, null) && !Equals(LastName, null))
         {
            var persistentObject = obj as ThingWithCompositeId;
            return (persistentObject != null) && FirstName == persistentObject.FirstName &&
                   LastName == persistentObject.LastName;
         }

         return base.Equals(obj);
      }
      #endregion
      #region GetHashcode
      public override int GetHashCode()
      {
         return (FirstName + "|" + LastName).GetHashCode();
      }
      #endregion
   }    public class ThingWithCompositeId
   {
      public virtual string FirstName { get; set; }
      public virtual string LastName { get; set; }

      public virtual int Age { get; set; }

      #region Equals
      public override bool Equals(object obj)
      {
         if (!Equals(FirstName, null) && !Equals(LastName, null))
         {
            var persistentObject = obj as ThingWithCompositeId;
            return (persistentObject != null) && FirstName == persistentObject.FirstName &&
                   LastName == persistentObject.LastName;
         }

         return base.Equals(obj);
      }
      #endregion
      #region GetHashcode
      public override int GetHashCode()
      {
         return (FirstName + "|" + LastName).GetHashCode();
      }
      #endregion
   } 


   // maybe add on many-to-one-id, might have some of these symptoms?
}