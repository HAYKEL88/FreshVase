/*
 * Crée par SharpDevelop.
 * Utilisateur: OUHICHI
 * Date: 25/12/2014
 * Heure: 19:21
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using System.Text;
using WComp.Beans;
using Phidgets; //Needed for the MotorControl class, Phidget base classes, and the PhidgetException class
using Phidgets.Events; //Needed for the Phidget event handling classes

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
	public class MyMotorControlBean
	{
		/// <summary>
		/// Fill in private attributes here.
		/// </summary>
		private MotorControl motoControl=null; //Declare a MotorControl object
		
		
		
		
		  public void setMotorState(Boolean state)
        {
        	if(state == true)
        	{
        		if(motoControl==null)
        		{
        		loadMotorControl();
        		}
        		
        		int i=0,k=0;
        		for(i=0;i<999999;i++)
        			k++;k--;
        			
        		setVelocity(100);
        	}
        	else
        	{
        		if(motoControl!=null)
        		{
        		//resetEncoder();
        		stopMotorControl();
        		}
        	}
        }
		  
		  
		  
		  
		  
		//initialize the MotorConrol object and hook the event handlers
        public void loadMotorControl()
        {
            motoControl = new MotorControl();

            motoControl.Attach += new AttachEventHandler(motoControl_Attach);
            motoControl.Detach += new DetachEventHandler(motoControl_Detach);
            motoControl.Error += new ErrorEventHandler(motoControl_Error);

            motoControl.CurrentChange += new CurrentChangeEventHandler(motoControl_CurrentChange);
            motoControl.InputChange += new InputChangeEventHandler(motoControl_InputChange);
            motoControl.VelocityChange += new VelocityChangeEventHandler(motoControl_VelocityChange);
            motoControl.BackEMFUpdate += new BackEMFUpdateEventHandler(motoControl_BackEMFUpdate);
            motoControl.EncoderPositionChange += new EncoderPositionChangeEventHandler(motoControl_EncoderPositionChange);
            motoControl.SensorUpdate += new SensorUpdateEventHandler(motoControl_SensorUpdate);

            openCmdLine(motoControl);
        }
		
		
		
		//When the form is being close, make sure to stop all the motors and close the Phidget.
        public void stopMotorControl()
        {
            motoControl.Attach -= motoControl_Attach;
            motoControl.Detach -= motoControl_Detach;
            motoControl.Error -= motoControl_Error;

            motoControl.InputChange -= motoControl_InputChange;
            motoControl.CurrentChange -= motoControl_CurrentChange;
            motoControl.VelocityChange -= motoControl_VelocityChange;
            motoControl.BackEMFUpdate -= motoControl_BackEMFUpdate;
            motoControl.EncoderPositionChange -= motoControl_EncoderPositionChange;
            motoControl.SensorUpdate -= motoControl_SensorUpdate;

            if (motoControl.Attached)
            {
                foreach (MotorControlMotor motor in motoControl.motors)
                {
                    motor.Velocity = 0;
                }
            }

            //run any events in the message queue - otherwise close will hang if there are any outstanding events
            //Application.DoEvents();

            motoControl.close();

            motoControl = null;
        }
		
		
        
        
        void motoControl_Error(object sender, ErrorEventArgs e)
        {
            
        }
         void motoControl_CurrentChange(object sender, CurrentChangeEventArgs e)
        {
            
        }
          void motoControl_InputChange(object sender, InputChangeEventArgs e)
        {
            
        }
        void motoControl_VelocityChange(object sender, VelocityChangeEventArgs e)
        {
            
        }
        void motoControl_BackEMFUpdate(object sender, BackEMFUpdateEventArgs e)
        {
            
        }
        void motoControl_EncoderPositionChange(object sender, EncoderPositionChangeEventArgs e)
        {
            
        }
        void motoControl_SensorUpdate(object sender, SensorUpdateEventArgs e)
        {
            
        }

		
		
		
        //MotorControl Attach event handler...populate the fields and controls
        void motoControl_Attach(object sender, AttachEventArgs e)
        {
            MotorControl attached = (MotorControl)sender;
           
            //supplyVoltageTimer.Start();
            
        }
        
  
        void motoControl_Detach(object sender, DetachEventArgs e)
        {
        	 MotorControl detached = (MotorControl)sender;
           
            //supplyVoltageTimer.Stop();
        	
        }
        
        

      
        
        


        //When the acceleration trakbar is changed send this data to the motor control to affect the motor
        //If no motor control is attached, a PhidgetException will be thrown, so we will catch it and deal
        //with it accordingly
        public void setAcceleration(int acceleration)
        {
            try
            {
                motoControl.motors[0].Acceleration = acceleration;
            }
            catch { }
        }

        //When the velocity trakbar is changed send this data tothe motor control to affect the motor
        //If no motor control is attached, a PhidgetException will be thrown, so we will catch it and deal
        //with it accordingly
        public void setVelocity(int velocity)
        {
            try
            {
                motoControl.motors[0].Velocity = velocity;
            }
            catch { }
        }

        public void setBraking(int braking)
        {
            try
            {
                motoControl.motors[0].Braking = braking;
            }
            catch { }
        }    

        public void setBackEmfSensing(Boolean backEmfSensing)
        {
            try
            {
                motoControl.motors[0].BackEMFSensing = backEmfSensing;
            }
            catch { }
        }

        public void resetEncoder()
        {
            try
            {
                motoControl.encoders[0].Position = 0;
            }
            catch { }
        }

        public void setRatiometric(Boolean ratiometric)
        {
            try
            {
                motoControl.Ratiometric = ratiometric;
            }
            catch { }
        }

        //Parses command line arguments and calls the appropriate open
        #region Command line open functions
        private void openCmdLine(Phidget p)
        {
            openCmdLine(p, null);
        }
        private void openCmdLine(Phidget p, String pass)
        {
            int serial = -1;
            String logFile = null;
            int port = 5001;
            String host = null;
            bool remote = false, remoteIP = false;
            string[] args = Environment.GetCommandLineArgs();
            String appName = args[0];

            try
            { //Parse the flags
                for (int i = 1; i < args.Length; i++)
                {
                    if (args[i].StartsWith("-"))
                        switch (args[i].Remove(0, 1).ToLower())
                        {
                            case "l":
                                logFile = (args[++i]);
                                break;
                            case "n":
                                serial = int.Parse(args[++i]);
                                break;
                            case "r":
                                remote = true;
                                break;
                            case "s":
                                remote = true;
                                host = args[++i];
                                break;
                            case "p":
                                pass = args[++i];
                                break;
                            case "i":
                                remoteIP = true;
                                host = args[++i];
                                if (host.Contains(":"))
                                {
                                    port = int.Parse(host.Split(':')[1]);
                                    host = host.Split(':')[0];
                                }
                                break;
                            default:
                                goto usage;
                        }
                    else
                        goto usage;
                }
                if (logFile != null)
                    Phidget.enableLogging(Phidget.LogLevel.PHIDGET_LOG_INFO, logFile);
                if (remoteIP)
                    p.open(serial, host, port, pass);
                else if (remote)
                    p.open(serial, host, pass);
                else
                    p.open(serial);
                return; //success
            }
            catch { }
        usage:
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Invalid Command line arguments." + Environment.NewLine);
            sb.AppendLine("Usage: " + appName + " [Flags...]");
            sb.AppendLine("Flags:\t-n   serialNumber\tSerial Number, omit for any serial");
            sb.AppendLine("\t-l   logFile\tEnable phidget21 logging to logFile.");
            sb.AppendLine("\t-r\t\tOpen remotely");
            sb.AppendLine("\t-s   serverID\tServer ID, omit for any server");
            sb.AppendLine("\t-i   ipAddress:port\tIp Address and Port. Port is optional, defaults to 5001");
            sb.AppendLine("\t-p   password\tPassword, omit for no password" + Environment.NewLine);
            sb.AppendLine("Examples: ");
            sb.AppendLine(appName + " -n 50098");
            sb.AppendLine(appName + " -r");
            sb.AppendLine(appName + " -s myphidgetserver");
            sb.AppendLine(appName + " -n 45670 -i 127.0.0.1:5001 -p paswrd");
          //  MessageBox.Show(sb.ToString(), "Argument Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

          //  Application.Exit();
        }
        #endregion
    
	
	
	
	
	   
		/// <summary>
		/// Here are the delegate and his event.
		/// A function checking nullity should be used to fire events (like FireIntEvent).
		/// </summary>
		public delegate void IntValueEventHandler(int val);
		/// <summary>
		/// the following declaration is the event by itself. Its name, here "PropertyChanged",
		/// is the name of the event as it will be displayed in the bean type's interface.
		/// </summary>
		public event IntValueEventHandler PropertyChanged;
		
		private void FireIntEvent(int i) {
			if (PropertyChanged != null)
				PropertyChanged(i);
		}	
		
		
		
	
		
	}
}
