namespace SpirvReflectSharp
{
	public struct ReflectTypeDescription
	{
		public uint Id;
		public Op Op;
		public string TypeName;
		public string StructMemberName;
		public StorageClass StorageClass;
		public ReflectType TypeFlags;
		public ReflectDecoration DecorationFlags;
		public Traits Traits;
		public ReflectTypeDescription[] Members;

		public override string ToString()
		{
			return "ReflectTypeDescription {" + StructMemberName + " " + TypeFlags + "} [" + Members.Length + "]";
		}

		internal static unsafe ReflectTypeDescription GetManaged(ref SpirvReflectNative.SpvReflectTypeDescription type_description)
		{
			ReflectTypeDescription desc = new ReflectTypeDescription();

			PopulateReflectTypeDescription(ref type_description, ref desc);
			desc.Members = ToManagedArray(type_description.members, type_description.member_count);

			return desc;
		}

		private static unsafe ReflectTypeDescription[] ToManagedArray(SpirvReflectNative.SpvReflectTypeDescription* type_description, uint member_count)
		{
			ReflectTypeDescription[] intf_vars = new ReflectTypeDescription[member_count];

			for (int i = 0; i < member_count; i++)
			{
				var typedesc = type_description[i];
				ReflectTypeDescription variable = new ReflectTypeDescription();

				PopulateReflectTypeDescription(ref typedesc, ref variable);
				variable.Members = ToManagedArray(typedesc.members, typedesc.member_count);

				intf_vars[i] = variable;
			}

			return intf_vars;
		}

		private static unsafe void PopulateReflectTypeDescription(
			ref SpirvReflectNative.SpvReflectTypeDescription type_description,
			ref ReflectTypeDescription desc)
		{
			desc.Id = type_description.id;
			desc.Op = (Op)type_description.op;
			desc.TypeName = new string(type_description.type_name);
			desc.StructMemberName = new string(type_description.struct_member_name);
			desc.StorageClass = (StorageClass)type_description.storage_class;
			desc.TypeFlags = (ReflectType)type_description.type_flags.Data;
			desc.DecorationFlags = (ReflectDecoration)type_description.decoration_flags.Data;
			desc.Traits = new Traits(type_description.traits);
		}
	}
}
