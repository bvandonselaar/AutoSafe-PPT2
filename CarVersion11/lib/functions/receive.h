/**
 * AutoSafe Firmware
 *
 * Copyright (c) 2018, AutoSafe, Inc.
 */

#ifndef _RECEIVE_H_
#define _RECEIVE_H_

#include <stdio.h>

/**
 * Checks if magic value is our magic value
 *
 * @param magic     Magic value, which is given to compare
 */
int checkMagic(uint16_t* magic);

/**
 * Checks if checksum value is correct
 *
 * @param packetChecksum    Checksum value where he will be written
 * @param expectedChecksum  Expected value of the checksum, is from packet
 * @param data              Packet with data
 * @param length            Length of data
 */
int checkChecksum(uint16_t* packetChecksum, uint16_t* expectedChecksum, uint8_t* data, uint8_t length);

/**
 * Collects all data to put in struct and make a message
 *
 * @param data      Packet with data
 * @param size      size of the data
 * @param Speed     speed of the car
 * @param state     Who is in control of the car
 */
int readMessage(uint8_t* data, size_t* size, uint8_t* Speed, uint8_t* state);

#endif
