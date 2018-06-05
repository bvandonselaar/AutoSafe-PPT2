/**
 * AutoSafe Firmware
 *
 * Copyright (c) 2018, AutoSafe, Inc.
 */

#include "send.h"
#include <Arduino.h>
#include "protocol.h"

int fletcher16(uint16_t* checksum, uint8_t* data, uint8_t length){
  if(checksum == NULL || data == NULL){
    return -1;
  }
  if(length < 6){
    return -1;
  }

  uint16_t sum1 = 0;
  uint16_t sum2 = 0;
  int index;

  for( index = 0; index < length; ++index )
  {
     sum1 = (sum1 + data[index]) % 255;
     sum2 = (sum2 + sum1) % 255;
  }

  *checksum = (sum2 << 8) | sum1;
  return 0;
}

int breakWarning(uint16_t *identifier){
  if(identifier == NULL){
    return -1;
  }
  struct packet pac;
  pac.magic = 0x0EE0;
  pac.length = 0x08;
  pac.category = 0x06;
  pac.command = 0x08;
  pac.identifier = *identifier;
  pac.payload = NULL;
  uint16_t checksum;
  uint8_t id1 = *identifier >> 8;
  uint8_t id2 = *identifier & 0xFF;
  uint8_t data[] = {0x0E, 0xE0, 0x08, 0xC8, id1, id2};
  int check = fletcher16(&checksum, data, 6);
  if(check == -1){
    return -1;
  }
  else{
    pac.checksum = checksum;
  }
  uint8_t dataSerialize[8];
  size_t size = 8;
  if(packet_serialize(&pac, dataSerialize, &size) == -1){
    return -1;
  }

  return 0;
}

int emergencyBreakWarning(uint16_t* identifier){
  if(identifier == NULL){
    return -1;
  }
  struct packet pac;
  pac.magic = 0x0EE0;
  pac.length = 0x08;
  pac.category = 0x06;
  pac.command = 0x09;
  pac.identifier = *identifier;
  pac.payload = NULL;
  uint16_t checksum;
  uint8_t id1 = *identifier >> 8;
  uint8_t id2 = *identifier & 0xFF;
  uint8_t data[] = {0x0E, 0xE0, 0x08, 0xC9, id1, id2};
  int check = fletcher16(&checksum, data, 6);
  if(check == -1){
    return -1;
  }
  else{
    pac.checksum = checksum;
  }
  uint8_t dataSerialize[8];
  size_t size = 8;
  if(packet_serialize(&pac, dataSerialize, &size) == -1){
    return -1;
  }
  return 0;
}

int SOSmessage(uint16_t* identifier){
  if(identifier == NULL){
    return -1;
  }
  struct packet pac;
  pac.magic = 0x0EE0;
  pac.length = 0x08;
  pac.category = 0x06;
  pac.command = 0x0A;
  pac.identifier = *identifier;
  pac.payload = NULL;
  uint16_t checksum;
  uint8_t id1 = *identifier >> 8;
  uint8_t id2 = *identifier & 0xFF;
  uint8_t data[6] = {0x0E, 0xE0, 0x08, 0xCA, id1, id2};
  int check = fletcher16(&checksum, data, 6);
  if(check == -1){
    return -1;
  }
  else{
    pac.checksum = checksum;
  }
  uint8_t dataSerialize[8];
  size_t size = 8;
  if(packet_serialize(&pac, dataSerialize, &size) == -1){
    return -1;
  }
  return 0;
}
