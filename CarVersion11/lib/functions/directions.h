/**
 * AutoSafe Firmware
 *
 * Copyright (c) 2018, AutoSafe, Inc.
 */

#ifndef _DIRECTIONS_H
#define _DIRECTIONS_H

#include <stdio.h>

/**
 * Methode for letting the car drive forward
 *
 * @param s         Speed
 */
void Forward(uint8_t* s);

/**
 * Methode for letting the car drive backward
 *
 * @param s         Speed
 */
void Backward(uint8_t* s);

/**
 * Methode for letting the car drive to the left
 *
 * @param s         Speed
 */
void Left(uint8_t* s);

/**
 * Methode for letting the car drive to the right
 *
 * @param s         Speed
 */
void Right(uint8_t* s);

/**
 * Methode for letting the car drive change for speed
 *
 * @param s         Speed
 */
void ChangeSpeed(uint8_t* s);

/**
 * Methode for letting the car make an emergency break
 *
 */
void Stop();

/**
 * Methode for letting the car break
 *
 * @param s      speed of the car
 */
void Break(uint8_t* s);

#endif
