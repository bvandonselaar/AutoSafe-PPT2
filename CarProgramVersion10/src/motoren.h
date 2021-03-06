

#ifndef _MOTOREN_H
#define _MOTOREN_H

#include <stdio.h>

/**
 * Methode for letting the left motor spinning forward
 *
 * @param motorPins Pins for controling the motors
 */
void MotorLeftForward(uint8_t* s);

/**
 * Methode for letting the right motor spinning forward
 *
 * @param motorPins Pins for controling the motors
 */
void MotorRightForward(uint8_t* s);

/**
 * Methode for letting the left motor spinning backward
 *
 * @param motorPins Pins for controling the motors
 */
void MotorLeftBackward(uint8_t* s);

/**
 * Methode for letting the right motor spinning backward
 *
 * @param motorPins Pins for controling the motors
 */
void MotorRightBackward(uint8_t* s);

#endif
