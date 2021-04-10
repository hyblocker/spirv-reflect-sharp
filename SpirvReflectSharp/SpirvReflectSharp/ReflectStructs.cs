namespace SpirvReflectSharp
{
	public struct SpirvScalar
	{
		public uint Width;
		public uint Signedness;
	}

	public struct SpirvVector
	{
		public uint ComponentCount;
	}

	public struct SpirvMatrix
	{
		public uint ColumnCount;
		public uint RowCount;
		public uint Stride;
	}

	public struct ReflectNumericTraits
	{
		public SpirvScalar Scalar;
		public SpirvVector Vector;
		public SpirvMatrix Matrix;
	}

	public struct ReflectArrayTraits
	{
		public uint[] Dims;
		public uint Stride;
	}
	
	public struct ReflectImageTraits
	{
		public Dim Dim;
		public uint Depth;
		public uint Arrayed;
		public SamplingMode MultiSampled;
		public uint Sampled;
		public ImageFormat ImageFormat;
	}

	public struct Traits
	{
		public ReflectNumericTraits Numeric;
		public ReflectImageTraits Image;
		public ReflectArrayTraits Array;
	}
}
