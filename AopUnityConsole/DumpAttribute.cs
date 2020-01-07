using System;
using Unity;
using Unity.Interception.PolicyInjection.Pipeline;
using Unity.Interception.PolicyInjection.Policies;

namespace AopUnityConsole
{
    class DumpAttribute : HandlerAttribute
    {
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new DumpCallHandler();
        }
    }

    public class DumpCallHandler : ICallHandler
    {
        public int Order { get; set; }

        public DumpCallHandler() : this(0) { }

        public DumpCallHandler(int order)
        {
            Order = order;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            Log($"Unity before executing on {input.MethodBase.DeclaringType.Name}:{input.MethodBase.Name}");
            Log($"Argumenter:");
            foreach (var arg in input.Arguments)
            {
                Console.WriteLine(arg);
            }

            var result = getNext().Invoke(input, getNext);
            Log($"Unity after executing on {input.MethodBase.DeclaringType.Name}:{input.MethodBase.Name}");
            return result;
        }

        private void Log(string msg, object arg = null)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(msg, arg);
            Console.ResetColor();
        }        
    } 
}
