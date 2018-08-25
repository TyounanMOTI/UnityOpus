#include "debug.h"

debug::debug(log_func func)
  : func(func)
{
}

void debug::log(const std::wstring& message)
{
  func(message.c_str());
}
