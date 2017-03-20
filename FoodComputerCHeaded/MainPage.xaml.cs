using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Maker.RemoteWiring;
using Microsoft.Maker.Serial;
using System.Text;
using System.Net.Http;
using Windows.ApplicationModel.Background;
using Windows.System.Threading;
using Windows.Devices.Gpio;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FoodComputerCHeaded
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private DispatcherTimer timer;
        private const int ACTUATE_PIN = 5;
        private const int POLL_PIN = 6;
        private const int SET_PIN = 7;

        private GpioPin actuatePin;
        private GpioPinValue actuatePinValue;
        private GpioPin pollPin;
        private GpioPinValue pollPinValue;
        private GpioPin setPin;
        private GpioPinValue setPinValue;

        private int myMoisture; // this is just for testing

        private GpioPinValue pinValue;

        //private ThreadPoolTimer timer;
        private string state = "actuate";

        private IStream connection;
        private RemoteDevice arduino;

        private Module testModule;

        public MainPage()
        {
            this.InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, object e)
        {
            switch (state)
            {
                case "actuate":


                    //actuatePin.Write(GpioPinValue.High);
                    //pollPin.Write(GpioPinValue.Low);
                    //setPin.Write(GpioPinValue.Low);



                    arduino.digitalWrite(ACTUATE_PIN, PinState.HIGH);
                    arduino.digitalWrite(POLL_PIN, PinState.LOW);
                    arduino.digitalWrite(SET_PIN, PinState.LOW);

                    state = "poll";

                    break;

                case "poll":


                    //actuatePin.Write(GpioPinValue.Low);
                    //pollPin.Write(GpioPinValue.High);
                    //setPin.Write(GpioPinValue.Low);


                    myMoisture = testModule.readMoisture();
                    

                    arduino.digitalWrite(ACTUATE_PIN, PinState.LOW);
                    arduino.digitalWrite(POLL_PIN, PinState.HIGH);
                    arduino.digitalWrite(SET_PIN, PinState.LOW);

                    state = "set";

                    break;

                case "set":


                    //actuatePin.Write(GpioPinValue.Low);
                    //pollPin.Write(GpioPinValue.Low);
                    //setPin.Write(GpioPinValue.High);
                    testMoistureBox.Text = myMoisture.ToString();

                    arduino.digitalWrite(ACTUATE_PIN, PinState.LOW);
                    arduino.digitalWrite(POLL_PIN, PinState.LOW);
                    arduino.digitalWrite(SET_PIN, PinState.HIGH);

                    state = "actuate";

                    break;
            }
    }


/* All this is from the og headed code, we'll need to import it at some point
            public void Run(IBackgroundTaskInstance taskInstance)
            {
                _deferral = taskInstance.GetDeferral();

                InitGPIO();

                timer = ThreadPoolTimer.CreatePeriodicTimer(Timer_Tick,
                           TimeSpan.FromMilliseconds(1000));
            }

            public void Timer_Tick(ThreadPoolTimer timer)
            {
                switch (state)
                {
                    case "actuate":

                        
                        //actuatePin.Write(GpioPinValue.High);
                        //pollPin.Write(GpioPinValue.Low);
                        //setPin.Write(GpioPinValue.Low);
                        


                        arduino.digitalWrite(ACTUATE_PIN, PinState.HIGH);
                        arduino.digitalWrite(POLL_PIN, PinState.LOW);
                        arduino.digitalWrite(SET_PIN, PinState.LOW);

                        state = "poll";

                        break;

                    case "poll":

                        
                        //actuatePin.Write(GpioPinValue.Low);
                        //pollPin.Write(GpioPinValue.High);
                        //setPin.Write(GpioPinValue.Low);
                        

                        myMoisture = testModule.readMoisture();
                        arduino.digitalWrite(ACTUATE_PIN, PinState.LOW);
                        arduino.digitalWrite(POLL_PIN, PinState.HIGH);
                        arduino.digitalWrite(SET_PIN, PinState.LOW);

                        state = "set";

                        break;

                    case "set":

                        
                        //actuatePin.Write(GpioPinValue.Low);
                        //pollPin.Write(GpioPinValue.Low);
                        //setPin.Write(GpioPinValue.High);
                        

                        arduino.digitalWrite(ACTUATE_PIN, PinState.LOW);
                        arduino.digitalWrite(POLL_PIN, PinState.LOW);
                        arduino.digitalWrite(SET_PIN, PinState.HIGH);

                        state = "actuate";

                        break;
                }
            }

*/

            // This is used for the raspberry pi pin I/O
            private void InitGPIO()
            {
                /*
                GpioController gpio = GpioController.GetDefault();
                actuatePin = gpio.OpenPin(ACTUATE_PIN);
                pollPin = gpio.OpenPin(POLL_PIN);
                setPin = gpio.OpenPin(SET_PIN);
                */



                connection = new UsbSerial("VID_2341", "PID_0043");
                arduino = new RemoteDevice(connection);

                testModule = new Module(arduino, 0, "0", 5);

                Setup();

                arduino.DeviceReady += Setup;

                connection.begin(57600, SerialConfig.SERIAL_8N1);

                //actuatePin.SetDriveMode(GpioPinDriveMode.Output);
                //pollPin.SetDriveMode(GpioPinDriveMode.Output);
                //setPin.SetDriveMode(GpioPinDriveMode.Output);
            }


            public void SetupRemoteArduino()
            {
                //connection = new UsbSerial("MyBTDevice");
                //arduino = new RemoteDevice(connection);
                //arduino = App.Arduino;

                //arduino.DeviceReady += Setup;

                //connection.begin(9600, SerialConfig.SERIAL_8N1);
            }

            public void Setup()
            {
                //set all 3 pins to output
                arduino.pinMode(ACTUATE_PIN, PinMode.OUTPUT);
                arduino.pinMode(POLL_PIN, PinMode.OUTPUT);
                arduino.pinMode(SET_PIN, PinMode.OUTPUT);
            }
        }
    }
    

