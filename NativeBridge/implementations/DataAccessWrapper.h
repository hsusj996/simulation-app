#pragma once
#include <msclr/marshal_cppstd.h>
#include "../interfaces/IDataAccess.h"
#include "../../NativeLib/FileDataAccess.h"

namespace NativeBridge {
	namespace Implementations {
		public ref class DataAccessWrapper : public NativeBridge::Interfaces::IDataAccess {
		private:
			FileDataAccess* nativeAccess;

		public:
			DataAccessWrapper();
			~DataAccessWrapper();
			!DataAccessWrapper();

			virtual System::String^ ReadFromFile(System::String^ path);
			virtual bool WriteToFile(System::String^ path, System::String^ content);
		};
	}
}