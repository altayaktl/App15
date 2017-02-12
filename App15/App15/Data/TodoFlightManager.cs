using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App15.Data
{
    public class TodoFlightManager
    {
        ServiceReference2.AMSIntegrationServiceClient soapService;

        public TodoFlightManager(ServiceReference2.AMSIntegrationServiceClient service)
        {
            soapService = service;
        }

        public Task<List<flight>> GetTodoFlightsAsync()
        {
            return null;
        }
    }
}
