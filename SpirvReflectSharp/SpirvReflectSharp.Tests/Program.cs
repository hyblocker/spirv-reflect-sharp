using System;
using System.IO;

namespace SpirvReflectSharp.Tests
{
	class Program
	{
		static void Main(string[] args)
		{
			byte[] shaderBytes = File.ReadAllBytes(@"E:\Data\Projects\Vessel\VesselSharp\spirv-reflect-sharp\frag.spv");
			SpirvReflect.ReflectCreateShaderModule(shaderBytes);


		}
	}
}
