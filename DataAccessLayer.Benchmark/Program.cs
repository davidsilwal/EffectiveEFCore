using BenchmarkDotNet.Running;
using System;

namespace DataAccessLayer.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run(typeof(QueryBenchmark));
        }
    }
}
