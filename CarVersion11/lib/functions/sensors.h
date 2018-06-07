/**
 * AutoSafe Firmware
 *
 * Copyright (c) 2018, AutoSafe, Inc.
 */

#ifndef _SENSORS_H
#define _SENSORS_H

#include <stdio.h>

/**
 * Methode for reading distance from ultrasone sensor
 *
 * @param ultrasonePin  Pins for reading ultrasone sensor
 * @param distance      Variable where the distance need to be saved
 */
void GetDistance(uint8_t* trigPin, uint8_t* echoPin, float* distance);

/**
 * Methode for reading infrarode sensor
 *
 * @param infrarodePin  Pins for reading infrarode sensor
 * @param value         Variable where the value need to be saved
 */
void ReadInfrarodeSensor(uint8_t* Infrarood, uint8_t* value);

void ReadAllSensors(float* distance1, float* distance2, uint8_t* InfraValue1,uint8_t* InfraValue2,uint8_t* InfraValue3,uint8_t* InfraValue4);


#endif
