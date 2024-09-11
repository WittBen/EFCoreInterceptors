namespace ConsoleApp1.Classes;
  public class Methods
  {
    public virtual void MethodA()
    {
      Console.WriteLine("method A and intercepted!!");
    }
    public void MethodB()
    {
      Console.WriteLine("method B, not virtual => no interception! :(");
    }
  }
