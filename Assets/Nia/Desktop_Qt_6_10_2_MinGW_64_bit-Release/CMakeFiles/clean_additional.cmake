# Additional clean files
cmake_minimum_required(VERSION 3.16)

if("${CONFIG}" STREQUAL "" OR "${CONFIG}" STREQUAL "Release")
  file(REMOVE_RECURSE
  "CMakeFiles\\popup_autogen.dir\\AutogenUsed.txt"
  "CMakeFiles\\popup_autogen.dir\\ParseCache.txt"
  "popup_autogen"
  )
endif()
