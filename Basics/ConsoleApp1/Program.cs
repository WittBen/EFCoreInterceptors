using Castle.DynamicProxy;
using ConsoleApp1.Classes;
using ConsoleApp1.Interceptor;

var generator = new ProxyGenerator();
var proxy = generator.CreateClassProxy<Methods>(new LogginInterceptor());

// "Intercepting: MethodA"
proxy.MethodA();
// method B is not virtual -> no interception
proxy.MethodB();
