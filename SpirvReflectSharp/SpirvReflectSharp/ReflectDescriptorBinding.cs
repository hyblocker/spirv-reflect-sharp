using System;
using System.Collections.Generic;
using System.Text;

namespace SpirvReflectSharp
{
	public struct ReflectDescriptorBinding
	{
		public uint SpirvId;
		public string Name;
		public uint Binding;
		public uint InputAttachmentIndex;
		public uint Set;
		public ReflectDescriptorType DescriptorType;
		public ReflectResourceType ResourceType;
		public ReflectImageTraits Image;
		public ReflectBlockVariable Block;
		//public ReflectBindingArrayTraits Array;
		public uint Count;
		public uint Accessed;
		public uint UavCounterId;
		//public ReflectDescriptorBinding* UavCounterBinding;
		//public ReflectTypeDescription* TypeDescription;
		//public Anonymous_Struct_word_offset WordOffset;
		
		public struct Anonymous_Struct_word_offset
		{
			public uint Binding;
			public uint Set;
		}
	}
}
