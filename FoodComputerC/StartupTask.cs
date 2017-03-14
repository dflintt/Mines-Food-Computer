using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Windows.ApplicationModel.Background;
using Windows.System.Threading;
using Windows.Devices.Gpio;
using Microsoft.Maker.RemoteWiring;
using Microsoft.Maker.Serial;

// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

// GND Pin 6
// Actuate Pin 29
// Poll Pin 31
// Set Pin 26

namespace FoodComputerC
{
    public sealed class StartupTask : IBackgroundTask
    {
        BackgroundTaskDeferral _deferral;
        private const int ACTUATE_PIN = 5;
        private const int POLL_PIN = 6;
        private const int SET_PIN = 7;

        private GpioPin actuatePin;
        private GpioPinValue actuatePinValue;
        private GpioPin pollPin;
        private GpioPinValue pollPinValue;
        private GpioPin setPin;
        private GpioPinValue setPinValue;

        private GpioPinValue pinValue;

        private ThreadPoolTimer timer;
        private string state = "actuate";

        private IStream connection;
        private RemoteDevice arduino; 

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            _deferral = taskInstance.GetDeferral();

            SetupRemoteArduino();

            timer = ThreadPoolTimer.CreatePeriodicTimer(Timer_Tick,
                       TimeSpan.FromMilliseconds(1000));
        }

        public void Timer_Tick(ThreadPoolTimer timer)
        {
            switch (state)
            {
                case "actuate":
                    /*
                    actuatePin.Write(GpioPinValue.High);
                    pollPin.Write(GpioPinValue.Low);
                    setPin.Write(GpioPinValue.Low);
                    */
                    arduino.digitalWrite(ACTUATE_PIN, PinState.HIGH);
                    arduino.digitalWrite(POLL_PIN, PinState.LOW);
                    arduino.digitalWrite(SET_PIN, PinState.LOW);
                    state = "poll";

                    break;

                case "poll":
                    /*
                    actuatePin.Write(GpioPinValue.Low);
                    pollPin.Write(GpioPinValue.High);
                    setPin.Write(GpioPinValue.Low);
                    */
                    arduino.digitalWrite(ACTUATE_PIN, PinState.LOW);
                    arduino.digitalWrite(POLL_PIN, PinState.HIGH);
                    arduino.digitalWrite(SET_PIN, PinState.LOW);
                    state = "set";

                    break;

                case "set":
                    /*
                    actuatePin.Write(GpioPinValue.Low);
                    pollPin.Write(GpioPinValue.Low);
                    setPin.Write(GpioPinValue.High);
                    */
                    arduino.digitalWrite(ACTUATE_PIN, PinState.LOW);
                    arduino.digitalWrite(POLL_PIN, PinState.LOW);
                    arduino.digitalWrite(SET_PIN, PinState.HIGH);
                    state = "actuate";

                    break;
            }
        }

        /* This is used for the raspberry pi pin I/O
        private void InitGPIO()
        {
            GpioController gpio = GpioController.GetDefault();
            actuatePin = gpio.OpenPin(ACTUATE_PIN);
            pollPin = gpio.OpenPin(POLL_PIN);
            setPin = gpio.OpenPin(SET_PIN);

            connection = new BluetoothSerial("MyBTDevice");
            arduino = new RemoteDevice(connection);

            arduino.DeviceReady += Setup;

            connection.begin(115200, SerialConfig.SERIAL_8N1);

            actuatePin.SetDriveMode(GpioPinDriveMode.Output);
            pollPin.SetDriveMode(GpioPinDriveMode.Output);
            setPin.SetDriveMode(GpioPinDriveMode.Output);
        }
        */

        public void SetupRemoteArduino()
        {
            connection = new UsbSerial("MyBTDevice");
            arduino = new RemoteDevice(connection);

            arduino.DeviceReady += Setup;

            connection.begin(9600, SerialConfig.SERIAL_8N1);
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