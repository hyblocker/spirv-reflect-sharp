namespace SpirvReflectSharp
{
	public struct ReflectInterfaceVariable
	{
		public uint SpirvId;
		public string Name;
		public uint Location;
		public StorageClass StorageClass;
		public string Semantic;
		public ReflectDecoration DecorationFlags;
		public BuiltIn BuiltIn;
		public ReflectNumericTraits Numeric;
		public ReflectArrayTraits Array;
		public ReflectInterfaceVariable[] Members;
		public ReflectFormat Format;
		public ReflectTypeDescription TypeDescription;

		public override string ToString()
		{
			return "ReflectInterfaceVariable {" + Name + "} [" + Members.Length + "]";
		}

		internal static unsafe ReflectInterfaceVariable[] ToManaged(SpirvReflectNative.SpvReflectInterfaceVariable** input_vars, uint var_count)
		{
			ReflectInterfaceVariable[] intf_vars = new ReflectInterfaceVariable[var_count];

			for (int i = 0; i < var_count; i++)
			{
				var interfaceVarNative = input_vars[i];
				var intf = *interfaceVarNative;
				ReflectInterfaceVariable variable = new ReflectInterfaceVariable();

				PopulateReflectInterfaceVariable(ref intf, ref variable);
				variable.Members = ToManagedArray(intf.members, intf.member_count);

				intf_vars[i] = variable;
			}

			return intf_vars;
		}

		private static unsafe ReflectInterfaceVariable[] ToManagedArray(SpirvReflectNative.SpvReflectInterfaceVariable* input_vars, uint var_count)
		{
			ReflectInterfaceVariable[] intf_vars = new ReflectInterfaceVariable[var_count];

			for (int i = 0; i < var_count; i++)
			{
				var intf = input_vars[i];
				ReflectInterfaceVariable variable = new ReflectInterfaceVariable();

				PopulateReflectInterfaceVariable(ref intf, ref variable);
				variable.Members = ToManagedArray(intf.members, intf.member_count);

				intf_vars[i] = variable;
			}

			return intf_vars;
		}

		internal static unsafe void PopulateReflectInterfaceVariable (
			ref SpirvReflectNative.SpvReflectInterfaceVariable intf,
			ref ReflectInterfaceVariable variable)
		{
			variable.SpirvId = intf.spirv_id;
			variable.Name = new string(intf.name);
			variable.Location = intf.location;
			variable.StorageClass = (StorageClass)intf.storage_class;
			variable.Semantic = new string(intf.semantic);
			variable.DecorationFlags = (ReflectDecoration)intf.decoration_flags.Data;
			variable.BuiltIn = (BuiltIn)intf.built_in;
			variable.Format = (ReflectFormat)intf.format;
			variable.TypeDescription = ReflectTypeDescription.GetManaged(ref *intf.type_description);
			variable.Array = new ReflectArrayTraits(intf.array);
			variable.Numeric = new ReflectNumericTraits(intf.numeric);


		}
	}
}
