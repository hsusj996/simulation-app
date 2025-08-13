#pragma once
#include <string>
#include <vector>

#ifdef NATIVELIB_EXPORTS
#define NATIVELIB_API __declspec(dllexport)
#else
#define NATIVELIB_API __declspec(dllimport)
#endif

struct DigitalInput {
    int index = -1;
    std::string tag;
    bool value = false;
};

struct AnalogInput {
    int index = -1;
    std::string tag;
    int value = 0;
};

class NATIVELIB_API FileDataAccess {
public:
    // 원본 베이스 (문자열 단위 R/W)
    static std::string ReadFromFile(const std::string& path);
    static bool WriteToFile(const std::string& path, const std::string& content);

    // 파일이 없으면 헤더만 생성
    static bool EnsureFileExists(const std::string& path, bool isDI);

    // 전체 로드/세이브
    static bool LoadDI(const std::string& path, std::vector<DigitalInput>& out);
    static bool SaveDI(const std::string& path, const std::vector<DigitalInput>& v);

    static bool LoadAI(const std::string& path, std::vector<AnalogInput>& out);
    static bool SaveAI(const std::string& path, const std::vector<AnalogInput>& v);

    // 수정 API (index 또는 tag 기준)
    static bool SetDI(const std::string& path, int index, bool value);
    static bool SetDI(const std::string& path, const std::string& tag, bool value);
    static bool ToggleDI(const std::string& path, int index);
    static bool ToggleDI(const std::string& path, const std::string& tag);

    static bool SetAI(const std::string& path, int index, int value);
    static bool SetAI(const std::string& path, const std::string& tag, int value);

    // 파싱/직렬화
    static bool ParseDI(const std::string& text, std::vector<DigitalInput>& out);
    static bool ParseAI(const std::string& text, std::vector<AnalogInput>& out);
    static std::string SerializeDI(const std::vector<DigitalInput>& v);
    static std::string SerializeAI(const std::vector<AnalogInput>& v);

    // 유틸
    static void Trim(std::string& s);
    static std::vector<std::string> SplitFields(const std::string& line); // 탭/콤마 모두 지원
    static int FindDIByTag(const std::vector<DigitalInput>& v, const std::string& tag);
    static int FindAIByTag(const std::vector<AnalogInput>& v, const std::string& tag);
};
