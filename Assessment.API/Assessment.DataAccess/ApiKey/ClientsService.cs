using Assessment.DataAccess.ApiKey.IApiKey.IApiKey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.DataAccess.ApiKey
{
    public class InMemoryClientsService : IApiKey.IApiKey.IClientsService
    {
        private static readonly Dictionary<string, Guid> _clients = new()
        {
            {"Assessment-d2YZ9wBc7aMXV6knUtiVSaU51Tf50dQP10iGGz0krY9v1srUfW-8ZgQxr6-H7lClyCXS4wVpZAVho3_HiaqNiGCP1qHP4YK",Guid.NewGuid()}
        };
        public Task<Dictionary<string, Guid>> GetActiveClients()
        {
            return Task.FromResult(_clients);
        }
        public Task InvalidateApiKey(string apiKey)
        {
            _ = _clients.Remove(apiKey);

            return Task.CompletedTask;
        }
    }
}
