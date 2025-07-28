#include "FileDataAccess.h"
#include <iostream>

int main() {
    FileDataAccess access;

    access.WriteToFile("test.txt", "Hello from C++!");
    std::string data = access.ReadFromFile("test.txt");

    std::cout << "Read: " << data << std::endl;
    return 0;
}
