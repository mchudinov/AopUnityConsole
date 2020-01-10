# Aspect Oriented Programming with Unity container
Aspect Oriented Programming (AOP) with Unity container

AOP concepts can be implemented with Unity container via dynamic proxy and C# method attributes.
The AOP usage looks like this:

```C#
//this is just an example
interface MyInterface
{
    [MyAspect]
    void MyMethod();    
}
```
Every time an implemenation of MyMethod() is called the attribute MyAspect is executed.

The following steps need to be implemented.
#### 1. Create class attribute
Attribute must be inherited from `HandlerAttribute` class and a call class that implements `ICallHandler` interface.
Method `IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)` is where the attribute is executed.

```C#
class DumpAttribute : HandlerAttribute
{
    public override ICallHandler CreateHandler(IUnityContainer container)
    {
        return new DumpCallHandler();
    }
}

class DumpCallHandler : ICallHandler
{
    public int Order { get; set; }

    public DumpCallHandler() : this(0) { }

    public DumpCallHandler(int order)
    {
        Order = order;
    }

    public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
    {        
        var result = getNext().Invoke(input, getNext);        
        return result;
    }
} 
```

#### 2. Enable TransparentProxyInterceptor
Enable `TransparentProxyInterceptor` in Unity configuration.

```C#
container.RegisterType<IRepository<Customer>, Repository<Customer>>();
container.Configure<Interception>().SetInterceptorFor<IRepository<Customer>>(new TransparentProxyInterceptor());
```

#### 2. Define target interface
Define a target interface that will be augumented with an aspect and use attribute on methods.

```C#
public interface IRepository<T>
{
    [Dump]
    void Add(T entity);    
}
```

