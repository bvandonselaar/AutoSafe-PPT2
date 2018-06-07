/**
 * AutoSafe Firmware
 *
 * Copyright (c) 2018, AutoSafe, Inc.
 */

#include <stdio.h>
#include <Arduino.h>
#include <protocol.h>
#include <directions.h>

//0xE0
void CatControl(struct packet* packet, uint8_t* Speed){
  if(packet != NULL && Speed != NULL){
  if(packet->payload != NULL){
    uint8_t* payload_pp = packet->payload;
    uint8_t speed= *payload_pp;
    *Speed = speed;
  }
    switch(packet->command){
      //emergency break
      case 0x01:
        Stop();
      break;
      //forward
      case 0x02:
        Forward(Speed);
      break;
      //backward
      case 0x03:
        Backward(Speed);
      break;
      //left
      case 0x04:
        Left(Speed);
      break;
      //right
      case 0x05:
        Right(Speed);
      break;
      //change speed
      case 0x06:
        ChangeSpeed(Speed);
      break;
      //break
      case 0x07:
        Break(Speed);
      break;
      default: break;
    }
  }
}

//0xC0
void CatWarning(struct packet* packet, uint8_t* Speed){
  if(packet != NULL && Speed != NULL){
    if(packet->payload != NULL){
      uint8_t* payload_pp = packet->payload;
      uint8_t speed= *payload_pp;
      *Speed = speed;
    }
    switch(packet->command){
      //gets warning for break
      case 0x08:
        Break(Speed);
      break;
      //gets warning for emergency break
      case 0x09:
        Stop();
      break;
      //gets SOS message
      case 0x0A:

      break;
      /*//gets warning for dangerous situation
      case 0x0B:

      break;
      //gets do not pass warning
      case 0x0C:

      break;*/
      default: break;
    }
  }
}
/*
//0xA0
void CatLocation(struct packet* packet, struct location* previousLocation, struct location* location){
  if(packet != NULL && previousLocation != NULL && location != NULL){
    switch(packet->command){
      //gets location on intersection
      case 0x0D:

      break;
      //gets location from other car, who wants to pass
      case 0x0E:

      break;
      // gets own location
      case 0x01:
        *previousLocation = *location;
      break;
      default: break;
    }
  }
}
*/
void SelectCategory(struct packet* packet, uint8_t* Speed, uint8_t* state){
  if(packet != NULL && Speed != NULL && state != NULL){
    switch(packet->category){
      //category is control
      case 0x01:
      if(*state == 0){
        CatControl(packet, Speed);
      }
      break;
      //category is warning
      case 0x02:
        CatWarning(packet, Speed);
        &state = 1;
        digitalWrite(led, HIGH);
      break;
      /*//category is location
      case 0xA0:
        CatLocation(packet, previousLocation, location);
      break;*/
      default: break;
    }
  }
}
