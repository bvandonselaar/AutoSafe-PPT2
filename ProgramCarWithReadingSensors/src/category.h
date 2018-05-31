/**
 * AutoSafe Firmware
 *
 * Copyright (c) 2018, AutoSafe, Inc.
 */

#ifndef _CATEGORY_H
#define _CATEGORY_H

#include <stdio.h>

struct location{
  uint8_t x;
  uint8_t y;
};

/**
 * Selects category and sends through to command
 *
 * @param packet    Packet with data that's come in
 * @param location  Struct with location where location has to be saved, if says in Command
 * @param motorPins Pins for controling the motors
 */
void SelectCategory(struct packet* packet,struct location* location, uint8_t* s);

/**
 * Category is 0xE0 so Control. Selects Command and execute
 *
 * @param packet    Packet with data that's come in
 * @param motorPins Pins for controling the motors
 */
void CatControl(struct packet* packet, uint8_t* s);

/**
 * Category is 0xC0 so Warning. Selects Command and execute
 *
 * @param packet    Packet with data that's come in
 * @param location  Struct with location where location has to be saved if is in Command
 * @param motorPins Pins for controling the motors
 */
void CatWarning(struct packet* packet, struct location* location, uint8_t* s);

/**
 * Category is 0xA0 so Location. Selects Command and execute
 *
 * @param packet    Packet with data that's come in
 * @param location  Struct with location where is has to be saved
 */
void CatLocation(struct packet* packet,struct location* location);

#endif
