#include "FileDataAccess.h"
#include <fstream>
#include <sstream>
#include <cstdio>
#include <algorithm>

static const char* DI_HEADER = "# index\ttag\tvalue\n";
static const char* AI_HEADER = "# index\ttag\tvalue\n";

// -------------------- Base R/W --------------------

std::string FileDataAccess::ReadFromFile(const std::string& path) {
    std::ifstream file(path.c_str(), std::ios::in | std::ios::binary);
    if (!file.is_open()) return "";
    std::ostringstream buffer;
    buffer << file.rdbuf();
    return buffer.str();
}

bool FileDataAccess::WriteToFile(const std::string& path, const std::string& content) {
    // 간단한 안전성: tmp에 쓰고 교체
    std::string tmp = path + ".tmp";
    {
        std::ofstream f(tmp.c_str(), std::ios::out | std::ios::binary | std::ios::trunc);
        if (!f.is_open()) return false;
        f << content;
        if (!f.good()) return false;
    }
    // Windows의 rename은 덮어쓰기 실패하므로 기존 파일 삭제 후 rename
    std::remove(path.c_str());
    if (std::rename(tmp.c_str(), path.c_str()) != 0) {
        // 실패 시 tmp를 지우지 않고 남겨 디버깅 단서로 활용
        return false;
    }
    return true;
}

// -------------------- Utils --------------------

void FileDataAccess::Trim(std::string& s) {
    auto notspace = [](int ch) { return !std::isspace(ch); };
    s.erase(s.begin(), std::find_if(s.begin(), s.end(), notspace));
    s.erase(std::find_if(s.rbegin(), s.rend(), notspace).base(), s.end());
}

std::vector<std::string> FileDataAccess::SplitFields(const std::string& line) {
    // 탭 우선, 없다면 콤마 기준
    if (line.find('\t') != std::string::npos) {
        std::vector<std::string> out;
        std::stringstream ss(line);
        std::string f;
        while (std::getline(ss, f, '\t')) {
            Trim(f);
            out.push_back(f);
        }
        return out;
    }
    else {
        std::vector<std::string> out;
        std::stringstream ss(line);
        std::string f;
        while (std::getline(ss, f, ',')) {
            Trim(f);
            out.push_back(f);
        }
        return out;
    }
}

int FileDataAccess::FindDIByTag(const std::vector<DigitalInput>& v, const std::string& tag) {
    for (size_t i = 0; i < v.size(); ++i) if (v[i].tag == tag) return static_cast<int>(i);
    return -1;
}
int FileDataAccess::FindAIByTag(const std::vector<AnalogInput>& v, const std::string& tag) {
    for (size_t i = 0; i < v.size(); ++i) if (v[i].tag == tag) return static_cast<int>(i);
    return -1;
}

// -------------------- Ensure --------------------

bool FileDataAccess::EnsureFileExists(const std::string& path, bool isDI) {
    std::ifstream f(path.c_str());
    if (f.good()) return true;
    const char* hdr = isDI ? DI_HEADER : AI_HEADER;
    return WriteToFile(path, std::string(hdr));
}

// -------------------- Parse / Serialize --------------------

bool FileDataAccess::ParseDI(const std::string& text, std::vector<DigitalInput>& out) {
    out.clear();
    std::stringstream ss(text);
    std::string line;
    while (std::getline(ss, line)) {
        if (line.empty()) continue;
        std::string trimLine = line;
        Trim(trimLine);
        if (trimLine.empty() || trimLine[0] == '#') continue;

        auto fields = SplitFields(trimLine);
        if (fields.size() < 3) continue;

        DigitalInput di;
        di.index = std::atoi(fields[0].c_str());
        di.tag = fields[1];
        di.value = (fields[2] == "1" || fields[2] == "true" || fields[2] == "TRUE");
        out.push_back(di);
    }
    return true;
}

bool FileDataAccess::ParseAI(const std::string& text, std::vector<AnalogInput>& out) {
    out.clear();
    std::stringstream ss(text);
    std::string line;
    while (std::getline(ss, line)) {
        if (line.empty()) continue;
        std::string trimLine = line;
        Trim(trimLine);
        if (trimLine.empty() || trimLine[0] == '#') continue;

        auto fields = SplitFields(trimLine);
        if (fields.size() < 3) continue;

        AnalogInput ai;
        ai.index = std::atoi(fields[0].c_str());
        ai.tag = fields[1];
        ai.value = std::atoi(fields[2].c_str());
        out.push_back(ai);
    }
    return true;
}

std::string FileDataAccess::SerializeDI(const std::vector<DigitalInput>& v) {
    std::ostringstream ss;
    ss << DI_HEADER;
    for (auto& d : v) {
        ss << d.index << "\t" << (d.tag.empty() ? ("DI" + std::to_string(d.index)) : d.tag)
            << "\t" << (d.value ? 1 : 0) << "\n";
    }
    return ss.str();
}

std::string FileDataAccess::SerializeAI(const std::vector<AnalogInput>& v) {
    std::ostringstream ss;
    ss << AI_HEADER;
    for (auto& a : v) {
        ss << a.index << "\t" << (a.tag.empty() ? ("AI" + std::to_string(a.index)) : a.tag)
            << "\t" << a.value << "\n";
    }
    return ss.str();
}

// -------------------- Load / Save --------------------

static bool load_text_then_parse(const std::string& path, std::string& textOut) {
    textOut = FileDataAccess::ReadFromFile(path);
    if (textOut.empty()) {
        // 파일이 없거나 비어있으면 헤더만 생성 후 성공으로 간주
        return false;
    }
    return true;
}

bool FileDataAccess::LoadDI(const std::string& path, std::vector<DigitalInput>& out) {
    EnsureFileExists(path, true);
    std::string text;
    load_text_then_parse(path, text);
    return ParseDI(text, out);
}

bool FileDataAccess::SaveDI(const std::string& path, const std::vector<DigitalInput>& v) {
    return WriteToFile(path, SerializeDI(v));
}

bool FileDataAccess::LoadAI(const std::string& path, std::vector<AnalogInput>& out) {
    EnsureFileExists(path, false);
    std::string text;
    load_text_then_parse(path, text);
    return ParseAI(text, out);
}

bool FileDataAccess::SaveAI(const std::string& path, const std::vector<AnalogInput>& v) {
    return WriteToFile(path, SerializeAI(v));
}

// -------------------- Mutators --------------------

static bool set_di_impl(const std::string& path, int idx, const std::string* tag, bool newVal) {
    std::vector<DigitalInput> v;
    if (!FileDataAccess::LoadDI(path, v)) return false;
    int pos = -1;
    if (tag) {
        pos = FileDataAccess::FindDIByTag(v, *tag);
    }
    else {
        for (size_t i = 0; i < v.size(); ++i) if (v[i].index == idx) { pos = (int)i; break; }
    }
    if (pos < 0) return false;
    v[pos].value = newVal;
    return FileDataAccess::SaveDI(path, v);
}

static bool toggle_di_impl(const std::string& path, int idx, const std::string* tag) {
    std::vector<DigitalInput> v;
    if (!FileDataAccess::LoadDI(path, v)) return false;
    int pos = -1;
    if (tag) {
        pos = FileDataAccess::FindDIByTag(v, *tag);
    }
    else {
        for (size_t i = 0; i < v.size(); ++i) if (v[i].index == idx) { pos = (int)i; break; }
    }
    if (pos < 0) return false;
    v[pos].value = !v[pos].value;
    return FileDataAccess::SaveDI(path, v);
}

static bool set_ai_impl(const std::string& path, int idx, const std::string* tag, int newVal) {
    std::vector<AnalogInput> v;
    if (!FileDataAccess::LoadAI(path, v)) return false;
    int pos = -1;
    if (tag) {
        pos = FileDataAccess::FindAIByTag(v, *tag);
    }
    else {
        for (size_t i = 0; i < v.size(); ++i) if (v[i].index == idx) { pos = (int)i; break; }
    }
    if (pos < 0) return false;
    v[pos].value = newVal;
    return FileDataAccess::SaveAI(path, v);
}

bool FileDataAccess::SetDI(const std::string& path, int index, bool value) {
    return set_di_impl(path, index, nullptr, value);
}
bool FileDataAccess::SetDI(const std::string& path, const std::string& tag, bool value) {
    return set_di_impl(path, -1, &tag, value);
}
bool FileDataAccess::ToggleDI(const std::string& path, int index) {
    return toggle_di_impl(path, index, nullptr);
}
bool FileDataAccess::ToggleDI(const std::string& path, const std::string& tag) {
    return toggle_di_impl(path, -1, &tag);
}

bool FileDataAccess::SetAI(const std::string& path, int index, int value) {
    return set_ai_impl(path, index, nullptr, value);
}
bool FileDataAccess::SetAI(const std::string& path, const std::string& tag, int value) {
    return set_ai_impl(path, -1, &tag, value);
}
