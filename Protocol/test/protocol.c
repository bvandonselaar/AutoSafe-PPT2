/**
 * AutoSafe Firmware
 *
 * Copyright (c) 2018, AutoSafe, Inc.
 */

#include <stdio.h>
#include <stdlib.h>

#include <autosafe.h>
#include <check.h>

START_TEST(test_serialize_proper_packet)
{
    uint8_t payload[] = { 0x1, 0x2, 0x3 };

    struct packet pkt = {
        .magic = 0x0EE0,
        .length = 0x9,
        .category = 0b001,
        .command = 0b00010,
        .payload = payload,
        .checksum = 0x42E5
    };

    uint8_t* data = malloc(pkt.length);
    size_t size = 0;

    ck_assert_int_eq(packet_serialize(&pkt, data, &size), 0);
}
END_TEST

START_TEST(test_serialize_invalid_arguments)
{
    struct packet pkt = { 0 };
    uint8_t data = 0;
    size_t size = 0;

    ck_assert_int_eq(packet_serialize(NULL, &data, &size), -1);
    ck_assert_int_eq(packet_serialize(&pkt, NULL, &size), -1);
    ck_assert_int_eq(packet_serialize(&pkt, &data, NULL), -1);
}
END_TEST

START_TEST(test_deserialize_invalid_arguments)
{
    struct packet pkt = { 0 };
    uint8_t data = 0;
    size_t size = 0;

    ck_assert_int_eq(packet_deserialize(NULL, &data, size), -1);
    ck_assert_int_eq(packet_deserialize(&pkt, NULL, size), -1);
}
END_TEST

START_TEST(test_deserialize_proper_data)
{
    struct packet* pkt = malloc(sizeof(struct packet));
    uint8_t data[] = { 0x0E, 0xE0, 0x9, 0b00100010, 0x1, 0x2, 0x3, 0x42, 0xE5 };
    size_t size = 9;

    ck_assert_int_eq(packet_deserialize(pkt, data, size), 0);
    ck_assert_int_eq(pkt->magic, 0x0EE0);
    ck_assert_int_eq(pkt->length, 0x9);
    ck_assert_int_eq(pkt->category, 1);
    ck_assert_int_eq(pkt->command, 2);
    ck_assert(pkt->payload != NULL);
    ck_assert_int_eq(pkt->payload[0], 0x1);
    ck_assert_int_eq(pkt->payload[1], 0x2);
    ck_assert_int_eq(pkt->payload[2], 0x3);
    ck_assert_int_eq(pkt->checksum, 0x42E5);
}
END_TEST

Suite *create_protocol_suite(void)
{
    Suite *suite = suite_create("Protocol");
    TCase *chain = tcase_create("protocol tests");

    suite_add_tcase(suite, chain);

    tcase_add_test(chain, test_serialize_proper_packet);
    tcase_add_test(chain, test_serialize_invalid_arguments);
    tcase_add_test(chain, test_deserialize_invalid_arguments);
    tcase_add_test(chain, test_deserialize_proper_data);

    return suite;
}
