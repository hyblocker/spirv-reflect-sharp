using System;

namespace SpirvReflectSharp
{
	public unsafe class SpirvReflect
	{
		/// <summary>
		/// Creates a <see cref="ShaderModule"/> given SPIR-V bytecode
		/// </summary>
		/// <param name="shaderBytes">Compiled SPIR-V bytecode</param>
		/// <returns>A <see cref="ShaderModule"/></returns>
		public static ShaderModule ReflectCreateShaderModule(byte[] shaderBytes) {
			fixed (void* shdrBytecode = &shaderBytes[0])
			{
				SpirvReflectNative.SpvReflectShaderModule module;
				SpirvReflectNative.SpvReflectResult result =
					SpirvReflectNative.spvReflectCreateShaderModule(Convert.ToUInt64(shaderBytes.LongLength), shdrBytecode, &module);

				if (result == SpirvReflectNative.SpvReflectResult.SPV_REFLECT_RESULT_SUCCESS)
				{
					return new ShaderModule(module);
				}
				else
				{
					throw new SpirvReflectException(result);
				}
			}
		}
	}
}
