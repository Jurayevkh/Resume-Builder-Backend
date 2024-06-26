using Microsoft.EntityFrameworkCore;
using Resume_Builder.Application.Abstractions;
using Resume_Builder.Domain.Entities.Resume;
using Resume_Builder.Domain.Entities.User;

namespace Resume_Builder.Infrastructure.Data;

public class ResumeBuilderDbContext : DbContext, IApplicationDbContext
{
    public ResumeBuilderDbContext(DbContextOptions<ResumeBuilderDbContext> options):base(options)
    {
            
    }

    public DbSet<Resumes> Resumes { get; set; }
    public DbSet<Users> Users { get; set; }
}

