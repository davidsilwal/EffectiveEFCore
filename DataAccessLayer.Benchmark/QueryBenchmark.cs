using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;
using StackoverflowDb.EFCore;
using System.Linq;

namespace DataAccessLayer.Benchmark
{
    [MemoryDiagnoser]
    [SimpleJob(BenchmarkDotNet.Jobs.RuntimeMoniker.NetCoreApp31)]
    //[RPlotExporter, RankColumn]
    public class StackoverflowDbBenchmark
    {
        StackOverflowContext _context;


        [GlobalSetup]
        public void Setup()
        {
            var option = new DbContextOptionsBuilder<StackOverflowContext>();
            var conn = "Server=(localdb)\\mssqllocaldb;Database=StackOverflow2010;Trusted_Connection=True;";
            option.UseSqlServer(conn);

            _context = new StackOverflowContext(option.Options);

        }


        [Benchmark(Description = "Take 20")]

        public void Take20_Default()
        {
            _context.Posts.Take(20).ToList();
        }


        [Benchmark(Description = "Compiled Query Take 20")]

        public void Take20_CompiledQuery()
        {
            _context.GetAllTop20Posts();
        }

    }
}
