using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace DataAccessLayer.Benchmark
{
    [MemoryDiagnoser]
    [SimpleJob(BenchmarkDotNet.Jobs.RuntimeMoniker.NetCoreApp31)]
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run(typeof(QueryBenchmark));
        }
    }
}
