﻿/*
 * Created by SharpDevelop.
 * User: Haykel
 * Date: 01/02/2015
 * Time: 20:30
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using WComp.Beans;
using System.Net;
using System.IO;
using System.Text;

namespace WComp.Beans
{
	/// <summary>
	/// This is a sample bean, which has an integer evented property and a method.
	/// 
	/// Notes: for beans creating threads, the IThreadCreator interface should be implemented,
	/// 	providing a cleanup method should be implemented and named `Stop()'.
	/// For proxy beans, the IProxyBean interface should  be implemented,
	/// 	providing the IsConnected property, allowing the connection status to be drawn in
	/// 	the AddIn's graphical designer.
	/// 
	/// Several classes can be defined or used by a Bean, but only the class with the
	/// [Bean] attribute will be available in WComp. Its ports will be all public methods,
	/// events and properties definied in that class.
	/// </summary>
	[Bean(Category="FreshVaseProject")]
	public class FreshVaseWebServiceSMS
	{
		private int motorVelocity=0;
		private Boolean motorState=false;

		public int MyMotorVelocity {
			get { return motorVelocity; }
			set {
				motorVelocity = value;
				FireIntEvent(motorVelocity);		// event will be fired for every property set.
			}
		}
		
		
		public Boolean MyMotorState {
			get { return motorState; }
			set {
				motorState = value;
				FireBooleanEvent(motorState);		// event will be fired for every property set.
			}
		}



		public delegate void IntValueEventHandler(int val);
		public delegate void BooleanValueEventHandler(Boolean val);

		public event IntValueEventHandler MotorVelocityChanged;
		public event BooleanValueEventHandler MotorStateChanged;
		
		private void FireIntEvent(int i) {
			if (MotorVelocityChanged != null)
				MotorVelocityChanged(i);
		}
		
		private void FireBooleanEvent(Boolean i) {
			if (MotorStateChanged != null)
				MotorStateChanged(i);
		}
		
		
		
		
		
		
		
		///////////////////////////////////////////////////////////////////////////
		public void getSystemStateFromSMS()
        {
            try
            {
                Boolean state = false;
                int velocity = 0;
                DateTime startTime;


                //The method we will use to send the data, this can be POST or GET.
                string requestmethod = "POST";

                //Here we will enter the data to send, just like if we where to go to a webpage and enter variables,
                // we would type: "www.somesite.com?var1=Hello&var2=Server!"!
                string postData = "state=haykel";

                //The Byte Array that will be used for writing the data to the stream.
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                //The URL of the webpage to send the data to.
                string URL = "http://freshvase.hebergratuit.net/FreshVase/sms.php";

                //The type of content being send, this is almost always "application/x-www-form-urlencoded".
                string contenttype = "application/x-www-form-urlencoded";

                //What the server sends back:
                string responseFromServer = null;
                Console.WriteLine("gg");

                //Here we will create the WebRequest object, and enter the URL as soon as it is created.
                WebRequest request = WebRequest.Create(URL);

                //We also need a Stream:
                Stream dataStream;

                //...And a webResponce,
                WebResponse response;

                //don't forget the streamreader either!
                StreamReader reader;


                Console.WriteLine("gg");

                //We will need to set the method used to send the data.
                request.Method = requestmethod;

                //Then the contenttype:
                request.ContentType = contenttype;

                //content length
                request.ContentLength = byteArray.Length;

                //ok, now get the request from the webRequest object, and put it into our Stream:
                dataStream = request.GetRequestStream();

                // Write the data to the request stream.
                dataStream.Write(byteArray, 0, byteArray.Length);

                // Close the Stream object.
                dataStream.Close();


                //Get the responce
                response = request.GetResponse();

                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();

                //Open the responce stream:
                reader = new StreamReader(dataStream);

                //read the content into the responcefromserver string
                responseFromServer = reader.ReadToEnd();

                // Clean up the streams.
                reader.Close();
                dataStream.Close();
                response.Close();
                //Now, display the responce!
                //Console.WriteLine(responseFromServer);

                state = Boolean.Parse(responseFromServer.Substring(responseFromServer.IndexOf('=') + 1, responseFromServer.IndexOf(',') - responseFromServer.IndexOf('=') - 1));
                String temp = responseFromServer.Substring(responseFromServer.IndexOf(',') + 1);
                velocity = Int32.Parse(temp.Substring(responseFromServer.IndexOf('=') + 1, temp.IndexOf(',') - temp.IndexOf('=') - 1));
                temp = responseFromServer.Substring(responseFromServer.IndexOf(',') + 1);
                temp = temp.Substring(temp.LastIndexOf('=') + 1);
                startTime = Convert.ToDateTime(temp);

                Console.WriteLine(startTime.ToString());
                if ((state == true) && (Math.Abs((startTime - DateTime.Now).TotalMinutes) < 3))
                {
					motorState=state;
					motorVelocity=velocity;
                	FireBooleanEvent(motorState);     	
                	FireIntEvent(motorVelocity);

                }

                if ((state == false) && (Math.Abs((startTime - DateTime.Now).TotalMinutes) < 3))
                {
                	motorState=state;
				FireBooleanEvent(motorState);
                }
            }
            catch (Exception ex)
            {
                String nothing = ex.Message;
            }


        }
		
				///////////////////////////////////////////////////////////////////////////
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
	}
}
