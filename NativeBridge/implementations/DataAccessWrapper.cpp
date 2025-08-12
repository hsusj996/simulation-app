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
			std::string nativePath = marshal_as<std::string>(path);	//c#���� �Ķ���� �Է¹޾� c++�� ��ȯ
			std::string result = nativeAccess->ReadFromFile(nativePath);

			return gcnew System::String(result.c_str());	//c++ ����� c#���� ��ȯ
		}

		bool DataAccessWrapper::WriteToFile(System::String^ path, System::String^ content) {
			std::string nativePath = marshal_as<std::string>(path);
			std::string nativeContent = marshal_as<std::string>(content);

			return nativeAccess->WriteToFile(nativePath, nativeContent);
		}

	}
}