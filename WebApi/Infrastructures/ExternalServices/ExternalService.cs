using System.Threading.Tasks;

namespace WebApi.Infrastructures.ExternalServices
{
    public class ExternalService : IExternalService
    {
        public async Task<string> GetAsync()
        {
            await Task.Delay(1000);
            throw new System.Exception("ExternalService Exception");
            return "api response";
        }
    }
    public interface IExternalService
    {
        Task<string> GetAsync();
    }
}
