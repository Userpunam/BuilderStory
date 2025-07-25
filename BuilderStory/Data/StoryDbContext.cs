using BuilderStory.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace BuilderStory.Data;
public class StoryDbContext : DbContext
{
    public StoryDbContext(DbContextOptions<StoryDbContext> options) : base(options) { }

    public DbSet<Story> Stories { get; set; }
    public DbSet<StoryImage> StoryImages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Story>()
            .HasMany(s => s.Images)
            .WithOne(i => i.Story)
            .HasForeignKey(i => i.StoryId);
      
        modelBuilder.Entity<Story>()
            .HasIndex(s => s.Word)
            .IsUnique();
    }
}
