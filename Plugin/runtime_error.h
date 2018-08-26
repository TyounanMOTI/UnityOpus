#pragma once

#include <stdexcept>
#include <string>

class runtime_error : public std::runtime_error
{
public:
  runtime_error(const std::wstring& message);
  std::wstring get_message() const;

private:
  std::wstring message;
};
