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
		private Boolean ledLevel1=false;
		private Boolean ledLevel2=false;
		private Boolean ledLevel3=false;
		
		
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
		
		
		public Boolean ledLevel1State {
			get { return ledLevel1; }
			set {
				ledLevel1 = value;
				FireBooleanEvent(ledLevel1);		// event will be fired for every property set.
			}
		}
		
		
		public Boolean ledLevel2State {
			get { return ledLevel2; }
			set {
				ledLevel2 = value;
				FireBooleanEvent(ledLevel2);		// event will be fired for every property set.
			}
		}
		
		
		public Boolean ledLevel3State {
			get { return ledLevel3; }
			set {
				ledLevel3 = value;
				FireBooleanEvent(ledLevel3);		// event will be fired for every property set.
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
				ledLevel1=true;
				ledLevel2=true;
				ledLevel3=true;
				FireBooleanEvent(state);
				FireBooleanEvent1(ledLevel1);
				FireBooleanEvent2(ledLevel2);
				FireBooleanEvent3(ledLevel3);
			}
			else
			{
				Boolean tempstate=state;
				state=false;
				ledLevel1=false;
				ledLevel2=false;
				ledLevel3=false;
				if(tempstate==true)
				{
				FireBooleanEvent(state);
				FireBooleanEvent1(ledLevel1);
				FireBooleanEvent2(ledLevel2);
				FireBooleanEvent3(ledLevel3);
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
		public delegate void BooleanValueEvent1Handler(Boolean val);
		public delegate void BooleanValueEvent2Handler(Boolean val);
		public delegate void BooleanValueEvent3Handler(Boolean val);
		/// <summary>
		/// the following declaration is the event by itself. Its name, here "PropertyChanged",
		/// is the name of the event as it will be displayed in the bean type's interface.
		/// </summary>
		public event BooleanValueEventHandler FreshVaseStateChanged;
		public event BooleanValueEvent1Handler LedLevel1StateChanged;
		public event BooleanValueEvent2Handler LedLevel2StateChanged;
		public event BooleanValueEvent3Handler LedLevel3StateChanged;
		
		private void FireBooleanEvent(Boolean i) {
			if (FreshVaseStateChanged != null)
				FreshVaseStateChanged(i);
		}
		private void FireBooleanEvent1(Boolean i) {
			if (LedLevel1StateChanged != null)
				LedLevel1StateChanged(i);
		}
		private void FireBooleanEvent2(Boolean i) {
			if (LedLevel2StateChanged != null)
				LedLevel2StateChanged(i);
		}
		private void FireBooleanEvent3(Boolean i) {
			if (LedLevel3StateChanged != null)
				LedLevel3StateChanged(i);
		}
	}
}
