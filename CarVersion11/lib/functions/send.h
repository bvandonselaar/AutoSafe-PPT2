/**
 * AutoSafe Firmware
 *
 * Copyright (c) 2018, AutoSafe, Inc.
 */

#ifndef _SEND_H_
#define _SEND_H_

#include <stdio.h>

int fletcher16(uint16_t* checksum, uint8_t* data, uint8_t length);

int breakWarning(uint16_t* identifier);

int emergencyBreakWarning(uint16_t* identifier);

int SOSmessage(uint16_t* identifier);

#endif
