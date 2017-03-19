using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maker.RemoteWiring;
using Microsoft.Maker.Serial;



namespace FoodComputerC
{
    class Fridge
    {
        private RemoteDevice arduino;
        private Module[] fridgeModules;
        private string pHPin;
        private string ecPin;
        private string wLevelPin;

        public Fridge(string pHPin, string ecPin, string wLevelPin)
        {
            this.pHPin = pHPin;
            this.ecPin = ecPin;
            this.wLevelPin = wLevelPin;
        }
    }
}
