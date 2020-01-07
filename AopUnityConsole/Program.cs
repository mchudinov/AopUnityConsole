using System;
using Unity;
using Unity.Interception;
using Unity.Interception.ContainerIntegration;
using Unity.Interception.Interceptors.InstanceInterceptors.InterfaceInterception;
using Unity.Interception.Interceptors.InstanceInterceptors.TransparentProxyInterception;
using Unity.Lifetime;

namespace AopUnityConsole
{
    class Program
    {
        // AOP with Unity
        //https://www.davidbreyer.com/programming/2015/07/01/aop-using-unity-interceptors-in-a-web-api-project/
        static void Main(string[] args)
        {
            using (var container = new UnityContainer().AddNewExtension<Interception>())
            {
                //container.RegisterType<IRepository<Customer>, Repository<Customer>>(new HierarchicalLifetimeManager(), new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<DumpInterceptor>());

                container.RegisterType<IRepository<Customer>, Repository<Customer>>(/*new ContainerControlledLifetimeManager()*/);
                container.Configure<Interception>().SetInterceptorFor<IRepository<Customer>>(new TransparentProxyInterceptor());

                var customerRepository = container.Resolve<IRepository<Customer>>();
                var customer = new Customer
                {
                    Id = 1,
                    Name = "Customer 1",
                    Address = "Address 1"
                };

                var temp = customerRepository.GetById(123);
                customerRepository.Add(customer);
                customerRepository.Update(customer);
                customerRepository.Delete(customer);
            }

            Console.WriteLine("\r\nEnd program\r\n***");
            Console.ReadLine();
        }
    }
}
