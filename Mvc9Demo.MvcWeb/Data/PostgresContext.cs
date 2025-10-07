using Microsoft.EntityFrameworkCore;

namespace Mvc9Demo.MvcWeb.Data;

public class PostgresContext : DbContext
{
    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
    }
    
    public DbSet<Models.Item> Items { get; set; } = null!; 
}