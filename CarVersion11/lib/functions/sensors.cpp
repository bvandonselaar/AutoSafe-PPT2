/**
 * AutoSafe Firmware
 *
 * Copyright (c) 2018, AutoSafe, Inc.
 */

#include <stdio.h>
#include <Arduino.h>
#include <pins.h>
#include <directions.h>

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

void ReadAllSensors(uint8_t* speed, uint8_t* state){
    float distanceU1 = 0;
    uint8_t infraValue1 = 0;
    uint8_t infraValue2 = 0;
    uint8_t infraValue3 = 0;
    uint8_t infraValue4 = 0;
    *state = 0;
    Serial.println("Ultrasone sensor 1: ");
    GetDistance(U1_trigPin, U1_echoPin, &distanceU1);
    Serial.println("Infrarode sensor 1: ");
    ReadInfrarodeSensor(Infrarood1, &infraValue1);
    Serial.println("Infrarode sensor 2: ");
    ReadInfrarodeSensor(Infrarood2, &infraValue2);
    Serial.println("Infrarode sensor 3: ");
    ReadInfrarodeSensor(Infrarood3, &infraValue3);
    Serial.println("Infrarode sensor 4: ");
    ReadInfrarodeSensor(Infrarood4, &infraValue4);

    if(distanceU1 < 5.00)
    {
        *state = 1;
        digitalWrite(led, HIGH);
        Stop();
    }
    if(distanceU1 < 10.00)
    {
        *state = 1;
        digitalWrite(led, HIGH);
        Break(speed);
    }

    if(infraValue1 < 5 || infraValue2 < 5 || infraValue3 < 5 || infraValue4 < 5)
    {
        *state = 1;
        digitalWrite(led, HIGH);
        Stop();
    }
    if(infraValue1 < 10 || infraValue2 < 10 || infraValue3 < 10 || infraValue4 < 10)
    {
        *state = 1;
        digitalWrite(led, HIGH);
        Break(speed);
    }

    digitalWrite(led, LOW);
    *state = 0;

} 
