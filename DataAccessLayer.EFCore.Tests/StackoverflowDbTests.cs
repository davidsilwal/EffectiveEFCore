using Microsoft.EntityFrameworkCore;
using StackoverflowDb.EFCore;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DataAccessLayer.EFCore.Tests
{
    public class StackoverflowDbTests
    {

        StackOverflowContext _context;
        System.Func<StackOverflowContext, IEnumerable<StackoverflowDb.EFCore.Data.Posts>> goodQuery;


        public StackoverflowDbTests()
        {
            var option = new DbContextOptionsBuilder<StackOverflowContext>();
            var conn = "Server=.;Database=StackOverflow2010;Trusted_Connection=True;";
            option.UseSqlServer(conn);

            _context = new StackOverflowContext(option.Options);

            goodQuery = EF.CompileQuery<StackOverflowContext, IEnumerable<StackoverflowDb.EFCore.Data.Posts>>((context) =>
                             context.Posts.Take(20));
        }

        [Fact]
        public void Take20_Default()
        {
            var result = _context.Posts.Take(20).ToList();
            Assert.NotEmpty(result);
        }


        [Fact]

        public void Take20_CompiledQuery()
        {     

            var result = goodQuery.Invoke(_context).ToList();

            Assert.NotEmpty(result);
        }
    }
}
