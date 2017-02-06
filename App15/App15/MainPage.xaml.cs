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
    public class Test
    {
        public string Name { get; set; }
        public string Age { get; set; }
    }
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        ObservableCollection<Employee> employees = new ObservableCollection<Employee>();
        ObservableCollection<Test> a = new ObservableCollection<Test>();
        public class Employee
        {
            public string DisplayName { get; set; }
        }

        
         
        private void onClickedButtin(object sender, EventArgs e)
        {
            ServiceReference2.AMSIntegrationServiceClient proxy = new ServiceReference2.AMSIntegrationServiceClient(
                ServiceReference2.AMSIntegrationServiceClient.EndpointConfiguration.BasicHttpBinding_IAMSIntegrationService,
                "http://57.31.17.135/SITAAMSIntegrationService/v1/SITAAMSIntegrationService/");

            //FlightView.ItemsSource = employees;
            //employees.Add(new Employee { DisplayName = "Rob Finnerty" });
            //employees.Add(new Employee { DisplayName = "Bill Wrestler" });
            //employees.Add(new Employee { DisplayName = "Dr. Geri-Beth Hooper" });
            //employees.Add(new Employee { DisplayName = "Dr. Keith Joyce-Purdy" });
            FlightView.ItemsSource = a;
            proxy.GetFlightsCompleted += Proxy_GetFlightsCompleted;
            proxy.GetFlightsAsync(
                DateTime.Parse("2017-02-06"), 
                DateTime.Parse("2017-02-07"), 
                "TSE", 
                ServiceReference2.AirportIdentifierType.IATACode);
        }

        private void Proxy_GetFlightsCompleted(object sender, ServiceReference2.GetFlightsCompletedEventArgs e)
        {

            XElement root = e.Result;
            //a = new ObservableCollection<Test>();
            foreach (XElement element in root.Elements())
            {
                if (element.Name.LocalName.Contains("Data"))
                {
                    foreach (XElement subelement in element.Elements())
                    {
                        if (subelement.Name.LocalName.Contains("Flights"))
                        {
                            // What do you want to add? The Attribute? Element value
                            foreach (XElement subsubelement in subelement.Elements())
                            {
                                if (subsubelement.Name.LocalName.Contains("Flight"))
                                {
                                    // What do you want to add? The Attribute? Element value

                                    a.Add(new Test() { Name = subsubelement.Value.ToString(), Age = "S" });
                                }
                            }

                        }
                    }
                }
            }
            //employees.Add(new Employee { DisplayName = "GetFlights" });

        }
    }
}
