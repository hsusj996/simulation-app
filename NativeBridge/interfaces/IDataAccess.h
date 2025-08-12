#pragma once

namespace NativeBridge {
	namespace Interfaces {
		public interface class IDataAccess {
			System::String^ ReadFromFile(System::String^ path);
			bool WriteToFile(System::String^ path, System::String^ content);
		};
	}
}
