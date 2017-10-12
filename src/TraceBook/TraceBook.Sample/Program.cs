using System;
using System.Threading;
using Thalus.Nuntius.Core;
using Thalus.Nuntius.Core.Contracts;
using Thalus.Nuntius.Core.Stringify;
using Thalus.Nuntius.Core.Tracing;
using Thalus.Nuntius.Core.Tracing.Contracts;

namespace TraceBook.Sample
{
    class Program
    {
        static Program()
        {
            //STraceBook.Register(new ConsolePusher());
            //  STraceBook.Register(new RollingFilePusher(10000, "logfile.csv",new CsvStringifier()));

         //   var sink = new NamedPipePusher<>("log");

       //     STraceBook.Register(sink);
            //   STraceBook.Register(new SentryTraceWriter("asfasasfsaf:sfsaffdfd", Level.Error | Level.Fatal));

            _mainTrace = STraceBook.Get(nameof(Main));
        }

        private static ITraceBook _mainTrace;
        static void Main(string[] args)
        {
            try
            {
                //NamedPipeReceiver<> receiver = new NamedPipeReceiver<>("log");
                //receiver.EntryReceivedEvent += Source_EntryReceivedEvent;

           //     receiver.Start();
                
                STraceBook.Get().Debug("Main has been started");

                ExampleClass exmpl = new ExampleClass();

                exmpl.Perfom();

                throw new Exception("Just to check");
            }
            catch (Exception e)
            {
                STraceBook.Get().Fatal("Something went wrong", new object[] {e.Message, args});
            }

            while (true)
            {
                Thread.Sleep(2000);
            }

            STraceBook.Cleanup();
        }

        private static void Source_EntryReceivedEvent(ITraceEntryFacade obj)
        {
          
        }
    }

    public class ExampleClass
    {
        private ITraceBook _tracebook = STraceBook.Get(nameof(ExampleClass));

        public void Perfom()
        {
            _tracebook.Warning("Perform called");
        }
    }
}
