using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Threading.Tasks;

namespace Common.Common.Proxy
{
    /// <summary>
    /// Proxy는 Interface의 메소드뿐 아니라 속성까지 Proxy한다.
    /// 속성 get, set, 메소드 호출 전 후에 작업할 것을 지정할 수 있다.
    /// 예를 들어 앞뒤로 로그해야 할때..
    /// </summary>
    /// <typeparam name="T"> Interface여야한다.</typeparam>
    public class ObjectProxy<T> : RealProxy
    {
        private T _instance;

        public ObjectProxy()
           : base(typeof(T))
        {

        }

        private ObjectProxy(T instance)
            : base(typeof(T))
        {
            _instance = instance;
        }

        public static T Create(T instance)
        {
            return (T)new ObjectProxy<T>(instance).GetTransparentProxy();
        }

        public void Change(T instance)
        {
            _instance = instance;

        }

        public override IMessage Invoke(IMessage msg)
        {
            var methodCall = (IMethodCallMessage)msg;
            var method = (MethodInfo)methodCall.MethodBase;

            try
            {
                // interface Method 혹은 Propery 실행 전
                Debug.WriteLine("Before invoke: " + method.Name);

                var result = method.Invoke(_instance, methodCall.InArgs);

                // interface Method 혹은 Propery 실행 후
                Debug.WriteLine("After invoke: " + method.Name);
                return new ReturnMessage(result, null, 0, methodCall.LogicalCallContext, methodCall);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e);
                if (e is TargetInvocationException && e.InnerException != null)
                {
                    return new ReturnMessage(e.InnerException, msg as IMethodCallMessage);
                }

                return new ReturnMessage(e, msg as IMethodCallMessage);
            }
        }
    }

}
