using System;
using System.Collections.Generic;
using System.Configuration;
using Examples.Models;
using NHibernate;
using NUnit.Framework;
using System.Data;
using System.Data.SqlClient;

namespace Tests
{
   [TestFixture]
   public class ThingWithAssignedIdTest : DataTestBase
   {
      [Test]
      public void AddNewAssignedIdThing()
      {
         var testThing = new ThingWithAssignedId {Name = "Thing 1", Age = 4};

         //this works... now, but see profiler!
         using (ISession session = GetSession())
         {
            session.SaveOrUpdate(testThing);
            session.Flush();
         }

         using (ISession session = GetSession())
         {
            var getAgain = session.Get<ThingWithAssignedId>(testThing.Age);
            getAgain.Name = "qqq";
            session.SaveOrUpdate(getAgain);
            session.Flush();
         }
      }
   }

   [TestFixture]
   public class ThingWithCompositeIdTest : DataTestBase
   {
      [Test]
      public void AddNewCompositeIdThing()
      {
         var testThing = new ThingWithCompositeId { FirstName = "Thing", LastName = "One", Age = 4 };

         // this will work
         using (ISession session = GetSession())
         {
            session.Save(testThing);
            session.Flush();
         }

         //this works ... now - but see profiler!
//         using (ISession session = GetSession())
//         {
//            session.SaveOrUpdate(testThing);
//            session.Flush();
//         }

         // this is horrible but works
         using (ISession session = GetSession())
         {
            var getAgain = session.Get<ThingWithCompositeId>(new ThingWithCompositeId { FirstName = "Thing", LastName = "One" });
            getAgain.Age = 5;
            session.SaveOrUpdate(getAgain);
            session.Flush();
         }
      }
   }

   [TestFixture]
   public class ThingWithVersionedCompositeIdTest : DataTestBase
   {
      [Test]
      public void AddNewVersionedCompositeIdThing()
      {
         var testThing = new ThingWithVersionedCompositeId { FirstName = "Thing", LastName = "One", Age = 4 };

         // this will work
//         using (ISession session = GetSession())
//         {
//            session.Save(testThing);
//            session.Flush();
//         }

         // no double query?
         using (ISession session = GetSession())
         {
            session.SaveOrUpdate(testThing);
            session.Flush();
         }

         // this is horrible but works
         using (ISession session = GetSession())
         {
            var getAgain = session.Get<ThingWithVersionedCompositeId>(new ThingWithVersionedCompositeId { FirstName = "Thing", LastName = "One" });
            getAgain.Age = 5;
            session.SaveOrUpdate(getAgain);
            session.Flush();
         }
      }
   }



   [TestFixture]
   public class ThingWithPlainOldGuidIdTest : DataTestBase
   {
      [Test]
      public void AddNewIdentityIdThing()
      {
         var testThing = new ThingWithPlainOldGuidId { Name = "Thing", Age = 4 };


         using (ISession session = GetSession())
         {
            session.SaveOrUpdate(testThing);
            // flush - what's it do?
            session.Flush();
         }
      }
   }



   [TestFixture]
   public class ThingWithIdentityIdTest : DataTestBase
   {
      [Test]
      public void AddNewIdentityIdThing()
      {
         var testThing = new ThingWithIdentityId { Name = "Thing", Age = 4 };


         using (ISession session = GetSession())
         {
            session.SaveOrUpdate(testThing);
            // flush? what's it do?
            session.Flush();
         }
      }
   }


   [TestFixture]
   public class ThingWithSequenceIdTest : DataTestBase
   {
      [Test]
      public void AddThingWithSequenceId()
      {
         var testThing = new ThingWithSequenceId { Name = "Thing", Age = 4 };


         using (ISession session = GetSession())
         {
            session.SaveOrUpdate(testThing);
            // flush? what's it do?
            session.Flush();
         }
      }
   }

   [TestFixture]
   public class ThingWithPropertyRefParentTest : DataTestBase
   {
      [Test]
      public void AddThingWithSequenceId()
      {
         var parentThing = new ThingWithPropertyRefParent { Name = "Mr. Parent", Age = 25 };
         var childThing = new ThingWithPropertyRefParent { Name = "Child Thing", Age = 4, ParentThing = parentThing};

         using (ISession session = GetSession())
         {
            session.SaveOrUpdate(parentThing);
            session.SaveOrUpdate(childThing);
            
            session.Flush();
         }
      }
   }

   [TestFixture]
   public class ThingWithOneToOneTest : DataTestBase
   {
      [Test]
      public void AddThingWithOneToOneId()
      {
         var parentThing = new ThingWithOneToOneLeftHalf { Name = "Thing 1", LeftArmLength = 2, LeftLegLength = 3};
         var childThing = new ThingWithOneToOneRightHalf { RightArmLength = 4, RightLegLength = 5, LeftHalf = parentThing};
         parentThing.RightHalf = childThing;

         using (ISession session = GetSession())
         {
            session.SaveOrUpdate(parentThing);


            session.Flush();
         }
      }
   }

   [TestFixture]
   public class ThingWithAnyAnimalTest : DataTestBase
   {
      [Test]
      public void AddThingWithAnyAnimal()
      {
         var cow = new Cow {Name = "Bessie"};
         var thing = new ThingWithAnyAnimal { Name = "Thing 1", Pet = cow};
        

         using (ISession session = GetSession())
         {
            session.SaveOrUpdate(thing);

            session.Flush();
         }
      }
   }


   [TestFixture]
   public class ThingWithCatCollectionTest : DataTestBase
   {
      [Test]
      public void AddThingWithCatCollection()
      {
         var cat1 = new Cat { Name = "Fluffy" };
         var cat2 = new Cat { Name = "Meowser" };
         var cat3 = new Cat { Name = "Mittens" };
         var thing = new ThingWithCatCollection { Name = "Thing 1", Cats = new List<Cat>{cat1, cat2, cat3}};

         using (ISession session = GetSession())
         {
            //note commands sent to db 
            session.SaveOrUpdate(thing);

            session.Flush();
         }
      }
   }

   [TestFixture]
   public class ThingWithGhostsTest : DataTestBase
   {
      [Test]
      public void AddThingWithGhosts()
      {
         var newId = Guid.NewGuid();
      using(ISession session = GetSession())
      {
         // Create the Command and Parameter objects.
         ExecuteNonQuery(session.Connection, "insert GhostThings (id, name, age) values ('" + newId + "', 'Casper', null)", "inserting");
      }
         using (ISession session = GetSession())
         {
            //note commands sent to db ...update? 
            session.Get<ThingWithGhosts>(newId);

            session.Flush();
         }
      }
   }
}