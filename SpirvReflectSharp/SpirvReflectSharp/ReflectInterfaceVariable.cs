using System;
using System.Collections.Generic;
using System.Text;

namespace SpirvReflectSharp
{
	public struct ReflectInterfaceVariable
	{
		public uint spirv_id;
		public string name;
		public uint location;
		public StorageClass storage_class;
		public sbyte semantic;
		public ReflectDecoration decoration_flags;
		public BuiltIn built_in;
		//public ReflectNumericTraits numeric;
		//public ReflectArrayTraits array;
		public ReflectInterfaceVariable[] members;
		public ReflectFormat format;
		//public ReflectTypeDescription* type_description;

		public Anonymous_Struct_word_offset word_offset;
		public struct Anonymous_Struct_word_offset
		{
			public uint location;
		}
	}
}
