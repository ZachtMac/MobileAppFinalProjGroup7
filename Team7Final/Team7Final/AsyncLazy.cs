using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Team7Final
{
    public class AsyncLazy<T>
    {
        readonly Lazy<Task<T>> instance;
        public void Start()
        {
            var unused = instance.Value;
        }

        public AsyncLazy(Func<T> factory)
        {
            instance = new Lazy<Task<T>>(() => Task.Run(factory));
        }

        public AsyncLazy(Func<Task<T>> factory)
        {
            instance = new Lazy<Task<T>>(() => Task.Run(factory));
        }



        public TaskAwaiter<T> GetAwaiter()
        {
            return instance.Value.GetAwaiter();
        }
    }
}
