#pragma once
#include <string>

#ifdef NATIVELIB_EXPORTS
#define NATIVELIB_API __declspec(dllexport)
#else
#define NATIVELIB_API __declspec(dllimport)
#endif

class NATIVELIB_API FileDataAccess {
public:
    std::string ReadFromFile(const std::string& path);
    bool WriteToFile(const std::string& path, const std::string& content);
};
