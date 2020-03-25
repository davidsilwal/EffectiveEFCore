using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;
using StackoverflowDb.EFCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.Benchmark
{
    [MemoryDiagnoser]
    [SimpleJob(BenchmarkDotNet.Jobs.RuntimeMoniker.NetCoreApp31)]
    [ThreadingDiagnoser]
    [RPlotExporter, RankColumn]
    public class StackoverflowDbBenchmark
    {
        StackOverflowContext _context;

        System.Func<StackOverflowContext, IEnumerable<StackoverflowDb.EFCore.Data.Posts>> goodQuery = EF.CompileQuery<StackOverflowContext, IEnumerable<StackoverflowDb.EFCore.Data.Posts>>((context) =>
                  context.Posts.Take(20));


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
            goodQuery.Invoke(_context).ToList();
        }

    }
}
