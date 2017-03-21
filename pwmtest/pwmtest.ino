/*************************************************** 
  This is an example for our Adafruit 16-channel PWM & Servo driver
  PWM test - this will drive 16 PWMs in a 'wave'

  Pick one up today in the adafruit shop!
  ------> http://www.adafruit.com/products/815

  These displays use I2C to communicate, 2 pins are required to  
  interface. For Arduino UNOs, thats SCL -> Analog 5, SDA -> Analog 4

  Adafruit invests time and resources providing this open source code, 
  please support Adafruit and open-source hardware by purchasing 
  products from Adafruit!

  Written by Limor Fried/Ladyada for Adafruit Industries.  
  BSD license, all text above must be included in any redistribution
 ****************************************************/

#include <Wire.h>
#include <Adafruit_PWMServoDriver.h>
#include "Adafruit_MCP9808.h"

#define I2C_SLOWMODE 1

// called this way, it uses the default address 0x40
//Adafruit_PWMServoDriver pwm = Adafruit_PWMServoDriver();
// you can also call it with a different address you want
Adafruit_PWMServoDriver pwm = Adafruit_PWMServoDriver(0x41);
Adafruit_MCP9808 tempsensor = Adafruit_MCP9808();


void setup() {
  Serial.begin(9600);
  Serial.println("16 channel PWM test!");

  // if you want to really speed stuff up, you can go into 'fast 400khz I2C' mode
  // some i2c devices dont like this so much so if you're sharing the bus, watch
  // out for this!

  pwm.begin();
  pwm.setPWMFreq(1000);  // This is the maximum PWM frequency
    
  // save I2C bitrate
  uint8_t twbrbackup = TWBR;
  // must be changed after calling Wire.begin() (inside pwm.begin())
  TWBR = 12; // upgrade to 400KHz!

      if (!tempsensor.begin(0x19)) {
    Serial.println("Couldn't find MCP9808!");
  }
}

void loop() {
  pwm.setPWM(0,0,0);
  pwm.setPWM(1,0,0);
  pwm.setPWM(2,0,0);
  pwm.setPWM(3,0,0);
  pwm.setPWM(4,0,0);
  
  for(int i=0; i<5; i++)
  {
    for(int j=0; j<2500; j++)
    {
      pwm.setPWM(i,0,j);
        pwm.setPWM(0,0,0);
    }
      pwm.setPWM(0,0,0);
    pwm.setPWM(1,0,0);
    pwm.setPWM(2,0,0);
    pwm.setPWM(3,0,0);
    pwm.setPWM(4,0,0);
  }
//for(;;)
//{
//  pwm.setPWM(0,0,2200);
//  pwm.setPWM(1,0,2200);
//  pwm.setPWM(2,0,2200);
//  pwm.setPWM(3,0,2200);
//  pwm.setPWM(4,0,2200);
//  
//   delay(5000);
//}

}
