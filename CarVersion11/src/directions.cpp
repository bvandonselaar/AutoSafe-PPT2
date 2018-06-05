/**
 * AutoSafe Firmware
 *
 * Copyright (c) 2018, AutoSafe, Inc.
 */

#include <stdio.h>
#include <Arduino.h>
#include "motoren.h"
#include "pins.h"

void Forward(uint8_t* s){
if(s != NULL){
  MotorLeftForward(s);
  MotorRightForward(s);
  }
}

void Backward(uint8_t* s){
if(s != NULL){
  MotorLeftBackward(s);
  MotorRightBackward(s);
  }
}

void Left(uint8_t *s){
if(s != NULL){
  MotorLeftBackward(s);
  MotorRightForward(s);
  }
}

void Right(uint8_t* s){
if(s != NULL){
  MotorLeftForward(s);
  MotorRightBackward(s);
  }
}

void ChangeSpeed(uint8_t* s){
  if(s != NULL){
    analogWrite(M1_PWM, *s);
    analogWrite(M2_PWM, *s);
  }
}

void Stop(){
  digitalWrite(M2_Dir1, LOW);
  digitalWrite(M2_Dir2, LOW);
  digitalWrite(M1_Dir1, LOW);
  digitalWrite(M1_Dir2, LOW);
}

void Break(uint8_t* s){
  if(s != NULL){
    for(int i = 0; i<15; i++){
      *s = *s - 15;
      ChangeSpeed(s);
    }
    Stop();
  }
}
