/**
 * AutoSafe Firmware
 *
 * Copyright (c) 2018, AutoSafe, Inc.
 */

 #ifndef _LOCATION_H
 #define _LOCATION_H

 #include <stdio.h>

 struct location{
   uint16_t x;
   uint16_t y;
 };

 /**
  * Compare two locations
  *
  * @param previousLocation   Struct previous location
  * @param location           Struct new location
  *
  * @return 1 when direction is Forward, 0 when first location is bigger, -1 when failed.
  */
int getDirection(struct location* previousLocation, struct location* location);

 /**
  * Compare two locations
  *
  * @param location1  Struct first location
  * @param location2  Struct second location
  *
  * @return 1 when second location is bigger, 0 when first location is bigger, -1 when failed.
  */
int compareLocation(struct location* location1, struct location* location2);

#endif
