using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App15.Data
{
    public class SoapService : ISoapService
    {
        ServiceReference2.AMSIntegrationServiceClient todoService;

        public List<flight> Items { get; private set; }

        public SoapService()
        {
            todoService = new ServiceReference2.AMSIntegrationServiceClient(
                ServiceReference2.AMSIntegrationServiceClient.EndpointConfiguration.BasicHttpBinding_IAMSIntegrationService,
                "DD");
        }

        public async Task<List<flight>> RefreshDataAsync(DateTime from,DateTime to, string airport)
        {
            Items = new List<flight>();

           

            return Items;
        }
    }
}
