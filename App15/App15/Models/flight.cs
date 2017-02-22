using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App15
{
    public class flight
    {
        private string flightId;
        private string flightIdentifier;
        private string flightNumber;
        private string ArrDep;
        private DateTime flightSTO;
        private DateTime flightSDO;
        private DateTime flightATO; //Departure_ESTIMATED TIME OF DEPARTURE_Time
        private DateTime flightADO;
        private DateTime flightSTA; //Sheduled time of arrival
        private DateTime flightOnBlockTA; //ONBLOCK time of arrival
        private string qual;
        private string cIata;
        private string cIcao;
        private string registration;
        private string acIata;
        private string acIcao;
        private bool depUpdateFFM;
        private bool depUpdateFSULITE;
        private Boolean Departure_FFM_NTS;
        private string typeBADDRESS;
        private int countMail;
        private int weightMail;
        private int countCargo;
        private int weightCargo;
        private string email;
        private string emailCargo;
        private char typeTimeIndicator;
        private char dayChangeIndicator;
        private string airport;
        private string destination;
        private int dayOfOperation;
        private string depTime;
        private string arrTime;
        public string FlightId
        {
            get
            {
                return flightId;
            }

            set
            {
                flightId = value;
            }
        }

        public string FlightIdentifier
        {
            get
            {
                return flightIdentifier;
            }

            set
            {
                flightIdentifier = value;

                StringBuilder sb = new StringBuilder();
                foreach (char ch in flightIdentifier.Substring(2, flightIdentifier.Length - 2).ToCharArray())
                {
                    if (char.IsDigit(ch))
                        sb.Append(ch);
                }
                FlightNumber = sb.ToString();
            }
        }

        public string ArrDep1
        {
            get
            {
                return ArrDep;
            }

            set
            {
                ArrDep = value;
            }
        }

        public DateTime FlightSTO
        {
            get
            {
                return flightSTO;
            }

            set
            {
                flightSTO = value;
            }
        }

        public DateTime FlightSDO
        {
            get
            {
                return flightSDO;
            }

            set
            {
                flightSDO = value;
                dayOfOperation = Convert.ToInt32(FlightSDO.ToString("dd"));
            }
        }

        public DateTime FlightATO
        {
            get
            {
                return flightATO;
            }

            set
            {
                flightATO = value;
                if (FlightSTO < FlightATO)
                {
                    TypeTimeIndicator = 'E';
                }
                else if (FlightSTO == FlightATO)
                {
                    TypeTimeIndicator = 'S';
                }
                //DepTime = FlightATO.Hour + "" + FlightATO.Minute;
                DepTime = FlightATO.ToString("HHmm");
            }
        }

        public DateTime FlightADO
        {
            get
            {
                return flightADO;
            }

            set
            {
                flightADO = value;
            }
        }

        public string Qual
        {
            get
            {
                return qual;
            }

            set
            {
                qual = value;
            }
        }

        public string CIata
        {
            get
            {
                return cIata;
            }

            set
            {
                cIata = value;
            }
        }

        public string CIcao
        {
            get
            {
                return cIcao;
            }

            set
            {
                cIcao = value;
            }
        }

        public string Registration
        {
            get
            {
                return registration;
            }

            set
            {
                if (value.IndexOf("-") > 0) registration = value.Remove(value.IndexOf("-"), 1); else registration = value;
            }
        }

        public string AcIata
        {
            get
            {
                return acIata;
            }

            set
            {
                acIata = value;
            }
        }

        public string AcIcao
        {
            get
            {
                return acIcao;
            }

            set
            {
                acIcao = value;
            }
        }

        public bool Departure_FFM_NTS1
        {
            get
            {
                return Departure_FFM_NTS;
            }

            set
            {
                Departure_FFM_NTS = value;
            }
        }

        public bool DepUpdateFFM
        {
            get
            {
                return depUpdateFFM;
            }

            set
            {
                depUpdateFFM = value;
            }
        }

        public string TypeBADDRESS
        {
            get
            {
                return typeBADDRESS;
            }

            set
            {
                typeBADDRESS = value;
            }
        }

        public string Airport
        {
            get
            {
                return airport;
            }

            set
            {
                airport = value;
            }
        }

        public int DayOfOperation
        {
            get
            {
                return dayOfOperation;
            }

            set
            {
                dayOfOperation = value;
            }
        }

        public string Destination
        {
            get
            {
                return destination;
            }

            set
            {
                destination = value;
            }
        }


        public string Email
        {
            get
            {
                return email;
            }

            set
            {
                email = value;
            }
        }

        public bool DepUpdateFSULITE
        {
            get
            {
                return depUpdateFSULITE;
            }

            set
            {
                depUpdateFSULITE = value;
            }
        }

        public int CountMail
        {
            get
            {
                return countMail;
            }

            set
            {
                countMail = value;
            }
        }

        public int WeightMail
        {
            get
            {
                return weightMail;
            }

            set
            {
                weightMail = value;
            }
        }

        public int CountCargo
        {
            get
            {
                return countCargo;
            }

            set
            {
                countCargo = value;
            }
        }

        public int WeightCargo
        {
            get
            {
                return weightCargo;
            }

            set
            {
                weightCargo = value;
            }
        }

        public string FlightNumber
        {
            get
            {
                return flightNumber;
            }

            set
            {
                flightNumber = value;
            }
        }

        public string DepTime
        {
            get
            {
                return depTime;
            }

            set
            {
                depTime = value;
            }
        }

        public string ArrTime
        {
            get
            {
                return arrTime;
            }

            set
            {
                arrTime = value;
                if ((Convert.ToInt16(DepTime) > Convert.ToInt16(arrTime)))
                {
                    DayChangeIndicator = 'N';
                }
            }
        }

        public char TypeTimeIndicator
        {
            get
            {
                return typeTimeIndicator;
            }

            set
            {
                typeTimeIndicator = value;
            }
        }

        public char DayChangeIndicator
        {
            get
            {
                return dayChangeIndicator;
            }

            set
            {
                dayChangeIndicator = value;
            }
        }

        public string EmailCargo
        {
            get
            {
                return emailCargo;
            }

            set
            {
                emailCargo = value;
            }
        }

        public DateTime FlightSTA
        {
            get
            {
                return flightSTA;
            }

            set
            {
                flightSTA = value;
            }
        }

        public DateTime FlightOnBlockTA
        {
            get
            {
                return flightOnBlockTA;
            }

            set
            {
                flightOnBlockTA = value;
            }
        }

        public override string ToString()
        {
            return FlightNumber + " reg: " + Registration + " Board Airport: " + Registration + " flightID: " + Convert.ToString(FlightSTA) +
                 " TypeB Address: " + TypeBADDRESS + " qual: " + Qual + " flightSTO: " + FlightSTO;
        }
    }

}

