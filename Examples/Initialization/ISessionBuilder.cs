using System.Reflection;
using NHibernate;

namespace Examples.Initialization
{

   public interface ISessionBuilder
   {
      ISession GetSession(Assembly assemblyContainingClassMaps);
   }

   
   public class SessionBuilder : ISessionBuilder
   {
      private readonly ISessionFactoryBuilder _factoryBuilder;

      public SessionBuilder(ISessionFactoryBuilder factoryBuilder)
      {
         _factoryBuilder = factoryBuilder;
      }

      public ISession GetSession(Assembly assemblyContainingClassMaps)
      {
         var sessionFactory = _factoryBuilder.GetFactory(assemblyContainingClassMaps);
         var session = sessionFactory.OpenSession();
         return session;
      }
   }

}