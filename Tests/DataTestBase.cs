using System;
using System.Data;
using System.Reflection;
using Examples.Initialization;
using Examples.Models;
using NHibernate;
using NUnit.Framework;
using StructureMap;

namespace Tests
{

   public class TestingBootstrapper
   {
      private static bool _dependenciesRegistered;

      private static readonly object Sync = new object();

      public static void RegisterDependenciesForTesting()
      {
         if (!_dependenciesRegistered)
         {
            lock (Sync)
            {
               if (!_dependenciesRegistered)
               {
                  Bootstrapper.Bootstrap();
                  _dependenciesRegistered = true;
               }
            }
         }
      }
   }

  
   public abstract class DataTestBase : TestBase
   {
      [SetUp]
      public virtual void Setup()
      {
         SetCurrentUserOnThread();
         SetupStructureMap();
         DeleteData();
      }

      [TearDown]
      public virtual void Teardown()
      {
      }

      protected virtual void SetupStructureMap()
      {
         TestingBootstrapper.RegisterDependenciesForTesting();
      }

      protected ISession GetSession()
      {
         var sessionFactory = GetSessionFactory();
         var openSession = sessionFactory.OpenSession();
         return openSession;
      }

//      protected void PersistEntities(params ViewModelObject[] entities)
//      {
//         using (ISession session = GetSession())
//         {
//            foreach (var entity in entities)
//            {
//               session.Save(entity);
//            }
//            session.Flush();
//         }
//      }

      protected void AssertObjectWasPersisted<T>(T persistentObject, object id) 
      {
         using (var session = GetSession())
         {
            var reloadedObject = session.Get<T>(id);
            Assert.That(reloadedObject, Is.EqualTo(persistentObject));
            Assert.That(reloadedObject, Is.Not.SameAs(persistentObject));
            AssertObjectsMatch(persistentObject, reloadedObject);
         }
      }
      private static ISessionFactory GetSessionFactory()
      {
         var sessionFactoryBuilder = ObjectFactory.GetInstance<ISessionFactoryBuilder>();
         return sessionFactoryBuilder.GetFactory(GetAssemblyContainingClassMaps());
      }

//      private void AssertObjectWasPersisted<T>(T persistentObject) where T : ViewModelObject
//      {
//         using (var session = GetSession())
//         {
//            var reloadedObject = session.Get<T>(persistentObject.Id);
//            Assert.That(reloadedObject, Is.EqualTo(persistentObject));
//            Assert.That(reloadedObject, Is.Not.SameAs(persistentObject));
//            AssertObjectsMatch(persistentObject, reloadedObject);
//         }
//      }

      private static void AssertObjectsMatch(object obj1, object obj2)
      {
         Assert.AreNotSame(obj1, obj2);
         Assert.AreEqual(obj1, obj2);

         PropertyInfo[] infos = obj1.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
         foreach (var info in infos)
         {
            object value1 = info.GetValue(obj1, null);
            object value2 = info.GetValue(obj2, null);

            Assert.AreEqual(value1, value2, string.Format("Property {0} doesn't match", info.Name));
         }
      }

      private static void DeleteData()
      {
         var sessionFactory = GetSessionFactory();
         var deleter = new DatabaseDeleter(sessionFactory);
         deleter.DeleteAllData();
      }

      protected static Assembly GetAssemblyContainingClassMaps()
      {
         return typeof(ThingWithAssignedId).Assembly;
      }

      protected static void ExecuteNonQuery(IDbConnection conn, string script, string alert)
      {
         using (var cmd = conn.CreateCommand())
         {
            cmd.CommandText = script;
            Console.WriteLine(alert);
            cmd.ExecuteNonQuery();
         }
      }
   }
}