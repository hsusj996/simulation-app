#pragma once

#include <string>

class FileDataAccess {
public:
	std::string ReadFromFile(const std::string& path);

	bool WriteToFile(const std::string& path, const std::string& content);
};