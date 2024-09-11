using Castle.DynamicProxy;

namespace ConsoleApp1.Interceptor;
public class LogginInterceptor : IInterceptor
{
  public void Intercept(IInvocation invocation)
  {
    Console.WriteLine($"Intercepted: {invocation.Method}");
    invocation.Proceed();
  }
}
