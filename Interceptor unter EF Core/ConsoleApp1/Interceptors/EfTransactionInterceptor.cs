using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;

public class EfTransactionInterceptor : DbTransactionInterceptor
{
  public override InterceptionResult TransactionCommitting(DbTransaction transaction, TransactionEventData eventData, InterceptionResult result)
  {
    Console.WriteLine($"Committing transaction: {transaction.Connection.DataSource}");
    return base.TransactionCommitting(transaction, eventData, result);
  }


  public override void TransactionFailed(DbTransaction transaction, TransactionErrorEventData eventData)
  {
    Console.WriteLine($"Trasnaction failed with this error: {eventData.Exception.Message}");
    base.TransactionFailed(transaction, eventData);
  }

  public override void TransactionRolledBack(DbTransaction transaction, TransactionEndEventData eventData)
  {
    Console.WriteLine($"Rolling back transaction");
    base.TransactionRolledBack(transaction, eventData);
  }

}
