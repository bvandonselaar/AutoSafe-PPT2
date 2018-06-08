/**
 * AutoSafe Firmware
 *
 * Copyright (c) 2018, AutoSafe, Inc.
 */

#ifndef _SEND_H_
#define _SEND_H_

#include <stdio.h>

/**
 * The algorithm of the checksum
 *
 * @param checksum    Where the value will be written
 * @param data        packet with data
 * @param length      length of data
 */
int fletcher16(uint16_t* checksum, uint8_t* data, uint8_t length);

/**
 * Sends a breaking warning
 */
int breakWarning();

/**
 * Sends an emergency warning
 */
int emergencyBreakWarning();

/**
 * Sends a SOS warning, when the button is pressed
 */
int SOSmessage();

#endif
