using BenchmarkDotNet.Attributes;
using DataAccessLayer.EFCore.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.Benchmark
{
    public class QueryBenchmark
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

        public async Task GetAllPost_CompiledQueryAsync()
        {
            await _context.Posts.Take(20).ToListAsync();
        }


        [Benchmark(Description = "Compiled Query Take 20")]

        public void Take20_CompiledQuery()
        {
            var goodQuery = EF.CompileQuery<StackOverflowContext, List<Posts>>((context) =>
                  context.Posts.Take(20).ToList());

        }

    }
}
