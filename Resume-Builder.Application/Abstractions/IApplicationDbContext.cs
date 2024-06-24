using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Resume_Builder.Domain.Entities.Resume;
using Resume_Builder.Domain.Entities.User;

namespace Resume_Builder.Application.Abstractions;

public interface IApplicationDbContext
{
    public DbSet<Resumes> Resumes { get; set; }
    public DbSet<Users> Users { get; set; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default); 
}

