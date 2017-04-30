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
using Windows.System.Threading;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Food_Computer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private IStream connection;
        private RemoteDevice arduino;
        private const int pinNum = 13;
        private const int pinNum2 = 12;
        private bool pinState = false;
        private DispatcherTimer timer;

        public MainPage()
        {
            this.InitializeComponent();

            InitGPIO();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += Timer_Tick;

            

            

            arduino.digitalWrite(pinNum, PinState.HIGH);
            arduino.digitalWrite(pinNum2, PinState.HIGH);

            timer.Start();


        }

        private void Timer_Tick(object sender, object e)
        {
            if (pinState == false)
            {
            arduino.digitalWrite(pinNum, PinState.HIGH);
            arduino.digitalWrite(pinNum2, PinState.HIGH);
                pinState = true;
            }
            else
            {
                arduino.digitalWrite(pinNum, PinState.LOW);
                pinState = false;
            }
        }

         private void button_Click(object sender, RoutedEventArgs e)
        {
           this.Frame.Navigate(typeof(Manual));
        }

        private void InitGPIO()
        {


            //connection = new UsbSerial("VID_2341", "PID_0043"); For initial arduino
            connection = new UsbSerial("VID_2A03", "PID_0043");
            arduino = new RemoteDevice(connection);

            //Setup();

            arduino.DeviceReady += Setup;

            connection.begin(9600, SerialConfig.SERIAL_8N1);

            arduino.digitalWrite(pinNum, PinState.HIGH);
            arduino.digitalWrite(pinNum2, PinState.HIGH);

            //actuatePin.SetDriveMode(GpioPinDriveMode.Output);
            //pollPin.SetDriveMode(GpioPinDriveMode.Output);
            //setPin.SetDriveMode(GpioPinDriveMode.Output);
        }

        public void Setup()
        {
            //set all 3 pins to output
            arduino.pinMode(pinNum, PinMode.OUTPUT);
            arduino.pinMode(pinNum2, PinMode.OUTPUT);


            //arduino.pinMode(POLL_PIN, PinMode.OUTPUT);
            //arduino.pinMode(SET_PIN, PinMode.OUTPUT);

            //arduino.pinMode("A4", PinMode.ANALOG);
            //arduino.pinMode(4, PinMode.ONEWIRE);
            //arduino.pinMode(SET_PIN, PinMode.ONEWIRE);
        }




        private void Manual_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Manual));
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Settings));
        }

        private void Library_Click(object sender, RoutedEventArgs e)
        {

        }

        
    }
}
