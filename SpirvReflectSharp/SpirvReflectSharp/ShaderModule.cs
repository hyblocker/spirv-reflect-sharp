using System;
using System.Collections.Generic;
using System.Text;

namespace SpirvReflectSharp
{
	public class ShaderModule
	{
		/// <summary>
		/// The compiler that generated this SPIR-V module
		/// </summary>
		public Generator Generator;

		public uint EntryPointId;
		public uint EntryPointCount;
	}

	public struct ReflectInterfaceVariable
	{
		public uint SpirvId;
		public uint Location;
		public string Name;
		public sbyte Semantic;

	}
}
