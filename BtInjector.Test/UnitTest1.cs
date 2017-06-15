using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using BtInjector;
using BtInjector.Test.Models;
using Xunit.Runners;
using System.Threading;

namespace BtInjector.Test
{
    public class MatrixTheoryData<T1> : TheoryData<T1>
    {
        public MatrixTheoryData(IEnumerable<T1> data1)
        {
            Assert.True(data1 != null && data1.Any());

            foreach (T1 t1 in data1)
                Add(t1);
        }
    }

    public class MatrixTheoryData<T1, T2> : TheoryData<T1, T2>
    {
        public MatrixTheoryData(IEnumerable<T1> data1, IEnumerable<T2> data2)
        {
            Assert.True(data1 != null && data1.Any());
            Assert.True(data2 != null && data2.Any());

            foreach (T1 t1 in data1)
                foreach (T2 t2 in data2)
                    Add(t1, t2);
        }
    }

    public class BtInjectorTestClass
    {
        // We use consoleLock because messages can arrive in parallel, so we want to make sure we get
        // consistent console output.
        static object consoleLock = new object();

        // Use an event to know when we're done
        static ManualResetEvent finished = new ManualResetEvent(false);

        // Start out assuming success; we'll set this to 1 if we get a failed test
        static int result = 0;

        static int Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var dllPath = System.Reflection.Assembly.GetEntryAssembly().Location;

            // OpenCoverをつかうなら同じAppDomainである必要あり
            // using (var runner = AssemblyRunner.WithAppDomain(dllPath))
            using (var runner = AssemblyRunner.WithoutAppDomain(dllPath))
            {
                runner.OnDiscoveryComplete = OnDiscoveryComplete;
                runner.OnExecutionComplete = OnExecutionComplete;
                runner.OnTestFailed = OnTestFailed;
                runner.OnTestSkipped = OnTestSkipped;

                Console.WriteLine("Discovering...");
                runner.Start(null);

                finished.WaitOne();
                finished.Dispose();

                return result;
            }
        }

        static void OnDiscoveryComplete(DiscoveryCompleteInfo info)
        {
            lock (consoleLock)
                Console.WriteLine($"Running {info.TestCasesToRun} of {info.TestCasesDiscovered} tests...");
        }

        static void OnExecutionComplete(ExecutionCompleteInfo info)
        {
            lock (consoleLock)
            {
                Console.WriteLine("  === TEST EXECUTION SUMMARY ===");
                Console.WriteLine($"Finished: {info.TotalTests} tests in {Math.Round(info.ExecutionTime, 3)}s ({info.TestsFailed} failed, {info.TestsSkipped} skipped)");
                // BtInjector.Test.Fx462  Total: 18, Errors: 0, Failed: 0, Skipped: 0, Time: 0.241s
            }

            finished.Set();
        }

        static void OnTestFailed(TestFailedInfo info)
        {
            lock (consoleLock)
            {
                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine("[FAIL] {0}: {1}", info.TestDisplayName, info.ExceptionMessage);
                if (info.ExceptionStackTrace != null)
                    Console.WriteLine(info.ExceptionStackTrace);

                Console.ResetColor();
            }

            result = 1;
        }

        static void OnTestSkipped(TestSkippedInfo info)
        {
            lock (consoleLock)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[SKIP] {0}: {1}", info.TestDisplayName, info.SkipReason);
                Console.ResetColor();
            }
        }

        static Func<Lifecycle, Container>[] containerCreators = {
            (Lifecycle lifecycle) =>
            {
                var container = new Container();
                container.For<IRepository>().As<Repository>(lifecycle);
                container.For<IAuthenticationService>().As<AuthenticationService>(lifecycle);
                // container.For<UserController>().As<UserController>(lifecycle);

                container.For<IWebService>().As<WebService>(lifecycle);
                container.For<IAuthenticator>().As<Authenticator>(lifecycle);
                container.For<IStockQuote>().As<StockQuote>(lifecycle);
                container.For<IDatabase>().As<Database>(lifecycle);
                container.For<IErrorHandler>().As<ErrorHandler>(lifecycle);

                container.For<IService1>().As<Service1>(lifecycle);
                container.For<IService2>().As<Service2>(lifecycle);
                container.For<IService3>().As<Service3>(lifecycle);
                container.For<IService4>().As<Service4>(lifecycle);

                container.For<ILogger>().As<Logger>(lifecycle);

                return container;
            },
            (Lifecycle lifecycle) =>
            {
                var container = new Container();

                container.For<IRepository>().As(() => new Repository(), lifecycle);
                container.For<IAuthenticationService>().As(() => new AuthenticationService(), lifecycle);
                // container.For<UserController>().As((IRepository x, IAuthenticationService y) => new UserController(x, y), lifecycle);

                container.For<IWebService>().As((IAuthenticator x,  IStockQuote y, UserController a1, IService1 a2, IService2 a3, IService3 a4, IService2 a5, IDatabase a6, IDummy9 a7) => new WebService(x, y), lifecycle);
                container.For<IAuthenticator>().As((ILogger x, IErrorHandler y, IDatabase z, UserController a1, IService1 a2, IService2 a3, IService3 a4, IService2 a5) => new Authenticator(x, y, z), lifecycle);
                container.For<IStockQuote>().As((ILogger x, IErrorHandler y, IDatabase z, UserController a1, IService1 a2, IService2 a3, IService3 a4) => new StockQuote(x, y, z), lifecycle);
                container.For<IDatabase>().As((ILogger x, IErrorHandler y, UserController a1, IService1 a2, IService2 a3, IService3 a4) => new Database(x, y), lifecycle);
                container.For<IErrorHandler>().As((Func<Func<Lazy<Lazy<Func<ILogger>>>>> x) => new ErrorHandler(x()().Value.Value()), lifecycle);

                container.For<IService1>().As((Func<ILogger> x, IErrorHandler y, UserController a1, Lazy<IService2> a3, IService3 a4) => new Service1(x(), y), lifecycle);
                container.For<IService2>().As((ILogger x, IErrorHandler y, Func<UserController> a1, IService3 a3) => new Service2(x, y), lifecycle);
                container.For<IService3>().As((ILogger x, IErrorHandler y, UserController a1) => new Service3(x, y), lifecycle);
                container.For<IService4>().As((ILogger x, IErrorHandler y) => new Service4(x, y), lifecycle);

                container.For<IDummy0>().As<Dummy0>(lifecycle);
                container.For<IDummy1>().As<Dummy1>(lifecycle);
                container.For<IDummy2>().As<Dummy2>(lifecycle);
                container.For<IDummy3>().As<Dummy3>(lifecycle);
                container.For<IDummy4>().As<Dummy4>(lifecycle);
                container.For<IDummy5>().As<Dummy5>(lifecycle);
                container.For<IDummy6>().As<Dummy6>(lifecycle);
                container.For<IDummy7>().As<Dummy7>(lifecycle);
                container.For<IDummy8>().As<Dummy8>(lifecycle);
                container.For<IDummy9>().As<Dummy9>(lifecycle);

                container.For<ILogger>().As(new Logger());

                return container;
            }
        };

        public static MatrixTheoryData<Func<Lifecycle, Container>> MatrixData = new MatrixTheoryData<Func<Lifecycle, Container>>(containerCreators);

        [Theory]
        [MemberData(nameof(MatrixData))]
        public void Test1_Singleton(Func<Lifecycle, Container> containerCreator)
        {
            var container = containerCreator(Lifecycle.Singleton);

            var instance = container.GetInstance<IWebService>();
            var instance2 = container.GetInstance<IWebService>();

            Assert.Equal(instance, instance2);
        }

        [Theory]
        [MemberData(nameof(MatrixData))]
        public void Test2_Transient(Func<Lifecycle, Container> containerCreator)
        {
            var container = containerCreator(Lifecycle.Transient);

            var instance = container.GetInstance<IWebService>();
            var instance2 = container.GetInstance<IWebService>();

            Assert.NotEqual(instance, instance2);
        }

        [Theory]
        [MemberData(nameof(MatrixData))]
        public void Test3_ThreadLocal(Func<Lifecycle, Container> containerCreator)
        {
            var container = containerCreator(Lifecycle.ThreadLocal);

            var instance = container.GetInstance<IWebService>();
            var instance2 = container.GetInstance<IWebService>();

            Assert.Equal(instance, instance2);
        }

        [Theory]
        [MemberData(nameof(MatrixData))]
        public void Test4_GetFunc(Func<Lifecycle, Container> containerCreator)
        {
            var container = containerCreator(Lifecycle.Transient);

            var instance = container.GetInstance<Func<IWebService>>();
            var instance2 = container.GetInstance<Func<IWebService>>();

            Assert.Equal(instance, instance2);
            Assert.NotEqual(instance(), instance2());
        }

        [Theory]
        [MemberData(nameof(MatrixData))]
        public void Test5_GetLazy(Func<Lifecycle, Container> containerCreator)
        {
            var container = containerCreator(Lifecycle.Transient);

            var instance = container.GetInstance<Lazy<IWebService>>();
            var instance2 = container.GetInstance<Lazy<IWebService>>();

            Assert.Equal(instance.Value, instance.Value);
            Assert.Equal(instance2.Value, instance2.Value);
            Assert.NotEqual(instance.Value, instance2.Value);
        }

        [Theory]
        [MemberData(nameof(MatrixData))]
        public void Test6_GetFuncLazy(Func<Lifecycle, Container> containerCreator)
        {
            var container = containerCreator(Lifecycle.Transient);

            var instance = container.GetInstance<Func<Lazy<IWebService>>>();
            var instance2 = container.GetInstance<Func<Lazy<IWebService>>>();

            Assert.NotEqual(instance().Value, instance().Value);
            Assert.NotEqual(instance2().Value, instance2().Value);
            Assert.NotEqual(instance().Value, instance2().Value);
        }

        [Theory]
        [MemberData(nameof(MatrixData))]
        public void Test7_GetLazyFunc(Func<Lifecycle, Container> containerCreator)
        {
            var container = containerCreator(Lifecycle.Transient);

            var instance = container.GetInstance<Lazy<Func<IWebService>>>();
            var instance2 = container.GetInstance<Lazy<Func<IWebService>>>();

            Assert.Equal(instance, instance2);
            Assert.Equal(instance.Value, instance2.Value);
            Assert.NotEqual(instance.Value(), instance.Value());
        }

        [Theory]
        [MemberData(nameof(MatrixData))]
        public void Test8_GetFuncSingleton(Func<Lifecycle, Container> containerCreator)
        {
            var container = containerCreator(Lifecycle.Singleton);

            var instance = container.GetInstance<Func<IWebService>>();
            var instance2 = container.GetInstance<Func<IWebService>>();

            Assert.Equal(instance, instance2);
            Assert.Equal(instance(), instance2());
        }

        [Theory]
        [MemberData(nameof(MatrixData))]
        public void Test9_GetLazySingleton(Func<Lifecycle, Container> containerCreator)
        {
            var container = containerCreator(Lifecycle.Singleton);

            var instance = container.GetInstance<Lazy<IWebService>>();
            var instance2 = container.GetInstance<Lazy<IWebService>>();

            Assert.Equal(instance.Value, instance.Value);
            Assert.Equal(instance2.Value, instance2.Value);
            Assert.Equal(instance.Value, instance2.Value);
        }
    }
}
