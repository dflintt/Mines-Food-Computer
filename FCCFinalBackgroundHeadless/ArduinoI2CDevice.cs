using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maker.RemoteWiring;
using Microsoft.Maker.Serial;
using System.Threading;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Net.Http;
using Windows.ApplicationModel.Background;
using Windows.System.Threading;
using Windows.Devices.Gpio;
using Windows.Devices.I2c;


namespace FCCFinalBackgroundHeadless
{
    class ArduinoI2CDevice
    {
        private RemoteDevice arduino; // If we have to use two unos, need to specify which one we'll be reading from
        private byte _i2caddr;

        //private I2cDevice rpiI2CDev;

        public ArduinoI2CDevice(RemoteDevice arduino, byte addr) // addr should be x41 i think?
        {
            this.arduino = arduino;
            this.arduino.I2c.enable();

            this._i2caddr = addr;
        }

        public void setPWMFreq(float freq) // Need to change this to use different frequencies! (Rerun trevors testcode with frq changed)
        {
            /*freq *= (float) 0.9;  // Correct for overshoot in the frequency setting (see issue #11).
            float prescaleval = 25000000;
            prescaleval /= 4096;
            prescaleval /= freq;
            prescaleval -= 1; */

            write8(0x0, 127);
            write8(0xFE, 6);
            write8(0x0, 255);

            // delay for 5 ms possibly

            write8(0x0, 255);
        }

        public void begin()
        {
            arduino.I2c.enable();
            write8(0x0, 0x0);
        }

        public void setPWM(byte num, UInt16 on, UInt16 off)
        {
            arduino.I2c.beginTransmission(_i2caddr);
            arduino.I2c.write((byte)(0x6 + 4 * num));
            arduino.I2c.write((byte)on);
            arduino.I2c.write((byte)(on >> 8));
            arduino.I2c.write((byte)off);
            arduino.I2c.write((byte)(off >> 8));
            arduino.I2c.endTransmission();
        }

        public void write8(byte addr, byte d)
        {
            arduino.I2c.beginTransmission(_i2caddr);
            arduino.I2c.write(addr);
            arduino.I2c.write(d);
            arduino.I2c.endTransmission();
        }
    }
}
