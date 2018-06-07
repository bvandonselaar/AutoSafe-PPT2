/**
 * AutoSafe Firmware
 *
 * Copyright (c) 2018, AutoSafe, Inc.
 */

#include "receive.h"
#include <stdio.h>
#include <Wire.h>
#include "protocol.h"
#include "send.h"
#include "category.h"

int checkMagic(uint16_t* magic){
  if(magic == NULL){
    return -1;
  }
  else{
    if(*magic == 0x0EE0){
      return 0;
    }
    return -1;
  }
}

int checkChecksum(uint16_t* packetChecksum, uint8_t* data, uint8_t length){
  uint16_t expectedChecksum;
  if(fletcher16(&expectedChecksum, data, length) == 0){
    if(*packetChecksum == expectedChecksum){
      return 0;
    }
  }
  else{
    return -1;
  }
}

int readMessage(uint8_t* data, size_t size, uint8_t* Speed){
  if(data == NULL ||  Speed == NULL){
    return -1;
  }
  struct packet pac = {};
  if(packet_deserialize(&pac, data, size) == -1){
    return -1;
  }
  if(checkMagic(&pac.magic) == -1){
    return -1;
  }
  uint8_t length = pac.length;
  if(checkChecksum(&pac.checksum, data, length) == -1){
    return -1;
  }
  SelectCategory(&pac, Speed);
  return 0;
}
