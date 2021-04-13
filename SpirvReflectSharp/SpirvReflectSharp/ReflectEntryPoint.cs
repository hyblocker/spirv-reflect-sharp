namespace SpirvReflectSharp
{
	public struct ReflectEntryPoint
	{
		public string Name;
		public uint Id;
		public ExecutionModel SpirvExecutionModel;
		public ReflectShaderStage ShaderStage;
		public ReflectDescriptorSet[] DescriptorSets;
		public uint[] UsedUniforms;
		public uint[] UsedPushConstants;
	}
}
