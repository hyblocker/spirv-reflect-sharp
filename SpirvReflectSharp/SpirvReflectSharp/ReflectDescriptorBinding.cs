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
		public ReflectBindingArrayTraits Array;
		public uint Count;
		public uint Accessed;
		public uint UavCounterId;
		// Removed because recursive struct :/
		//public ReflectDescriptorBinding UavCounterBinding;
		public ReflectTypeDescription TypeDescription;

		public SpirvReflectNative.SpvReflectDescriptorBinding Native;

		public override string ToString()
		{
			return "ReflectDescriptorBinding {" + Name + "; Type: " + DescriptorType + "}";
		}

		internal unsafe ReflectDescriptorBinding(SpirvReflectNative.SpvReflectDescriptorBinding binding)
		{
			Native = binding;

			Set = binding.set;
			Accessed = binding.accessed;
			Name = new string(binding.name);
			Binding = binding.binding;
			SpirvId = binding.spirv_id;
			Count = binding.count;
			ResourceType = (ReflectResourceType)binding.resource_type;
			UavCounterId = binding.uav_counter_id;
			InputAttachmentIndex = binding.input_attachment_index;
			Image = new ReflectImageTraits(binding.image);
			Array = new ReflectBindingArrayTraits(binding.array);
			DescriptorType = (ReflectDescriptorType)binding.descriptor_type;
			Block = new ReflectBlockVariable();
			ReflectBlockVariable.PopulateReflectBlockVariable(ref binding.block, ref Block);
			TypeDescription = ReflectTypeDescription.GetManaged(ref *binding.type_description);

			//UavCounterBinding = new ReflectDescriptorBinding(*binding.uav_counter_binding);
		}
	}
}
