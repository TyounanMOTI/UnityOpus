#include "runtime_error.h"

runtime_error::runtime_error(const std::wstring & message)
  : std::runtime_error("Runtime error.")
  , message(message)
{
}

std::wstring runtime_error::get_message() const
{
  return message;
}
