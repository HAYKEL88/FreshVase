I- Phidget Container :
We have used:
Four BoolMethodProbe :
1- setLedLevel1 : to set motor level 1 led state (true or false).
2- setLedLevel2 : to set motor level 2 led state (true or false).
3- setLedLevel3 : to set motor level 3 led state (true or false).
4- setMotorState : to set motor state (true or false).

one StringMethodProbe :
1- stringSetVelocity : to set motor speed (from -100 to +100)

and two Beans :
1- StringToInt : Bean to convert any string value to an int value, used to convert speed gotten from stringSetVelocity to pass it to the motor controller bean.
2- MyMotorControlBean : Bean to communicate with motor ( load motor, stop motor, reset encoder, setVelocity, setAcceleration, setBraking, setBackEmfSensing,setRatiometric ...


II- WCOMP Container :

1- Button : to start system.
2- Timer : to start verification services processes every k=15 seconds.
3- FreshVaseGoogleCalendarServiceBean : Verify all user events in google calendar and decide if it should run the system or not.
4- FreshVaseWebServiceSMS : Verify if the user send a message to stop system or start it with specified parameters like speed, start time.
5- ToString : to convert motor speed gotten as int to string.
6- FreshVaseDevice_Functional_0 : Fresh Vase Device which embedded all functionalities of device such as setLedLevel2 and setMotorState.

