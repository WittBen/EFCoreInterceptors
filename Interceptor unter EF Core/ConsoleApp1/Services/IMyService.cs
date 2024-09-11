using System.Xml.Linq;

namespace ConsoleApp1.Services;

public interface IMyService
{
  IEnumerable<Employee> GetAllData();
  Employee GetOneEmployeeId();

  void SaveEmployee();
  void SaveEmployeeWithTransaction();
}