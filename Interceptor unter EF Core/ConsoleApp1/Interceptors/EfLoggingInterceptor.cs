using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data;
using System.Data.Common;



public class EfLoggingInterceptor : DbCommandInterceptor
{
  public override InterceptionResult<int> NonQueryExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<int> result)
    => base.NonQueryExecuting(command, eventData, result);
  public override int NonQueryExecuted(DbCommand command, CommandExecutedEventData eventData, int result)
  => base.NonQueryExecuted(command, eventData, result);


  public override InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result)
    => base.ReaderExecuting(command, eventData, result);

  public override DbDataReader ReaderExecuted(DbCommand command, CommandExecutedEventData eventData, DbDataReader result)
  {
    if (eventData.Context == null)
    {
      return base.ReaderExecuted(command, eventData, result);
    }

    /// In this example, we intercept the result that the user expects from the database and look at what the user has queried.
    /// Here we have to make sure that we do not materialize the result object, otherwise it will be unusable for the user and trigger an error.
    /// therefore we create a copy of the result and load it into a DataTable,
    /// then read it out.
    using var dt = new DataTable();
    dt.Load(result);

    if (eventData.CommandSource == CommandSource.LinqQuery)
    {
      foreach (var row in dt.Rows.Cast<DataRow>())
      {
        string Id = row["Id"].ToString();
        string FirstName = row["FirstName"].ToString();
        string LastName = row["LastName"].ToString();

        LogInfo("EFCommandInterceptor.ReaderExecuted", $"{ Id} >> { FirstName}, { LastName}", command.CommandText);

      }
    }
    //since the user is still waiting for his result, we create a new reader here and then return it
    return base.ReaderExecuted(command, eventData, dt.CreateDataReader());
  }

  public override object? ScalarExecuted(DbCommand command, CommandExecutedEventData eventData, object? result)
  {
    LogInfo("EFCommandInterceptor.ScalarExecuted", eventData.ToString(), command.CommandText);
    return base.ScalarExecuted(command, eventData, result);
  }

  public override InterceptionResult<object> ScalarExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<object> result)
  {
    LogInfo("EFCommandInterceptor.ScalarExecuting", eventData.ToString(), command.CommandText);
    return base.ScalarExecuting(command, eventData, result);
  }

  public override void CommandFailed(DbCommand command, CommandErrorEventData eventData)
  {
    base.CommandFailed(command, eventData);
    LogInfo("EFCommandInterceptor.CommandFailed", eventData.ToString(), command.CommandText);
  }

  private void LogInfo(string method, string command, string commandText)
  {
    Console.WriteLine("Intercepted on: {0} \n {1} \n {2}", method, command, commandText);
  }
}