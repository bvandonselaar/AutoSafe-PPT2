#include <stdio.h>
#include <Arduino.h>
#include "directions.h"
#include "sensors.h"
#include "category.h"
#include "protocol.h"
#include "pins.h"

uint8_t Speed = 255;
float distance1 = 0;  //distance ultrasone sensor 1
float distance2 =0;   //distance ultrasone sensor 2

uint8_t Ivalue1 = 0;
uint8_t Ivalue2 = 0;
uint8_t Ivalue3 = 0;
uint8_t Ivalue4 = 0;

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
}

void loop()
{
  Forward(&Speed);
  Serial.println("Forward");
  delay(1000);
  Break(&Speed);
  Serial.println("Break");
  delay(1000);
  Speed = 255;
  Backward(&Speed);
  Serial.println("Backward");
  delay(1000);
  Left(&Speed);
  Serial.println("Left");
  delay(1000);
  Right(&Speed);
  Serial.println("Right");
  delay(1000);
  Stop();
  Serial.println("Stop");

  ReadAllSensors(&distance1, &distance2, &Ivalue1, &Ivalue2, &Ivalue3, &Ivalue4);
}
