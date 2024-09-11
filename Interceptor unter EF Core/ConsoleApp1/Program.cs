var serviceProvider = new ServiceCollection().AddTransient<IMyService, MyService>().BuildServiceProvider();

var service = serviceProvider.GetService<IMyService>()!;

Console.WriteLine("*********************  Data  **********************");


foreach (var data in service.GetAllData())
{
  Console.WriteLine($"ID: {data.Id} | FirstName:{data.FirstName} | LastName:{data.LastName} | Deparment:{data.Department} | EntryYear:{data.EntryYear}");
}
Console.WriteLine("*********************++++++++**********************");

Console.WriteLine("*********************  one record  **********************");
Console.WriteLine(service.GetOneEmployeeId().Fullname);
Console.WriteLine("*********************++++++++**********************");

Console.WriteLine("*********************  save data **********************");
service.SaveEmployee();

Console.WriteLine("*********************++++++++**********************");

foreach (var data in service.GetAllData())
{
  Console.WriteLine($"ID: {data.Id} | FirstName:{data.FirstName} | LastName:{data.LastName} | Deparment:{data.Department} | EntryYear:{data.EntryYear}");
}

Console.WriteLine("*********************++++++++**********************");

foreach (var data in service.GetAllData())
{
  Console.WriteLine($"ID: {data.Id} | FirstName:{data.FirstName} | LastName:{data.LastName} | Deparment:{data.Department} | EntryYear:{data.EntryYear}");
}




Console.WriteLine("*********************  save data with transaction **********************");


service.SaveEmployeeWithTransaction();


Console.WriteLine("*********************++++++++**********************");
Console.ReadKey();



