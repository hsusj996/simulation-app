#include "DataAccessWrapper.h"

using namespace msclr::interop;

namespace NativeBridge {
	namespace Implementations {
		DataAccessWrapper::DataAccessWrapper() {
			nativeAccess = new FileDataAccess();
		}

		DataAccessWrapper::~DataAccessWrapper() {
			this->!DataAccessWrapper();
		}

		DataAccessWrapper::!DataAccessWrapper() {
			delete nativeAccess;
		}

		System::String^ DataAccessWrapper::ReadFromFile(System::String^ path) {
			std::string nativePath = marshal_as<std::string>(path);	//c#에서 파라미터 입력받아 c++로 전환
			std::string result = nativeAccess->ReadFromFile(nativePath);

			return gcnew System::String(result.c_str());	//c++ 결과를 c#으로 전환
		}

		bool DataAccessWrapper::WriteToFile(System::String^ path, System::String^ content) {
			std::string nativePath = marshal_as<std::string>(path);
			std::string nativeContent = marshal_as<std::string>(content);

			return nativeAccess->WriteToFile(nativePath, nativeContent);
		}

	}
}