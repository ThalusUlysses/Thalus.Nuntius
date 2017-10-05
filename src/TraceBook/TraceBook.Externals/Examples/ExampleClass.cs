using System;
using System.Collections.Generic;
using System.Text;
using TraceBook.Contracts;
using TraceBook.Writers;

namespace TraceBook.Externals.Examples
{
    class ExampleClass
    {
        static ExampleClass()
        {
          
        }

        private ITraceBook _trace = STrace.Get(nameof(ExampleClass));

        public void DoSomething()
        {

            try
            {
                _trace.Debug("I did something useful", new object[] { this });
            }
            catch (Exception e)
            {
                STrace.Get().Fatal("Some idiot did something wrong", new object[] {e});
                throw;
            }
        }
    }
}
