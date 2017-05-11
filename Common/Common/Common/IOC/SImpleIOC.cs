using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCommon.Common.IOC
{
    public class SImpleIOC
    {
        private static readonly SImpleIOC _Default;

        public static SImpleIOC Default
        {
            get
            {
                return _Default;
            }
        }


        public delegate object CreateInstanceDelegate(SImpleIOC simpleIOC);

        private readonly Dictionary<string, CreateInstanceDelegate> KeyCreator
                   = new Dictionary<string, CreateInstanceDelegate>();

        private readonly Dictionary<string, Object> KeyInstance
               = new Dictionary<string, Object>();


        static SImpleIOC()
        {
            _Default = new SImpleIOC();
        }


        private SImpleIOC()
        {

        }

        public SImpleIOC CreateInstance()
        {
            return new SImpleIOC();
        }

        public void Register<T>(CreateInstanceDelegate creator)
        {
            KeyCreator.Add(typeof(T).FullName,creator);
        }

        public  T Create<T>(bool isSingleton=true)
        {
            if(isSingleton==true)
            {
                return Create<T>(typeof(T).FullName);
            }
            else
            {
                return (T)KeyCreator[typeof(T).FullName](this);
            }
        }

        public T Create<T>(string key)
        {
            lock(KeyInstance)
            {
                if(!KeyInstance.ContainsKey(key))
                {
                    KeyInstance.Add(key, KeyCreator[typeof(T).FullName](this));
                   
                }

                return (T)KeyInstance[key];
            }
        }

    }
}
