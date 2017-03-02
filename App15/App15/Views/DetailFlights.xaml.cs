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
using System.ComponentModel;

namespace App15
{
    public partial class DetailFlights : ContentPage
    {
        DetailFlights page;
        bool alive = true;
        public DetailFlights()
        {
            InitializeComponent();


        }
        private void OnSaveActivated(object sender, EventArgs e)
        {
            //flightSaktaubyValues("SOP Mail Arrival", nameEntry.Text);

        }

        private void flightSaktaubyValues(string propertyNameFieldtxt, string valueFieldtxt)
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
            val1.valueField = valueFieldtxt;
            val1.propertyNameField = propertyNameFieldtxt;

            ObservableCollection<PropertyValue> values = new ObservableCollection<PropertyValue>();
            values.Add(val1);

            proxy.UpdateFlightCompleted += Proxy_UpdateFlightCompleted;
            proxy.UpdateFlightAsync(idFl, values);
        }

        private  void Proxy_UpdateFlightCompleted(object sender, ServiceReference2.UpdateFlightCompletedEventArgs e)
        {
            XElement root = e.Result;
            //DisplayAlert("Updated", "Success updated.", "OK");
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

        private void OnTrapClicked(object sender, EventArgs e)
        {
            if (alive == true)
            {
                alive = false;
                trapStrStpBtn.Text = "PLAY";
            }
            else
            {
                alive = true;
                trapStrStpBtn.Text = "STOP";
                //trapStrStpBtn.TextColor = "Red";
                flightSaktaubyValues("FIL_TRAP", "1");
                Device.StartTimer(TimeSpan.FromSeconds(1), OnTimerTick);

            }
        }

        private void OnLoadingBrigdeInClicked(object sender, EventArgs e)
        {
            if (alive == true)
            {
                alive = false;
                looadBIStrStpBtn.Text = "PLAY";
            }
            else
            {
                alive = true;
                looadBIStrStpBtn.Text = "STOP";
                //trapStrStpBtn.TextColor = "Red";
                flightSaktaubyValues("FIL_LOADING_BRIDGE_IN", "1");
                Device.StartTimer(TimeSpan.FromSeconds(1), OnLoadBITick);

            }
        }
        private void OnBusClicked(object sender, EventArgs e)
        {
            if (alive == true)
            {
                alive = false;
                busStrStpBtn.Text = "PLAY";
            }
            else
            {
                alive = true;
                busStrStpBtn.Text = "STOP";
                //trapStrStpBtn.TextColor = "Red";
                flightSaktaubyValues("FIL_BUS", "1");
                Device.StartTimer(TimeSpan.FromSeconds(1), OnBusTick);

            }
        }

        private bool OnLoadBITick()
        {
            loadBITimerBtn.Text = DateTime.Now.ToString("T");
            return alive;
        }

        private bool OnBusTick()
        {
            BusTimerBtn.Text = DateTime.Now.ToString("T");
            return alive;
        }

        private bool OnTimerTick()
        {
            trapTimerBtn.Text = DateTime.Now.ToString("T");
            return alive;
        }

        private void OnBlockClicked(object sender, EventArgs e)
        {
            DateTime kazyr;
            if (alive == true)
            {
                alive = false;
                onBlockStrStpBtn.Text = "PLAY";
            }
            else
            {
                alive = true;
                onBlockStrStpBtn.Text = "STOP";
                //trapStrStpBtn.TextColor = "Red";
                kazyr = DateTime.Now;
                onBlockTextDate.Text = kazyr.ToString("T");
                flightSaktaubyValues("ON CHOCKS", kazyr.ToString("T"));
                flightSaktaubyValues("FIL_KOLODKI", "1");
                //Device.StartTimer(TimeSpan.FromSeconds(1), OnBlockTick);
            }
        }
        private void OnFollowMeClicked(object sender, EventArgs e)
        {
            onFollowMeBtn.BackgroundColor = Color.Red;
            flightSaktaubyValues("FIL_FUEL_TRACK", "1");
        }
        private void OnPWATERClicked(object sender, EventArgs e)
        {
            onPWATERBtn.BackgroundColor = Color.Red;
            flightSaktaubyValues("FIL_PWATER", "1");
        }
        private void OnFuelTruckClicked(object sender, EventArgs e)
        {
            onFuelTruckBtn.BackgroundColor = Color.Red;
            flightSaktaubyValues("FIL_FUEL_TRACK", "1");
        }

        private void OnHighLoaderClicked(object sender, EventArgs e)
        {
            onHighLoaderBtn.BackgroundColor = Color.Red;
            flightSaktaubyValues("FIL_HIGH_LOADER", "1");
        }
        private void OnLServiceClicked(object sender, EventArgs e)
        {
            onLServiceBtn.BackgroundColor = Color.Red;
            flightSaktaubyValues("FIL_LSERVICE", "1");
        }
        private void OnGPUClicked(object sender, EventArgs e)
        {
            onGPUBtn.BackgroundColor = Color.Red;
            flightSaktaubyValues("FIL_GPU", "1");
        }
        private void OnCateringClicked(object sender, EventArgs e)
        {
            onCateringBtn.BackgroundColor = Color.Red;
            flightSaktaubyValues("FIL_CATERING", "1");
        }

        private bool OnBlockTick()
        {
            onBlockTextDate.Text = DateTime.Now.ToString("T");
            return alive;
        }

        private void OnCleaningClicked(object sender, EventArgs e)
        {
            DateTime kazyr;
            if (alive == true)
            {
                alive = false;
                onCleaningStrStpBtn.Text = "PLAY";
            }
            else
            {
                alive = true;
                onCleaningStrStpBtn.Text = "STOP";
                //trapStrStpBtn.TextColor = "Red";
                kazyr = DateTime.Now;
                onCleaningTextDate.Text = kazyr.ToString("T");
                
                flightSaktaubyValues("FIL_CLEANING", "1");
                //Device.StartTimer(TimeSpan.FromSeconds(1), OnBlockTick);
            }
        }

        
    }
}
