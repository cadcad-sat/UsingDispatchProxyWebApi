using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WebApi.Infrastructures.ExternalServices;

namespace WebApi.Domains
{
    public class SampleEvent : ISampleEvent
    {
        private readonly IExternalService service;
        public SampleEvent(ILogger<SampleEvent> logger, IExternalService service)
        {
            //this.service = DispatchProxyService<IExternalService>.Create(service, logger);
            this.service = service;
        }

        public async Task<string> GetMessage()
        {
            var response = await service.GetAsync();
            return response;
        }
    }

    public interface ISampleEvent
    {
        Task<string> GetMessage();
    }
}
