project(
    'autosafe',
    'c',
    license: 'MIT',
    version: '0.0.1'
)

add_project_arguments(
    '-Wall',
    '-Wextra',
    '-Werror',
    '-std=c99',
    '-Wno-unused-variable',
    '-Wno-maybe-uninitialized',
    language: 'c'
)

inc_dir = include_directories('src')

subdir('src')
subdir('test')
