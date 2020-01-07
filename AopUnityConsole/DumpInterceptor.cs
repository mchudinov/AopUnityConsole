using System;
using System.Collections.Generic;
using Unity.Interception.InterceptionBehaviors;
using Unity.Interception.PolicyInjection.Pipeline;

namespace AopUnityConsole
{
    public class DumpInterceptor : IInterceptionBehavior
    {
        public bool WillExecute => true;

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            Log($"Unity before executing on {input.MethodBase.DeclaringType.Name}:{input.MethodBase.Name}");
            var result = getNext()(input, getNext);
            Log($"Unity after executing on {input.MethodBase.DeclaringType.Name}:{input.MethodBase.Name}");
            return result;
        }

        private void Log(string msg, object arg = null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg, arg);
            Console.ResetColor();
        }
    }
}
