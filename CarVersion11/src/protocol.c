/**
 * AutoSafe Firmware
 *
 * Copyright (c) 2018, AutoSafe, Inc.
 */

#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#include "protocol.h"

int packet_serialize(struct packet* packet, uint8_t* data, size_t* size)
{
    if (packet == NULL || data == NULL) {
        return -1;
    }
    if(packet->length > 0 && packet->payload == NULL){
      return -1;
    }

    size_t i = 0;

    // Magic
    data[i++] = packet->magic >> 8;
    data[i++] = packet->magic & 0xFF;

    // Length
    data[i++] = packet->length;

    // Command
    data[i++] = (packet->category << 5) | packet->command;

    // Identifier
    data[i++] = packet->identifier >> 8;
    data[i++] = packet->identifier & 0xFF;

    // Payload
    if (packet->length > 0) {
        memcpy(&data[i], packet->payload, packet->length - PACKET_FIXED);
        i += packet->length - PACKET_FIXED;
    }

    *size = i;

    return 0;
}

int packet_deserialize(struct packet* packet, const uint8_t* data, const size_t size)
{
    if (packet == NULL || data == NULL) {
        return -1;
    }

    if (size < PACKET_FIXED) {
        return -1;
    }

    size_t i = 0;

    // Magic
    packet->magic = data[i] << 8 | data[i + 1];
    i += 2;

    // Length
    packet->length = data[i++];

    if (packet->length != size) {
        return -1;
    }

    // Category + command
    packet->category = data[i] >> 5;
    packet->command = data[i++] & 0b00011111;

    // Identifier
    packet->identifier = data[i+1] << 8 | data[i+1];

    // Payload
    packet->payload = malloc(packet->length - PACKET_FIXED);

    if (packet->payload == NULL) {
        return -1;
    }

    memcpy(packet->payload, &data[i], packet->length - PACKET_FIXED);
    i += packet->length - PACKET_FIXED;

    // Checksum
    packet->checksum = data[i] << 8 | data[i + 1];

    return 0;
}

void packet_free(struct packet* packet)
{
    if (packet != NULL) {
        if (packet->payload != NULL) {
            free(packet->payload);
        }

        free(packet);
    }
}
