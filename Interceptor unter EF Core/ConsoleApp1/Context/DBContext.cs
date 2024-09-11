using ConsoleApp1.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ConsoleApp1.Context;

public class DBContext : DbContext
{
  private static readonly EfLoggingInterceptor _interceptor = new EfLoggingInterceptor();

  public DBContext(DbContextOptions<DBContext> options)
  : base(options)
  {
    this.EnsureSeedData(); // new line added
  }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder
      .AddInterceptors(
      new EfLoggingInterceptor(), 
      new EfTransactionInterceptor(),
      new EfSaveChangesInterceptor());

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(Employee).Assembly);
  }


  public DbSet<Employee> Data { get; set; }
}
