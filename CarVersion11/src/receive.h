/**
 * AutoSafe Firmware
 *
 * Copyright (c) 2018, AutoSafe, Inc.
 */

#ifndef _RECEIVE_H_
#define _RECEIVE_H_

#include <stdio.h>

int checkMagic(uint16_t* magic);

int checkChecksum(uint16_t* packetChecksum, uint16_t* expectedChecksum, uint8_t* data, uint8_t length);

int readMessage(uint8_t* data, size_t* size);

#endif
