using ConsoleApp1.Models.AuditInterface;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ConsoleApp1.Interceptors
{
  public  class EfSaveChangesInterceptor : SaveChangesInterceptor
  {
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
      DbContext? dbContext = eventData.Context;

      if (dbContext == null)
      {
        return base.SavingChanges(eventData, result);
      }


      var entities = dbContext!.ChangeTracker.Entries<IWiretap>().ToList();

      foreach (var entry in entities)
      {
        var auditMessage = entry.State switch
        {
          EntityState.Deleted => CreateDeleted(entry),
          EntityState.Modified => CreateModified(entry),
          EntityState.Added => CreateAdded(entry),
          _ => null
        };
        if (auditMessage != null)
        {
          Console.WriteLine(auditMessage);
        }
      }

      return base.SavingChanges(eventData, result);
    }


    static string CreateAdded(EntityEntry entry)
       => entry.Properties.Aggregate(
           $"Intercepted on: Inserting {entry.Metadata.DisplayName()} with ",
           (auditString, property) => auditString + $"{property.Metadata.Name}: '{property.CurrentValue}' ");

    static string CreateModified(EntityEntry entry)
        => entry.Properties.Where(property => property.IsModified || property.Metadata.IsPrimaryKey()).Aggregate(
            $"Intercepted on: Updating {entry.Metadata.DisplayName()} with ",
            (auditString, property) => auditString + $"{property.Metadata.Name}: '{property.CurrentValue}' ");

    static string CreateDeleted(EntityEntry entry)
         => entry.Properties.Where(property => property.Metadata.IsPrimaryKey()).Aggregate(
             $"Intercepted on: Deleting {entry.Metadata.DisplayName()} with ",
             (auditString, property) => auditString + $"{property.Metadata.Name}: '{property.CurrentValue}' ");
  }
}
