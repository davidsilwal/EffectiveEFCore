using System;
using System.Collections.Generic;

namespace StackoverflowDb.EFCore
{
    public partial class Badges
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
    }
}
