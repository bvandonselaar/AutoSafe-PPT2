autosafe_test_files = files(
    'protocol.c',
    'test.c',
)

check = dependency('check')

executable(
    'autosafe_test',
    autosafe_test_files,
    include_directories: inc_dir,
    link_with: [libautosafe],
    dependencies: [check]
)
