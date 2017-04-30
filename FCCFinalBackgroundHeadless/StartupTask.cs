using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Windows.ApplicationModel.Background;
using Windows.System.Threading;
using Microsoft.Maker.RemoteWiring;
using Microsoft.Maker.Serial;
using Windows.Devices.Gpio;

// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace FCCFinalBackgroundHeadless
{
    public sealed class StartupTask : IBackgroundTask
    {
        BackgroundTaskDeferral _deferral; 
        private const int LED1_PIN = 13;
        private const int LED2_PIN = 12;
        private const int LED3_PIN = 5;

        private int ledState = 0;


        private ThreadPoolTimer timer;

        private IStream connection1;
        private IStream connection2;
        private RemoteDevice arduino1;
        private RemoteDevice arduino2;

        //private ArduinoI2CDevice pwm;

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            _deferral = taskInstance.GetDeferral(); // Ensure that we don't quit when run function exits (since we want to keep timer ticking)

            InitSystem();

            ThreadPoolTimer timer = ThreadPoolTimer.CreatePeriodicTimer(Timer_Tick, TimeSpan.FromMilliseconds(3000));
        }

        private void Timer_Tick(ThreadPoolTimer timer)
        {
            
            if (ledState == 1)
            {
                arduino1.digitalWrite(LED1_PIN, PinState.LOW);
                arduino1.digitalWrite(LED2_PIN, PinState.LOW);
                //arduino2.digitalWrite(LED3_PIN, PinState.LOW);
                ledState = 0;

            }
            else
            {

                arduino1.digitalWrite(LED1_PIN, PinState.HIGH);
                arduino1.digitalWrite(LED2_PIN, PinState.HIGH);
                //arduino2.digitalWrite(LED3_PIN, PinState.HIGH);
                ledState = 1;

            }

           /*
            if (pwm != null)
            {
                arduino1.digitalWrite(LED1_PIN, PinState.HIGH);
                arduino1.digitalWrite(LED2_PIN, PinState.HIGH);
                pwm.setPWM(0, 0, 0); // channel indicates channel, first number indicates time of switch from low to high (out of 4095), second number from high to low
                pwm.setPWM(1, 0, 0); // for example, 14, 1024, 3072 is low from 0-1023, high from 1024-3072, low from 3072 to 4095 giving a 50% dty cycle
                pwm.setPWM(2, 0, 0);
                pwm.setPWM(3, 0, 0);
                pwm.setPWM(4, 0, 0);

                for (UInt32 i = 0; i < 5; i++)
                {
                    for (UInt32 j = 0; j < 2500; j++)
                    {
                        pwm.setPWM((byte)i, 0, (UInt16)j);
                        pwm.setPWM(0, 0, 0);
                    }
                    pwm.setPWM(0, 0, 0);
                    pwm.setPWM(1, 0, 0);
                    pwm.setPWM(2, 0, 0);
                    pwm.setPWM(3, 0, 0);
                    pwm.setPWM(4, 0, 0);
                }
            }
            */
            

        }

        private void InitSystem()
        {
            connection1 = new UsbSerial("VID_2A03", "PID_0043"); // Default for uno
            arduino1 = new RemoteDevice(connection1);

            ///connection1 = new UsbSerial("VID_2A03", "PID_0042"); // Default for MAGA
            //arduino1 = new RemoteDevice(connection1);

            //connection2 = new UsbSerial("VID_2A03", "PID_0043");
            //arduino2 = new RemoteDevice(connection2);

            arduino1.DeviceReady += SetupAOne;
            //arduino2.DeviceReady += SetupATwo;

            //connection1.begin(57600, SerialConfig.SERIAL_8N1);
            connection1.begin(9600, SerialConfig.SERIAL_8N1);
            //connection2.begin(9600, SerialConfig.SERIAL_8N1);



        }

        private void SetupAOne()
        {
            arduino1.pinMode(LED1_PIN, PinMode.OUTPUT);
            arduino1.pinMode(LED2_PIN, PinMode.OUTPUT);


            arduino1.digitalWrite(LED1_PIN, PinState.LOW); //init to match state declarations up there
            arduino1.digitalWrite(LED2_PIN, PinState.LOW);

            //pwm = new ArduinoI2CDevice(arduino1, 0x41);
            //pwm.begin();
            //pwm.setPWMFreq(1000);  // temp hardcoded to 1000

            // save I2C bitrate
            //uint8_t twbrbackup = TWBR;
            // must be changed after calling Wire.begin() (inside pwm.begin())
            //TWBR = 12; // upgrade to 400KHz!

        }

        private void SetupATwo()
        {
            arduino2.pinMode(LED3_PIN, PinMode.OUTPUT);
            arduino2.digitalWrite(LED3_PIN, PinState.LOW);
        }
    }
}
