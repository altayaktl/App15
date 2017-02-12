using App15.ServiceReference2;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Runtime.Serialization;
using Xamarin.Forms;

namespace App15
{
    public partial class DetailFlights : ContentPage
    {
        DetailFlights page;
        public DetailFlights()
        {
            InitializeComponent();


        }
        private void OnSaveActivated(object sender, EventArgs e)
        {
            var todoItem = (flight)BindingContext;

            ServiceReference2.AMSIntegrationServiceClient proxy = new ServiceReference2.AMSIntegrationServiceClient(
                ServiceReference2.AMSIntegrationServiceClient.EndpointConfiguration.BasicHttpBinding_IAMSIntegrationService,
                "http://57.31.17.135/SITAAMSIntegrationService/v1/SITAAMSIntegrationService/");

            FlightId idFl = new FlightId();
            idFl.flightNumberField = todoItem.FlightNumber;
            idFl.scheduledDateField = todoItem.FlightSTA;
            idFl.flightKindField = FlightKind.Arrival;

            LookupCode asq = new LookupCode();
            asq.codeContextField = CodeContext.IATA;
            asq.valueField = todoItem.CIata;


            ObservableCollection<LookupCode> airdEs = new ObservableCollection<LookupCode>();
            airdEs.Add(asq);
            idFl.airlineDesignatorField = airdEs;

            LookupCode qsa = new LookupCode();
            qsa.codeContextField = CodeContext.IATA;
            qsa.valueField = "TSE";

            ObservableCollection<LookupCode> airportLo = new ObservableCollection<LookupCode>();
            airportLo.Add(qsa);
            idFl.airportCodeField = airportLo;

            PropertyValue val1 = new PropertyValue();
            val1.valueField = nameEntry.Text;
            val1.propertyNameField = "SOP Mail Arrival";

            ObservableCollection<PropertyValue> values = new ObservableCollection<PropertyValue>();
            values.Add(val1);

            proxy.UpdateFlightCompleted += Proxy_UpdateFlightCompleted;
            proxy.UpdateFlightAsync(idFl,values);
            
        }

        private  void Proxy_UpdateFlightCompleted(object sender, ServiceReference2.UpdateFlightCompletedEventArgs e)
        {
            XElement root = e.Result;
            DisplayAlert("Updated", "Success updated.", "OK");
            if (root.Value == "Success")
            {
                //Title = "Saved";
                //ttSave.Text = "Saved";
                //
            }
            
        }
        private void OnCancelActivated(object sender, EventArgs e)
        {
           // DisplayAlert("Cancelled", "Success f.", "OK");
            Navigation.PopAsync();
        }

        }
}
