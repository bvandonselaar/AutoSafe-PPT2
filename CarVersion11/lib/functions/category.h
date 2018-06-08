/**
 * AutoSafe Firmware
 *
 * Copyright (c) 2018, AutoSafe, Inc.
 */

#ifndef _CATEGORY_H
#define _CATEGORY_H

#include <stdio.h>

/**
 * Selects category and sends through to command
 *
 * @param packet    Packet with data that's come in
 * @param Speed     speed of the car
 * @param state     Who is in control of the car
 */
void SelectCategory(struct packet* packet, uint8_t* Speed, uint8_t* state);

/**
 * Category is 0xE0 so Control. Selects Command and execute
 *
 * @param packet    Packet with data that's come in
 * @param Speed     speed of the car
 */
void CatControl(struct packet* packet, uint8_t* Speed);

/**
 * Category is 0xC0 so Warning. Selects Command and execute
 *
 * @param packet    Packet with data that's come in
 * @param Speed     speed of the car
 */
void CatWarning(struct packet* packet, uint8_t* Speed);

/**
 * Category is 0xA0 so Location. Selects Command and execute
 *
 * @param packet    Packet with data that's come in
 * @param Speed     speed of the car

void CatLocation(struct packet* packet,struct location* previousLocation, struct location* location);
*/
#endif
