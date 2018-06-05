/**
 * AutoSafe Firmware
 *
 * Copyright (c) 2018, AutoSafe, Inc.
 */

#include <stdio.h>
#include <Arduino.h>
#include "pins.h"

void GetDistance(uint8_t trigPin, uint8_t echoPin, float* distance){
  if(distance != NULL){
    long duration;
    // Clears the trigPin
    digitalWrite(trigPin, LOW);
    delayMicroseconds(2);
    // Sets the trigPin on HIGH state for 10 micro seconds
    digitalWrite(trigPin, HIGH);
    delayMicroseconds(10);
    digitalWrite(trigPin, LOW);
    // Reads the echoPin, returns the sound wave travel time in microseconds
    duration = pulseIn(echoPin, HIGH);
    // Calculating the distance
    float calculatedDistance = duration * 0.034 / 2;
    delay(10);

    *distance = calculatedDistance;
    Serial.println(*distance);
  }
}

void ReadInfrarodeSensor(uint8_t Infrarood, uint8_t* value){
  if( value != NULL){
    *value = analogRead(Infrarood);
    Serial.println(*value);
    delay(200);
  }
}

void ReadAllSensors(float* distance1, float* distance2, uint8_t* InfraValue1,uint8_t* InfraValue2,uint8_t* InfraValue3,uint8_t* InfraValue4){
  Serial.println("Ultrasone sensor 1: ");
  GetDistance(U1_trigPin, U1_echoPin, distance1);
  Serial.println("Ultrasone sensor 2: ");
  GetDistance(U2_trigPin, U2_echoPin, distance2);
  Serial.println("Infrarode sensor 1: ");
  ReadInfrarodeSensor(Infrarood1, InfraValue1);
  Serial.println("Infrarode sensor 2: ");
  ReadInfrarodeSensor(Infrarood2, InfraValue2);
  Serial.println("Infrarode sensor 3: ");
  ReadInfrarodeSensor(Infrarood3, InfraValue3);
  Serial.println("Infrarode sensor 4: ");
  ReadInfrarodeSensor(Infrarood4, InfraValue4);
}
