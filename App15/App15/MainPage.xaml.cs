using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.ServiceModel;
using Xamarin.Forms;
using static App15.MainPage;

namespace App15
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            
        }
        ObservableCollection<Employee> employees = new ObservableCollection<Employee>();
        public class Employee
        {
            public string DisplayName { get; set; }
        }

        
         
        private void onClickedButtin(object sender, EventArgs e)
        {
            ServiceReference2.AMSIntegrationServiceClient proxy = new ServiceReference2.AMSIntegrationServiceClient(
                ServiceReference2.AMSIntegrationServiceClient.EndpointConfiguration.BasicHttpBinding_IAMSIntegrationService,
                "http://57.31.17.135/SITAAMSIntegrationService/v1/SITAAMSIntegrationService/");

            EmployeeView.ItemsSource = employees;
            employees.Add(new Employee { DisplayName = "Rob Finnerty" });
            employees.Add(new Employee { DisplayName = "Bill Wrestler" });
            employees.Add(new Employee { DisplayName = "Dr. Geri-Beth Hooper" });
            employees.Add(new Employee { DisplayName = "Dr. Keith Joyce-Purdy" });

            proxy.GetFlightsCompleted += Proxy_GetFlightsCompleted;
            proxy.GetFlightsAsync(
                DateTime.Parse("2017-02-06"), 
                DateTime.Parse("2017-02-07"), 
                "TSE", 
                ServiceReference2.AirportIdentifierType.IATACode);
        }

        private void Proxy_GetFlightsCompleted(object sender, ServiceReference2.GetFlightsCompletedEventArgs e)
        {

            XElement resss = e.Result;
            EmployeeView.ItemsSource = resss.Descendants("Flights").First().Elements();
            employees.Add(new Employee { DisplayName = "GetFlights" });

        }
    }
}
