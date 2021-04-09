using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace SpirvReflectSharp
{
	public class ShaderModule : IDisposable
	{
		/// <summary>
		/// The compiler that generated this SPIR-V module
		/// </summary>
		public ReflectGenerator Generator;

		public string EntryPointName;
		public uint EntryPointId;
		public uint EntryPointCount;

		//public ReflectEntryPoint EntryPoints;

		public SourceLanguage SourceLanguage;
		public uint SourceLanguageVersion;


		public ExecutionModel SPIRVExecutionModel;
		public ReflectShaderStage ShaderStage;

		public unsafe ReflectInterfaceVariable[] EnumerateInputVariables()
		{
			fixed (SpirvReflectNative.SpvReflectShaderModule* inmodule = &NativeShaderModule)
			{
				uint var_count = 0;
				var result = SpirvReflectNative.spvReflectEnumerateInputVariables(inmodule, &var_count, null);

				SpirvReflectUtils.Assert(result == SpirvReflectNative.SpvReflectResult.SPV_REFLECT_RESULT_SUCCESS);

				SpirvReflectNative.SpvReflectInterfaceVariable** input_vars =
					stackalloc SpirvReflectNative.SpvReflectInterfaceVariable*[(int)(var_count * sizeof(SpirvReflectNative.SpvReflectInterfaceVariable))];

				result = SpirvReflectNative.spvReflectEnumerateInputVariables(inmodule, &var_count, input_vars);
				SpirvReflectUtils.Assert(result == SpirvReflectNative.SpvReflectResult.SPV_REFLECT_RESULT_SUCCESS);

				// Convert to managed
				ReflectInterfaceVariable[] intf_vars = new ReflectInterfaceVariable[var_count];

				for (int i = 0; i < var_count; i++)
				{
					var interfaceVarNative = input_vars[i];
					var intf = *interfaceVarNative;
					ReflectInterfaceVariable variable = new ReflectInterfaceVariable();

					variable.name = new string(intf.name);
					variable.location = intf.location;
					variable.spirv_id = intf.spirv_id;
					variable.storage_class = (StorageClass)intf.storage_class;
					variable.format = (ReflectFormat)intf.format;
					variable.built_in = (BuiltIn)intf.built_in;

					intf_vars[i] = variable;
				}

				return intf_vars;
			}
		}

		public unsafe ReflectBlockVariable[] EnumeratePushConstants()
		{
			fixed (SpirvReflectNative.SpvReflectShaderModule* inmodule = &NativeShaderModule)
			{
				uint var_count = 0;
				var result = SpirvReflectNative.spvReflectEnumeratePushConstants(inmodule, &var_count, null);

				SpirvReflectUtils.Assert(result == SpirvReflectNative.SpvReflectResult.SPV_REFLECT_RESULT_SUCCESS);

				SpirvReflectNative.SpvReflectBlockVariable** push_consts =
					stackalloc SpirvReflectNative.SpvReflectBlockVariable*[(int)(var_count * sizeof(SpirvReflectNative.SpvReflectBlockVariable))];

				result = SpirvReflectNative.spvReflectEnumeratePushConstants(inmodule, &var_count, push_consts);
				SpirvReflectUtils.Assert(result == SpirvReflectNative.SpvReflectResult.SPV_REFLECT_RESULT_SUCCESS);

				// Convert to managed
				ReflectBlockVariable[] blockVars = new ReflectBlockVariable[var_count];

				return blockVars;
			}
		}

		#region Unmanaged

		internal unsafe ShaderModule(SpirvReflectNative.SpvReflectShaderModule module)
		{
			NativeShaderModule = module;

			// Convert to managed
			Generator = (ReflectGenerator)module.generator;
			EntryPointName = new string(module.entry_point_name);
			EntryPointId = module.entry_point_id;
			SourceLanguage = (SourceLanguage)module.source_language;
			SourceLanguageVersion = module.source_language_version;
			SPIRVExecutionModel = (ExecutionModel)module.spirv_execution_model;
			ShaderStage = (ReflectShaderStage)module.shader_stage;

			// Entry point extraction
			//this.EntryPointCount = module.entry_point_count;
			//this.EntryPoints = module.entr;
		}

		/// <summary>
		/// The native shader module
		/// </summary>
		public SpirvReflectNative.SpvReflectShaderModule NativeShaderModule;
		public bool Disposed = false;

		public unsafe void Dispose(bool freeManaged)
		{
			if (!Disposed)
			{
				fixed (SpirvReflectNative.SpvReflectShaderModule* inmodule = &NativeShaderModule)
				{
					SpirvReflectNative.spvReflectDestroyShaderModule(inmodule);
				}
				Disposed = true;
			}
		}

		public void Dispose()
		{
			Dispose(true);
		}

		~ShaderModule()
		{
			Dispose(false);
		}

		#endregion
	}
}
