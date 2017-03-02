using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.ServiceModel;
using Xamarin.Forms;

namespace App15.Views
{
    public partial class ArrivalFlightList : ContentPage
    {
        public ArrivalFlightList()
        {
            InitializeComponent();
            
        }

        ObservableCollection<flight> oflight = new ObservableCollection<flight>();
        

        private void onClickedButtin(object sender, EventArgs e)
        {
            ServiceReference2.AMSIntegrationServiceClient proxy = new ServiceReference2.AMSIntegrationServiceClient(
                ServiceReference2.AMSIntegrationServiceClient.EndpointConfiguration.BasicHttpBinding_IAMSIntegrationService,
                "http://57.31.17.135/SITAAMSIntegrationService/v1/SITAAMSIntegrationService/");

            FlightView.ItemsSource = oflight;
            proxy.GetFlightsCompleted += Proxy_GetFlightsCompleted;
            proxy.GetFlightsAsync(
                DateTime.Now.AddHours(-12)                , 
                DateTime.Now.AddHours(6), 
                "TSE", 
                ServiceReference2.AirportIdentifierType.IATACode);
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            //if (e.Item != null)
            //{
            //    //DisplayAlert("tapped", e.Item.ToString() + " was selected.", "OK");
            //    Navigation.PushAsync(new DetailFlights());
            //}
            if (e.Item != null)
            {
                //  DisplayAlert("Selected", e.SelectedItem.ToString() + " was selected.", "OK");

                var todoFlight = e.Item as flight;
                DetailFlights todoPage = new DetailFlights();
                todoPage.Title = todoFlight.CIata + todoFlight.FlightNumber + " " + todoFlight.FlightSTA.ToString("T");
                todoPage.BindingContext = todoFlight;
                Navigation.PushAsync(todoPage);
            }

        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            
        }

        private void Proxy_GetFlightsCompleted(object sender, ServiceReference2.GetFlightsCompletedEventArgs e)
        {
            XElement root = e.Result;
            //a = new ObservableCollection<Test>();
            bool belgiArrival = false;
            
            foreach (XElement rootElm in root.Elements())
            {
                if (rootElm.Name.LocalName.Contains("Data"))
                {
                    foreach (XElement dataElm in rootElm.Elements())
                    {
                        if (dataElm.Name.LocalName.Contains("Flights"))
                        {
                            // What do you want to add? The Attribute? Element value
                            foreach (XElement flightSSElm in dataElm.Elements())
                            {
                                if (flightSSElm.Name.LocalName.Contains("Flight"))
                                {
                                    flight selFlight = new flight();
                                    foreach (XElement flightElm in flightSSElm.Elements())
                                    {
                                        if (flightElm.Name.LocalName.Contains("FlightId"))
                                        {
                                            foreach (XElement flightIDelm in flightElm.Elements())
                                            {
                                                if (flightIDelm.Name.LocalName.Contains("FlightKind"))
                                                {
                                                    if (flightIDelm.Value == "Arrival")
                                                    {
                                                        belgiArrival = true;
                                                    }
                                                }
                                                else if (flightIDelm.Name.LocalName.Contains("FlightNumber") && belgiArrival)
                                                {
                                                    selFlight.FlightNumber = flightIDelm.Value.ToString();
                                                }
                                                else if (flightIDelm.Name.LocalName.Contains("AirlineDesignator") && flightIDelm.Attribute("codeContext").Value.Contains("IATA") && belgiArrival)
                                                {
                                                    selFlight.CIata = flightIDelm.Value.ToString();
                                                }
                                            }
                                        }
                                        else if (flightElm.Name.LocalName.Contains("FlightState") && belgiArrival)
                                        {
                                            foreach (XElement flighStateelement in flightElm.Elements())
                                            {
                                                if (flighStateelement.Name.LocalName.Contains("ScheduledTime"))
                                                {
                                                    //selFlight.FlightSTA = DateTime.Parse(flighStateelement.Value);
                                                    selFlight.FlightSTA = Convert.ToDateTime(flighStateelement.Value);
                                                   // selFlight.Email =  flighStateelement.Value.ToString();
                                                } else
                                                if (flighStateelement.Name.LocalName.Contains("Value") && flighStateelement.Attribute("propertyName").Value.Contains("ON CHOCKS"))
                                                {
                                                    //selFlight.FlightSTA = DateTime.Parse(flighStateelement.Value);
                                                    selFlight.FlightOnBlockTA = Convert.ToDateTime(flighStateelement.Value);
                                                    // selFlight.Email =  flighStateelement.Value.ToString();
                                                }
                                            }
                                            belgiArrival = false;
                                            oflight.Add(selFlight);
                                        }
                                    }
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
