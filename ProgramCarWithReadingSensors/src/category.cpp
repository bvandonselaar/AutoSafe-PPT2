/**
 * AutoSafe Firmware
 *
 * Copyright (c) 2018, AutoSafe, Inc.
 */

#include <stdio.h>
#include <Arduino.h>
#include "protocol.h"
#include "directions.h"


//0xE0
void CatControl(struct packet* packet, uint8_t* s){
  if(packet != NULL && s != NULL){
    switch(packet->command){
      //emergency break
      case 0x01:
        Stop();
      break;
      //forward
      case 0x02:
        Forward(s);
      break;
      //backward
      case 0x03:
        Backward(s);
      break;
      //left
      case 0x04:
        Left(s);
      break;
      //right
      case 0x05:
        Right(s);
      break;
      //change speed
      case 0x06:
        ChangeSpeed(s);
      break;
      //break
      case 0x07:
        Break(s);
      break;
      default: break;
    }
  }
}

//0xC0
void CatWarning(struct packet* packet, uint8_t* s){
  if(packet != NULL && s != NULL){
    switch(packet->command){
      //gets warning for break
      case 0x08:
        Break(s);
      break;
      //gets warning for emergency break
      case 0x09:
        Stop();
      break;
      //gets SOS message
      case 0x0A:

      break;
      //gets warning for dangerous situation
      case 0x0B:

      break;
      //gets do not pass warning
      case 0x0C:

      break;
      default: break;
    }
  }
}

//0xA0
void CatLocation(struct packet* packet, struct location* location){
  if(packet != NULL && location != NULL){
    switch(packet->command){
      //gets location on intersection
      case 0x0D:

      break;
      //gets location from other car, who wants to pass
      case 0x0E:

      break;
      // gets own location
      case 0x01:

      break;
      default: break;
    }
  }
}

void SelectCategory(struct packet* packet, struct location* location, uint8_t* s){
  if(packet != NULL && location != NULL && s != NULL){
    switch(packet->category){
      //category is control
      case 0xE0:
        CatControl(packet, s);
      break;
      //category is warning
      case 0xC0:
        CatWarning(packet, s);
      break;
      //category is location
      case 0xA0:
        CatLocation(packet, location);
      break;
      default: break;
    }
  }
}
