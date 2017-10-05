using System;
using TraceBook.Contracts;
using TraceBook.Externals;
using TraceBook.Stringify;
using TraceBook.Writers;

namespace TraceBook.Sample
{
    class Program
    {
        static Program()
        {
           STrace.Register(new ConsoleTextWriter());
            STrace.Register(new RollingFileWriter(10000, "logfile.csv",new CsvStringifier()));
            //   STrace.Register(new SentryTraceWriter("asfasasfsaf:sfsaffdfd", Level.Error | Level.Fatal));

            _mainTrace = STrace.Get(nameof(Main));
        }

        private static ITraceBook _mainTrace;
        static void Main(string[] args)
        {
            try
            {
                STrace.Get().Debug("Main has been started");

                ExampleClass exmpl = new ExampleClass();

                exmpl.Perfom();

                throw new Exception("Just to check");
            }
            catch (Exception e)
            {
                STrace.Get().Fatal("Something went wrong", new object[] {e.Message, args});
            }

            STrace.Cleanup();
        }
    }

    public class ExampleClass
    {
        private ITraceBook _tracebook = STrace.Get(nameof(ExampleClass));

        public void Perfom()
        {
            _tracebook.Warning("Perform called");
        }
    }
}
