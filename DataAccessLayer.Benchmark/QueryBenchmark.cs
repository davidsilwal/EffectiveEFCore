using BenchmarkDotNet.Attributes;
using DataAccessLayer.EFCore.Data;
using Microsoft.EntityFrameworkCore;
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


        [Benchmark]
        public async Task GetAllPost_CompiledQueryAsync()
        {
            await _context.GePostAsync(1);
        }

    }
}
