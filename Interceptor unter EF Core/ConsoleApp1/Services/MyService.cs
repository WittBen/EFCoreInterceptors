using ConsoleApp1.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.ComponentModel;


namespace ConsoleApp1.Services;

public class MyService : IMyService
{
  private readonly DBContext _context;

  public MyService()
  {
    if(_context == null)
      _context = Create.Context();
  }


  #region List
  public IEnumerable<Employee> GetAllData()
  {
    var listOfData = _context.Data.ToList();
    return listOfData;
  }

  public Employee GetOneEmployeeId()
  {
    return _context.Data.First(x => x.LastName.StartsWith("B") || x.LastName.StartsWith("T"));
  }

  public void SaveEmployee()
  {

    var mit = new Employee();

    mit.FirstName = "test";
    mit.LastName = "test2";
    _context.Data.Add(mit);

    _context.SaveChanges();
  }


  public void SaveEmployeeWithTransaction()
  {

    var mit = new Employee();

    _context.Database.BeginTransaction();

    mit.FirstName = "test11";
    mit.LastName = "test22";

    _context.Data.Add(mit);
    _context.SaveChanges();
    _context.Database.CommitTransaction();
  }
  #endregion

}