using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maker.RemoteWiring;
using Microsoft.Maker.Serial;

namespace FoodComputerCHeaded
{
    class Module
    {
        private RemoteDevice arduino; // If we have to use two unos, need to specify which one we'll be reading from
        private int tempPin; // indicates pin of 
        private string moisturePin; // Indicates pin of moisture sensor. Digital read fxn appears to want a string???
        private int ledPin; // Test

        public Module(RemoteDevice arduino, int tempPin, string moisturePin, int ledPin) //constructor
        {
            this.arduino = arduino;
            this.tempPin = tempPin;
            this.moisturePin = moisturePin;
            this.ledPin = ledPin;
        }

        public double readTemp() // Architecture of this and readHumidity depend on however firmata connection is implemented
        {
            return 0.0;
        }

        public double readHumidity()
        {
            return 0.0;
        }

        public ushort readMoisture()
        {
            return arduino.analogRead(moisturePin);
            //Debug.writeLine()
        }
    }
}
