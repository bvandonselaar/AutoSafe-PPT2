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
 * @param motorPins Pins for controling the motors
 */
void Forward(uint8_t* s);

/**
 * Methode for letting the car drive backward
 *
 * @param s         Speed
 * @param motorPins Pins for controling the motors
 */
void Backward(uint8_t* s);

/**
 * Methode for letting the car drive to the left
 *
 * @param s         Speed
 * @param motorPins Pins for controling the motors
 */
void Left(uint8_t* s);

/**
 * Methode for letting the car drive to the left
 *
 * @param s         Speed
 * @param motorPins Pins for controling the motors
 */
void Right(uint8_t* s);

/**
 * Methode for letting the car drive change for speed
 *
 * @param s         Speed
 * @param motorPins Pins for controling the motors
 */
void ChangeSpeed(uint8_t* s);

/**
 * Methode for letting the car make an emergency break
 *
 * @param motorPins Pins for controling the motors
 */
void Stop();

/**
 * Methode for letting the car break
 *
 * @param motorPins Pins for controling the motors
 */
void Break(uint8_t* s);

#endif
