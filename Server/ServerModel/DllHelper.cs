using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ServerModel
{
	public static class DllHelper
	{
		public static Assembly GetHotfixAssembly()
		{
			byte[] dllBytes = File.ReadAllBytes("./ServerHotfix.dll");
			byte[] pdbBytes = File.ReadAllBytes("./ServerHotfix.pdb");
			Assembly assembly = Assembly.Load(dllBytes, pdbBytes);
			return assembly;
		}
	}
}
