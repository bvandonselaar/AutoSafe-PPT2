/**
 * AutoSafe Firmware
 *
 * Copyright (c) 2018, AutoSafe, Inc.
 */

#include <check.h>

extern Suite *create_protocol_suite(void);

int main(void)
{
    SRunner *runner = srunner_create(create_protocol_suite());

    srunner_run_all(runner, CK_NORMAL);

    int failed = srunner_ntests_failed(runner);

    srunner_free(runner);

    return failed == 0 ? 0 : 1;
}
