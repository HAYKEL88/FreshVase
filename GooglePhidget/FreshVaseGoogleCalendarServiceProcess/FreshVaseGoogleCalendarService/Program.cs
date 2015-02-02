using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

using Google;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;


namespace FreshVaseGoogleCalendarService
{



    public class GoogleCalendarServiceBean
    {



        public static Boolean state = false;
        public MyGoogleCalendarServiceThread myThreadClass = new MyGoogleCalendarServiceThread();


        public Boolean SystemState
        {
            get { return state; }
            set
            {
                state = value;
                FireBoolEvent(state);		// event will be fired for every property set.
            }
        }



        [STAThread]
        public void GetSystemState()
        {



            while (true)
            {

                myThreadClass.getGoogleCalendarData();
                state = myThreadClass.getState();
                FireBoolEvent(state);
             
                // Wait 30 seconds
                Thread.Sleep(30000);



            }


        }



        public delegate void BoolValueEventHandler(Boolean state);


        public event BoolValueEventHandler StateChanged;

        private void FireBoolEvent(Boolean state)
        {
            if (StateChanged != null)
                StateChanged(state);
        }









    }


    /////////////////////////////////////////////////////////////////////////






    public class MyGoogleCalendarServiceThread
    {



        public static Boolean state = false;

        public Boolean getState()
        {
            return state;
        }



        public void getGoogleCalendarData()
        {



            String tmp = "";

            try
            {
                new MyGoogleCalendarServiceThread().Run().Wait();
            }
            catch (AggregateException ex)
            {
                tmp = ex.Message;
            }
        }

        private async Task Run()
        {

            UserCredential credential;
            // using (var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
            // {
            credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                //  GoogleClientSecrets.Load(stream).Secrets,
                 new ClientSecrets
                 {
                     ClientId = "226690535450-b68dgduoeb41ign4ma5tjrdmvu3t7nfb.apps.googleusercontent.com",
                     ClientSecret = "X_2FT9cIk7-vh43wzr-SaVTe",
                 },
                new[] { CalendarService.Scope.Calendar },
                "FreshVase@gmail.com", CancellationToken.None);
            // }

            // Create the service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "FreshVase",
            });

            foreach (Event eventItem in service.Events.List("FreshVase@gmail.com").Execute().Items)
            {





                if ((eventItem.Summary.Equals("Retour à la maison")) && (eventItem.Start.DateTime.Value.Date == DateTime.Now.Date)
                    && ((Convert.ToInt32((eventItem.Start.DateTime.Value.TimeOfDay - DateTime.Now.TimeOfDay).TotalMinutes)) <= 30)
                    && ((Convert.ToInt32((eventItem.Start.DateTime.Value.TimeOfDay - DateTime.Now.TimeOfDay).TotalMinutes)) >= 0))
                {
                    state = true;
                    if (!File.Exists("D:\\GooglePhidget\\state.true"))
                    {
                        File.Create("D:\\GooglePhidget\\state.true").Dispose();
                    }

                }
                else
                {
                    if (File.Exists("D:\\GooglePhidget\\state.true"))
                    {
                        File.Delete("D:\\GooglePhidget\\state.true");
                    }
                    state = false;
                }

            }
        }







        /////////////////////////////////////////////////////////////////////////////
    }







    public class Program
    {
        static void Main(string[] args)
        {
            GoogleCalendarServiceBean c = new GoogleCalendarServiceBean();
            c.GetSystemState();
        }
    }
}
