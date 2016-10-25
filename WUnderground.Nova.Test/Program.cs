#if NETCOREAPP1_0
using NUnit.Common;
using NUnitLite;
#endif
using System;
using System.Reflection;

namespace WUnderground.Nova.Test
{
    public class Program
    {
        public int Main(string[] args)
        {
#if NETCOREAPP1_0
            return new AutoRun(typeof(Program).GetTypeInfo().Assembly)
                .Execute(args, new ExtendedTextWrapper(Console.Out), Console.In);
#else
            return 0;
#endif

        }
    }
}
