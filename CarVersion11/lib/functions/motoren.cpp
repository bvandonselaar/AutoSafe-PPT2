/**
 * AutoSafe Firmware
 *
 * Copyright (c) 2018, AutoSafe, Inc.
 */

#include <stdio.h>
#include <Arduino.h>
#include <pins.h>

void MotorLeftForward(uint8_t* s){
if(s != NULL){
  analogWrite(M1_PWM, *s);
  digitalWrite(M1_Dir1, LOW);
  digitalWrite(M1_Dir2, HIGH);
  }
}

void MotorRightForward(uint8_t* s){
if(s != NULL){
  analogWrite(M2_PWM, *s);
  digitalWrite(M2_Dir1, LOW);
  digitalWrite(M2_Dir2, HIGH);
  }
}

void MotorLeftBackward(uint8_t* s){
if(s != NULL){
  analogWrite(M1_PWM, *s);
  digitalWrite(M1_Dir1, HIGH);
  digitalWrite(M1_Dir2, LOW);
  }
}

void MotorRightBackward(uint8_t* s){
if(s != NULL){
  analogWrite(M2_PWM, *s);
  digitalWrite(M2_Dir1, HIGH);
  digitalWrite(M2_Dir2, LOW);
  }
}
