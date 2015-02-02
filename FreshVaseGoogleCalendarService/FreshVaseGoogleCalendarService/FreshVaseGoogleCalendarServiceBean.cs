/*
 * Created by SharpDevelop.
 * User: Haykel
 * Date: 01/02/2015
 * Time: 02:39
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using WComp.Beans;
using System.Diagnostics;

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
	public class FreshVaseGoogleCalendarServiceBean
	{
		/// <summary>
		/// Fill in private attributes here.
		/// </summary>
		private Boolean state=false;
		private Process proc=null;

		/// <summary>
		/// This property will appear in bean's property panel and bean's input functions.
		/// </summary>
		public Boolean FreshVaseState {
			get { return state; }
			set {
				state = value;
				FireBooleanEvent(state);		// event will be fired for every property set.
			}
		}

		/// <summary>
		/// A method sending an event, which is here simply the argument + 1.
		/// Note that there is no return type to the method, because we use events to send
		/// information in WComp. Return values don't have to be used.
		/// </summary>
		public void GetFreshVaseStateFromGoogle() {
			

			if(File.Exists("D:\\GooglePhidget\\state.true"))
            {
				state=true;
				FireBooleanEvent(state);
			}
			else
			{
				Boolean tempstate=state;
				state=false;
				if(tempstate==true)
				{
				FireBooleanEvent(state);
				}
			}
			
			
			
		}
		
		
		
		
		
		public void StartVerificationProcess()
		{
			
			if(proc!=null)
			{
			proc.Dispose();
			}
			 proc = new Process();
            proc.StartInfo.FileName = "D:\\GooglePhidget\\FreshVaseGoogleCalendarService.exe";
            
            proc.Start();
		}

		/// <summary>
		/// Here are the delegate and his event.
		/// A function checking nullity should be used to fire events (like FireIntEvent).
		/// </summary>
		public delegate void BooleanValueEventHandler(Boolean val);
		/// <summary>
		/// the following declaration is the event by itself. Its name, here "PropertyChanged",
		/// is the name of the event as it will be displayed in the bean type's interface.
		/// </summary>
		public event BooleanValueEventHandler FreshVaseStateChanged;
		
		private void FireBooleanEvent(Boolean i) {
			if (FreshVaseStateChanged != null)
				FreshVaseStateChanged(i);
		}
	}
}
