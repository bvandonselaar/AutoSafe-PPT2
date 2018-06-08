

#ifndef _MOTOREN_H
#define _MOTOREN_H

#include <stdio.h>

/**
 * Methode for letting the left motor spinning forward
 *
 * @param s      speed of the car
 */
void MotorLeftForward(uint8_t* s);

/**
 * Methode for letting the right motor spinning forward
 *
 * @param s      speed of the car
 */
void MotorRightForward(uint8_t* s);

/**
 * Methode for letting the left motor spinning backward
 *
 * @param s      speed of the car
 */
void MotorLeftBackward(uint8_t* s);

/**
 * Methode for letting the right motor spinning backward
 *
 * @param s      speed of the car
 */
void MotorRightBackward(uint8_t* s);

#endif
