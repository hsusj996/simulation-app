#include "FileDataAccess.h"
#include <fstream>
#include <sstream>

std::string FileDataAccess::ReadFromFile(const std::string& path) {
	std::ifstream file(path);
	if (!file.is_open()) return "";

	std::stringstream buffer;
	buffer << file.rdbuf();
	return buffer.str();
}

bool FileDataAccess::WriteToFile(const std::string& path, const std::string& content) {
	std::ofstream file(path);
	if (!file.is_open()) return false;

	file << content;
	return true;
}