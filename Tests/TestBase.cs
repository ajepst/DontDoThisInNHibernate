using System.Security.Principal;
using System.Threading;
using Rhino.Mocks;

namespace Tests
{
   public abstract class TestBase
   {
      protected void SetCurrentUserOnThread()
      {
         var currentIdentity = WindowsIdentity.GetCurrent();

         if (currentIdentity != null)
         {
            Thread.CurrentPrincipal = new WindowsPrincipal(currentIdentity);
         }
      }

      protected static T Stub<T>(params object[] argumentsForConstructor) where T : class
      {
         return MockRepository.GenerateStub<T>(argumentsForConstructor);
      }
   }
}