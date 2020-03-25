using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StackoverflowDb.EFCore;
using System.Collections.Generic;
using System.Linq;

namespace EffectiveEFCore.Pages.Posts
{
    public class IndexModel : PageModel
    {
        readonly StackOverflowContext _context;

        System.Func<StackOverflowContext, IEnumerable<StackoverflowDb.EFCore.Data.Posts>> goodQuery = EF.CompileQuery<StackOverflowContext, IEnumerable<StackoverflowDb.EFCore.Data.Posts>>((context) =>
          context.Posts.Take(20));

        public IndexModel(StackOverflowContext context)
        {
            _context = context;
        }

        public List<StackoverflowDb.EFCore.Data.Posts> Posts { get; set; }
        public void OnGet()
        {
           Posts = _context.Posts.Take(20).ToList();

            //  Posts = goodQuery.Invoke(_context).ToList();
        }
    }
}