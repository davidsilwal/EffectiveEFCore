using Microsoft.EntityFrameworkCore;
using StackoverflowDb.EFCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StackoverflowDb.EFCore
{
    public partial class StackOverflowContext : DbContext
    {

        private static Func<StackOverflowContext, int, Posts> _getPost =
         EF.CompileQuery((StackOverflowContext context, int id) =>
             context.Posts
                    .Where(c => c.Id == id)
                    .FirstOrDefault());

        private static readonly Func<StackOverflowContext, List<Posts>> _queryGetAllPosts =
            EF.CompileQuery((StackOverflowContext db) => db.Posts.Take(20).AsNoTracking().ToList());

        private static readonly Func<StackOverflowContext, int, List<Posts>> _queryGetPost =
            EF.CompileQuery((StackOverflowContext db, int id) =>
                db.Posts.Where(a => a.Id == id).AsNoTracking().ToList());

        private static readonly Func<StackOverflowContext, int, List<Posts>> _queryGetPostByPostId =
            EF.CompileQuery((StackOverflowContext db, int id) => db.Posts.Where(a => a.Id == id).AsNoTracking()
                .ToList());

        public Posts GePost(int id) => _getPost(this, id);

        public List<Posts> GetAllTop20Posts() => _queryGetAllPosts(this);

        public List<Posts> GetPostsAsync(int id) => _queryGetPost(this, id);

        public List<Posts> GetPostByPostId(int id) => _queryGetPostByPostId(this, id);


        public StackOverflowContext(DbContextOptions<StackOverflowContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Badges> Badges { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<LinkTypes> LinkTypes { get; set; }
        public virtual DbSet<PostLinks> PostLinks { get; set; }
        public virtual DbSet<PostTypes> PostTypes { get; set; }
        public virtual DbSet<Posts> Posts { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<VoteTypes> VoteTypes { get; set; }
        public virtual DbSet<Votes> Votes { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Badges>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(40);
            });

            modelBuilder.Entity<Comments>(entity =>
            {
                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(700);
            });

            modelBuilder.Entity<LinkTypes>(entity =>
            {
                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PostLinks>(entity =>
            {
                entity.Property(e => e.CreationDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<PostTypes>(entity =>
            {
                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Posts>(entity =>
            {
                entity.Property(e => e.Body).IsRequired();

                entity.Property(e => e.ClosedDate).HasColumnType("datetime");

                entity.Property(e => e.CommunityOwnedDate).HasColumnType("datetime");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.LastActivityDate).HasColumnType("datetime");

                entity.Property(e => e.LastEditDate).HasColumnType("datetime");

                entity.Property(e => e.LastEditorDisplayName).HasMaxLength(40);

                entity.Property(e => e.Tags).HasMaxLength(150);

                entity.Property(e => e.Title).HasMaxLength(250);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.EmailHash).HasMaxLength(40);

                entity.Property(e => e.LastAccessDate).HasColumnType("datetime");

                entity.Property(e => e.Location).HasMaxLength(100);

                entity.Property(e => e.WebsiteUrl).HasMaxLength(200);
            });

            modelBuilder.Entity<VoteTypes>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Votes>(entity =>
            {
                entity.Property(e => e.CreationDate).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
