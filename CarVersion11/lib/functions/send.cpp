/**
 * AutoSafe Firmware
 *
 * Copyright (c) 2018, AutoSafe, Inc.
 */

#include <send.h>
#include <Arduino.h>
#include <Wire.h>
#include <protocol.h>

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

int breakWarning(){

  struct packet pac;
  pac.magic = 0x0EE0;
  pac.length = 0x06;
  pac.category = 0x02;
  pac.command = 0x08;
  pac.payload = NULL;
  uint16_t checksum;
  uint8_t data[] = {0x0E, 0xE0, 0x06, 0x28};
  int check = fletcher16(&checksum, data, 4);
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
  //Wire.write(dataSerialize);
  return 0;
}

int emergencyBreakWarning(){

  struct packet pac;
  pac.magic = 0x0EE0;
  pac.length = 0x06;
  pac.category = 0x02;
  pac.command = 0x09;
  pac.payload = NULL;
  uint16_t checksum;
  uint8_t data[] = {0x0E, 0xE0, 0x06, 0x29};
  int check = fletcher16(&checksum, data, 4);
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
  //Wire.Write(dataSerialize);
  return 0;
}

int SOSmessage(){

  struct packet pac;
  pac.magic = 0x0EE0;
  pac.length = 0x06;
  pac.category = 0x02;
  pac.command = 0x0A;
  pac.payload = NULL;
  uint16_t checksum;
  uint8_t data[6] = {0x0E, 0xE0, 0x06, 0x2A};
  int check = fletcher16(&checksum, data, 4);
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
  //Wire.Write(dataSerialize);
  return 0;
}
