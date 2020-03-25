using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;
using StackoverflowDb.EFCore;
using StackoverflowDb.EFCore.Data;
using System.Linq;

namespace DataAccessLayer.Benchmark
{
    [MemoryDiagnoser]
    [SimpleJob(BenchmarkDotNet.Jobs.RuntimeMoniker.NetCoreApp31)]
    // [MarkdownExporter, AsciiDocExporter, HtmlExporter, CsvExporter, RPlotExporter]
    public class StackoverflowDbBenchmark
    {
        StackOverflowContext _context;


        [GlobalSetup]
        public void Setup()
        {
            var option = new DbContextOptionsBuilder<StackOverflowContext>();
            var conn = "Server=.;Database=StackOverflow2010;Trusted_Connection=True;";
            option.UseSqlServer(conn);

            _context = new StackOverflowContext(option.Options);

        }


        [Benchmark(Description = "Take 20")]

        public void Take20_Default()
        {
            _context.Posts.Take(20).ToList();
        }


        [Benchmark(Description = "Take 20 using compiled query")]

        public void Take20_CompiledQuery()
        {
            _context.GetAllTop20Posts();
        }


        [Benchmark(Description = "Take 20 using view")]
        public void Take20_View()
        {
            _context.PostComments.Take(20).ToList();
        }

        [Benchmark(Description = "Take 20 linq join")]

        public void Take20_Join()
        {
            var query = from p in _context.Posts
                        join c in _context.Comments on p.Id equals c.PostId
                        select new PostComments
                        {
                            Body = p.Body,
                            Id = p.Id,
                            OwnerUserId = p.OwnerUserId,
                            Text = c.Text,
                            Score = c.Score
                        };
            query.Take(20).ToList();
        }


        [Benchmark(Description = "Take 20 with store proc")]
        public void Take20_usp()
        {
            _context.PostComments.FromSqlRaw("EXEC [dbo].[usp_GetPostComments]").ToList();
        }

        [Benchmark(Description = "Take 20 MemoryOptimizedTable")]
        public void Take20_MemoryOptimizedTable()
        {
            _context.MemoryOptimizedPosts.Take(20).ToList();
        }
    }
}
