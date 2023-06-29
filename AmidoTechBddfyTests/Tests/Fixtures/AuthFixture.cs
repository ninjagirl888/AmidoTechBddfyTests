using System;

namespace AmidoTechBddfyTests.Tests.Fixtures
{
    public class AuthFixture : IDisposable
    {
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //fixture tear down goes here
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
