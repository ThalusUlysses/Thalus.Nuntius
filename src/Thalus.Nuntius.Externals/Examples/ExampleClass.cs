using System;
using Thalus.Nuntius.Core.Tracing;
using Thalus.Nuntius.Core.Tracing.Contracts;

namespace Thalus.Nuntius.Externals.Examples
{
    class ExampleClass
    {
        static ExampleClass()
        {
          
        }

        private ITraceBook _trace = STraceBook.Get(nameof(ExampleClass));

        public void DoSomething()
        {

            try
            {
                _trace.Debug("I did something useful", new object[] { this });
            }
            catch (Exception e)
            {
                STraceBook.Get().Fatal("Some idiot did something wrong", new object[] {e});
                throw;
            }
        }
    }
}
