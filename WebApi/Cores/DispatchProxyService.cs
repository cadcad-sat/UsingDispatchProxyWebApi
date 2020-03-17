using System;
using System.Reflection;
using System.Threading.Tasks;

namespace WebApi.Cores
{
    public class DispatchProxyService<T> : DispatchProxy
    {
        private T decorated;

        public static T Create(T decorated)
        {
            object proxy = Create<T, DispatchProxyService<T>>();
            ((DispatchProxyService<T>)proxy).SetParameters(decorated);

            return (T)proxy;
        }
        private void SetParameters(T decorated)
        {
            if (decorated == null)
                throw new ArgumentNullException(nameof(decorated));

            this.decorated = decorated;
        }

        protected override object Invoke(MethodInfo method, object[] args)
        {
            int counter = 0;
            var result = Repeater(counter, method, args);
            return result;
        }

        private object Repeater(int counter, MethodInfo method, object[] args)
        {
            try
            {
                var result = method.Invoke(decorated, args);
                if (result is Task resultTask)
                    resultTask.Wait();
                return result;
            }
            catch (AggregateException)
            {
                counter++;
                if (counter < 3)
                    return Repeater(counter, method, args);
                else
                    throw;
            }
        }
    }
}
