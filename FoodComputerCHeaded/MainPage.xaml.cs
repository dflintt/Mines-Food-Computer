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
using Windows.Devices.I2c;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FoodComputerCHeaded
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private DispatcherTimer timer;
        private const int ACTUATE_PIN = 13;
        private const int POLL_PIN = 12;
        private const int SET_PIN = 11;

        private GpioPin actuatePin;
        private GpioPinValue actuatePinValue;
        private GpioPin pollPin;
        private GpioPinValue pollPinValue;
        private GpioPin setPin;
        private GpioPinValue setPinValue;

        private SolidColorBrush redBrush = new SolidColorBrush(Windows.UI.Colors.Red);
        private SolidColorBrush grayBrush = new SolidColorBrush(Windows.UI.Colors.Gray);

        private ushort myMoisture; // this is just for testing

        private GpioPinValue pinValue;

        //private ThreadPoolTimer timer;
        private string state = "actuate";


        private IStream connection;
        private RemoteDevice arduino;

        private Module testModule;


        

       

        //private I2c

        public MainPage()
        {
            this.InitializeComponent();

            InitGPIO();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += Timer_Tick;

            timer.Start();

            //I2cDevice.
        }

        private void Timer_Tick(object sender, object e)
        {
            switch (state)
            {
                case "actuate":

                    arduino.digitalWrite(ACTUATE_PIN, PinState.HIGH);
                    arduino.digitalWrite(POLL_PIN, PinState.LOW);
                    arduino.digitalWrite(SET_PIN, PinState.LOW);

                    ActuateIndicator.Fill = redBrush;
                    PollIndicator.Fill = grayBrush;
                    SetIndicator.Fill = grayBrush;

                    state = "poll";

                    break;

                case "poll":

                    myMoisture = testModule.readMoisture();
                    

                    arduino.digitalWrite(ACTUATE_PIN, PinState.LOW);
                    arduino.digitalWrite(POLL_PIN, PinState.HIGH);
                    arduino.digitalWrite(SET_PIN, PinState.LOW);

                    ActuateIndicator.Fill = grayBrush;
                    PollIndicator.Fill = redBrush;
                    SetIndicator.Fill = grayBrush;


                    //arduino.I2c.r

                    //arduino.I2c.

                    state = "set";

                    break;

                case "set":

                    //testMoistureBox.Text = myMoisture.ToString();
                    //testMoistureBox.Text = arduino.analogRead("A4").ToString();
                    //testMoistureBox.Text = 101.ToString();

                    ushort temp = arduino.analogRead("A4");

                    arduino.digitalWrite(ACTUATE_PIN, PinState.LOW);
                    arduino.digitalWrite(POLL_PIN, PinState.LOW);
                    arduino.digitalWrite(SET_PIN, PinState.HIGH);

                    arduino.analogWrite(SET_PIN, 700);
                    arduino.analogWrite(POLL_PIN, 700);

                    
                    if (temp == 0)
                    {
                        arduino.analogWrite(ACTUATE_PIN, 0);
                    }

                    else if (temp == 1023)
                    {
                        arduino.analogWrite(ACTUATE_PIN, 255);
                    }

                    else
                    {
                        arduino.analogWrite(ACTUATE_PIN, (ushort) (temp / 4));
                    }
                    

                    ActuateIndicator.Fill = grayBrush;
                    PollIndicator.Fill = grayBrush;
                    SetIndicator.Fill = redBrush;

                    state = "actuate";

                    break;
            }
    }



            // This is used for the raspberry pi pin I/O
            private void InitGPIO()
            {
            /*
            GpioController gpio = GpioController.GetDefault();
            actuatePin = gpio.OpenPin(ACTUATE_PIN);
            pollPin = gpio.OpenPin(POLL_PIN);
            setPin = gpio.OpenPin(SET_PIN);
            */

            



            //connection = new UsbSerial("VID_2341", "PID_0043"); For initial arduino
            connection = new UsbSerial("VID_2A03", "PID_0043");
            arduino = new RemoteDevice(connection);

            testModule = new Module(arduino, 0, "0", 5);

                //Setup();

            arduino.DeviceReady += Setup;


            

            connection.begin(9600, SerialConfig.SERIAL_8N1);
            
                

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

               // arduino.pinMode("A4", PinMode.ANALOG);
                //arduino.pinMode(4, PinMode.ONEWIRE);
                //arduino.pinMode(SET_PIN, PinMode.ONEWIRE);
            }

    }
    }
    

