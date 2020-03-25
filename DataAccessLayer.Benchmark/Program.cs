using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace DataAccessLayer.Benchmark
{

    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run(typeof(StackoverflowDbBenchmark));
        }
    }
}
