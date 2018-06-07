/**
 * AutoSafe Firmware
 *
 * Copyright (c) 2018, AutoSafe, Inc.
 */

#ifndef _PINS_H_
#define _PINS_H_

#include <stdint.h>

#define M1_PWM   6      //MOTOR 1
#define M1_Dir1  7
#define M1_Dir2  8

#define M2_PWM   11     //MOTOR 2
#define M2_Dir1  10
#define M2_Dir2  9

#define U1_trigPin  2   //ultrasone sensor 1
#define U1_echoPin  4

/*#define U2_trigPin  12  //ultrasone sensor 2
#define U2_echoPin  13*/

#define Infrarood1  0   //infrarode sensoren
#define Infrarood2  1
#define Infrarood3  2
#define Infrarood4  3

#define ButtonPin 12    //SOS knop
#define led 13 //warning led

#endif
