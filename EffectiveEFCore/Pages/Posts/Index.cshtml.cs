using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StackoverflowDb.EFCore;
using StackoverflowDb.EFCore.Data;
using System.Collections.Generic;
using System.Linq;

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
            // Posts = _context.Posts.Take(20).ToList();
            //   Posts = _context.GetAllTop20Posts();


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
            query.TagWith("using linq").Take(20).ToList();

            _context.PostComments.TagWith("with view").Take(20).ToList();



        }
    }
}