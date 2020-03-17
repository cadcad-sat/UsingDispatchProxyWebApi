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
            var result = Repeater(counter, method, args).Result;
            return result;
        }

        private async Task<object> Repeater(int counter, MethodInfo method, object[] args)
        {
            try
            {
                var result = method.Invoke(decorated, args);
                if (result is Task resultTask)
                    await resultTask;
                return result;
            }
            catch (ArgumentException)
            {
                counter++;
                if (counter < 3)
                    return await Repeater(counter, method, args);
                else
                    throw;
            }
        }
    }
}
