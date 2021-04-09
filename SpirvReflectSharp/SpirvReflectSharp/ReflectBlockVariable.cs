using System;
using System.Collections.Generic;
using System.Text;

namespace SpirvReflectSharp
{
	public struct ReflectBlockVariable
	{
		public uint spirv_id;
		public sbyte name;
		public uint offset;
		public uint absolute_offset;
		public uint size;
		public uint padded_size;
		public ReflectDecoration decoration_flags;
		//public ReflectNumericTraits numeric;
		//public ReflectArrayTraits array;
		//public ReflectVariableFlags flags;
		public uint member_count;
		public ReflectBlockVariable[] members;
		//public ReflectTypeDescription* type_description;
	}
}
