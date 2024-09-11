using ConsoleApp1.Models.AuditInterface;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConsoleApp1.Models;

public class Employee : IWiretap
{
  public int Id { get; set; }
  public string FirstName { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;

  public string Fullname => $"{FirstName} {LastName}";

  public string Department { get; set; } = string.Empty;
  public int EntryYear { get; set; }
}

public class DataTypeConiguration : IEntityTypeConfiguration<Employee>
{
  public void Configure(EntityTypeBuilder<Employee> builder)
  {
    builder.HasKey(c => c.Id);
    builder.Property(c => c.Id).ValueGeneratedOnAdd();
  }
}
