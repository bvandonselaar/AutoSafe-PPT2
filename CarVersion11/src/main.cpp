#include <stdio.h>
#include <Arduino.h>
#include <Wire.h>
#include <directions.h>
#include <sensors.h>
#include <category.h>
#include <protocol.h>
#include <pins.h>
#include <receive.h>

uint8_t Speed = 255;

int newMessage = 0;
uint8_t data[50];
size_t size;

void receiveEvent(int howMany){
  int counter = 0;
  while(Wire.available() > 0){
    data[counter] = Wire.read();
    counter++;
    if(counter == howMany){
      size = counter;
      newMessage = 1;
      break;
    }
  }
}

void writeEvent()
{

}

void setup()
{
  Serial.begin(9600);

  pinMode(M1_PWM, OUTPUT);    //MOTOR 1
  pinMode(M1_Dir1, OUTPUT);
  pinMode(M1_Dir2, OUTPUT);

  pinMode(M2_PWM, OUTPUT);    //MOTOR 2
  pinMode(M2_Dir1, OUTPUT);
  pinMode(M2_Dir2, OUTPUT);

  pinMode(U1_trigPin, OUTPUT); //ultasone sensor 1
  pinMode(U1_echoPin, INPUT);

  pinMode(U2_trigPin, OUTPUT);  //ultrasone sensor 2
  pinMode(U2_echoPin, INPUT);

  pinMode(Infrarood1, INPUT);
  pinMode(Infrarood2, INPUT);
  pinMode(Infrarood3, INPUT);
  pinMode(Infrarood4, INPUT);

  Wire.begin(42);
  Wire.onReceive(receiveEvent);
  Wire.onRequest(writeEvent);
}

void loop()
{
    if(newMessage == 1)
    {
        
    }
    
}
