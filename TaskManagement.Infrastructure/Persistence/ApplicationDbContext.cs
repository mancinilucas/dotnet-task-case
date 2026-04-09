using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public DbSet<TaskItem> Tasks => Set<TaskItem>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
}