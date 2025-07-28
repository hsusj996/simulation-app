#pragma once
using namespace System;

namespace NativeBridge {
	namespace Interfaces {
		public interface class IDataAccess {
			String^ ReadFromFile(String^ path);
			bool WriteToFile(String^ path, String^ content);
		};
	}
}
