using Microsoft.AspNetCore.Mvc.RazorPages;
using StackoverflowDb.EFCore;
using System.Collections.Generic;

namespace EffectiveEFCore.Pages.Posts
{
    public class IndexModel : PageModel
    {
        readonly StackOverflowContext _context;

        public IndexModel(StackOverflowContext context)
        {
            _context = context;
        }

        public List<StackoverflowDb.EFCore.Data.Posts> Posts { get; set; }
        public void OnGet()
        {
            Posts = _context.GetAllTop20Posts();

            //  Posts = goodQuery.Invoke(_context).ToList();
        }
    }
}