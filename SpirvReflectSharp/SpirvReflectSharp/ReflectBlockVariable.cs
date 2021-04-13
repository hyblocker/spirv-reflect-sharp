namespace SpirvReflectSharp
{
	public struct ReflectBlockVariable
	{
		public uint SpirvId;
		public string Name;
		public uint Offset;
		public uint AbsoluteOffset;
		public uint Size;
		public uint PaddedSize;
		public ReflectDecoration DecorationFlags;
		public ReflectNumericTraits Numeric;
		public ReflectArrayTraits Array;
		public ReflectVariable Flags;
		public ReflectBlockVariable[] Members;
		public ReflectTypeDescription TypeDescription;

		public override string ToString()
		{
			return "ReflectBlockVariable {" + Name + "} [" + Members.Length + "]";
		}

		internal static unsafe ReflectBlockVariable[] ToManaged(SpirvReflectNative.SpvReflectBlockVariable** push_consts, uint var_count)
		{
			ReflectBlockVariable[] blockVars = new ReflectBlockVariable[var_count];

			for (int i = 0; i < var_count; i++)
			{
				var blockVarNative = push_consts[i];
				var block = *blockVarNative;
				ReflectBlockVariable variable = new ReflectBlockVariable();

				PopulateReflectBlockVariable(ref block, ref variable);
				variable.Members = ToManagedArray(block.members, block.member_count);

				blockVars[i] = variable;
			}

			return blockVars;
		}

		private static unsafe ReflectBlockVariable[] ToManagedArray(SpirvReflectNative.SpvReflectBlockVariable* push_consts, uint var_count)
		{
			ReflectBlockVariable[] blockVars = new ReflectBlockVariable[var_count];

			for (int i = 0; i < var_count; i++)
			{
				var block = push_consts[i];
				ReflectBlockVariable variable = new ReflectBlockVariable();

				PopulateReflectBlockVariable(ref block, ref variable);
				variable.Members = ToManagedArray(block.members, block.member_count);

				blockVars[i] = variable;
			}

			return blockVars;
		}

		internal static unsafe void PopulateReflectBlockVariable(
			ref SpirvReflectNative.SpvReflectBlockVariable block,
			ref ReflectBlockVariable variable)
		{
			variable.SpirvId = block.spirv_id;
			variable.Name = new string(block.name);
			variable.Offset = block.offset;
			variable.AbsoluteOffset = block.absolute_offset;
			variable.Size = block.size;
			variable.PaddedSize = block.padded_size;
			variable.DecorationFlags = (ReflectDecoration)block.decoration_flags.Data;
			variable.Flags = (ReflectVariable)block.flags.Data;
			if (block.type_description != null)
			{
				variable.TypeDescription = ReflectTypeDescription.GetManaged(ref *block.type_description);
			}

			variable.Array = new ReflectArrayTraits(block.array);
			variable.Numeric = new ReflectNumericTraits(block.numeric);

		}
	}
}
