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
		public ReflectEntryPoint[] EntryPoints;

		public SourceLanguage SourceLanguage;
		public uint SourceLanguageVersion;

		public string SourceFile;
		public string SourceSource;

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
				return ReflectInterfaceVariable.ToManaged(input_vars, var_count);
			}
		}
		
		public unsafe ReflectInterfaceVariable[] EnumerateOutputVariables()
		{
			fixed (SpirvReflectNative.SpvReflectShaderModule* inmodule = &NativeShaderModule)
			{
				uint var_count = 0;
				var result = SpirvReflectNative.spvReflectEnumerateOutputVariables(inmodule, &var_count, null);

				SpirvReflectUtils.Assert(result == SpirvReflectNative.SpvReflectResult.SPV_REFLECT_RESULT_SUCCESS);

				SpirvReflectNative.SpvReflectInterfaceVariable** output_vars =
					stackalloc SpirvReflectNative.SpvReflectInterfaceVariable*[(int)(var_count * sizeof(SpirvReflectNative.SpvReflectInterfaceVariable))];

				result = SpirvReflectNative.spvReflectEnumerateOutputVariables(inmodule, &var_count, output_vars);
				SpirvReflectUtils.Assert(result == SpirvReflectNative.SpvReflectResult.SPV_REFLECT_RESULT_SUCCESS);

				// Convert to managed
				return ReflectInterfaceVariable.ToManaged(output_vars, var_count);
			}
		}
		
		public unsafe ReflectInterfaceVariable[] EnumerateInterfaceVariables()
		{
			fixed (SpirvReflectNative.SpvReflectShaderModule* inmodule = &NativeShaderModule)
			{
				uint var_count = 0;
				var result = SpirvReflectNative.spvReflectEnumerateInterfaceVariables(inmodule, &var_count, null);

				SpirvReflectUtils.Assert(result == SpirvReflectNative.SpvReflectResult.SPV_REFLECT_RESULT_SUCCESS);

				SpirvReflectNative.SpvReflectInterfaceVariable** interface_vars =
					stackalloc SpirvReflectNative.SpvReflectInterfaceVariable*[(int)(var_count * sizeof(SpirvReflectNative.SpvReflectInterfaceVariable))];

				result = SpirvReflectNative.spvReflectEnumerateInterfaceVariables(inmodule, &var_count, interface_vars);
				SpirvReflectUtils.Assert(result == SpirvReflectNative.SpvReflectResult.SPV_REFLECT_RESULT_SUCCESS);

				// Convert to managed
				return ReflectInterfaceVariable.ToManaged(interface_vars, var_count);
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
				return ReflectBlockVariable.ToManaged(push_consts, var_count);
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
			SourceFile = new string(module.source_file);
			SourceSource = new string(module.source_source);

			// Entry point extraction
			EntryPoints = new ReflectEntryPoint[module.entry_point_count];
			for (int i = 0; i < module.entry_point_count; i++)
			{
				EntryPoints[i] = new ReflectEntryPoint()
				{
					Id = module.entry_points[i].id,
					Name = new string(module.entry_points[i].name),
					ShaderStage = (ReflectShaderStage)module.entry_points[i].shader_stage,
					SpirvExecutionModel = (ExecutionModel)module.entry_points[i].spirv_execution_model,

					UsedPushConstants = new uint[module.entry_points[i].used_push_constant_count],
					UsedUniforms = new uint[module.entry_points[i].used_uniform_count],

					DescriptorSets = new ReflectDescriptorSet[module.entry_points[i].descriptor_set_count]
				};
				// Enumerate used push constants
				for (int j = 0; j < module.entry_points[i].used_push_constant_count; j++)
				{
					EntryPoints[i].UsedPushConstants[j] = module.entry_points[i].used_push_constants[j];
				}
				// Enumerate used uniforms
				for (int j = 0; j < module.entry_points[i].used_uniform_count; j++)
				{
					EntryPoints[i].UsedUniforms[j] = module.entry_points[i].used_uniforms[j];
				}
				// Enumerate descriptor sets
				for (int j = 0; j < module.entry_points[i].descriptor_set_count; j++)
				{
					var desc = module.entry_points[i].descriptor_sets[j];
					EntryPoints[i].DescriptorSets[j].Set = desc.set;
					EntryPoints[i].DescriptorSets[j].Bindings = new ReflectDescriptorBinding[desc.binding_count];

					for (int k = 0; k < desc.binding_count; k++)
					{
						var binding = *desc.bindings[k];
						EntryPoints[i].DescriptorSets[j].Bindings[k] = new ReflectDescriptorBinding()
						{
							Set = binding.set,
							Accessed = binding.accessed,
							Name = new string(binding.name),
							Binding = binding.binding,
							SpirvId = binding.spirv_id,
							Count = binding.count,
							ResourceType = (ReflectResourceType)binding.resource_type,
							UavCounterId = binding.uav_counter_id,
							InputAttachmentIndex = binding.input_attachment_index,
							Image = new ReflectImageTraits(binding.image),
						};

					}
				}
			}
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
