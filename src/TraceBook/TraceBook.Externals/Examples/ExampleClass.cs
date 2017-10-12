using System;
using System.Collections.Generic;
using System.Text;
using Thalus.Nuntius.Core;
using Thalus.Nuntius.Core.Contracts;
using Thalus.Nuntius.Core.Tracing;
using Thalus.Nuntius.Core.Tracing.Contracts;

namespace TraceBook.Externals.Examples
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
