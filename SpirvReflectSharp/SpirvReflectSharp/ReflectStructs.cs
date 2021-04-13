namespace SpirvReflectSharp
{
	public struct SpirvScalar
	{
		public uint Width;
		public uint Signedness;

		internal SpirvScalar(SpirvReflectNative.Scalar scalar)
		{
			Width = scalar.width;
			Signedness = scalar.signedness;
		}
	}

	public struct SpirvVector
	{
		public uint ComponentCount;

		internal SpirvVector(SpirvReflectNative.Vector vector)
		{
			ComponentCount = vector.component_count;
		}
	}

	public struct SpirvMatrix
	{
		public uint ColumnCount;
		public uint RowCount;
		public uint Stride;

		internal SpirvMatrix(SpirvReflectNative.Matrix matrix)
		{
			ColumnCount = matrix.column_count;
			RowCount = matrix.row_count;
			Stride = matrix.stride;
		}
	}

	public struct ReflectNumericTraits
	{
		public SpirvScalar Scalar;
		public SpirvVector Vector;
		public SpirvMatrix Matrix;

		internal ReflectNumericTraits(SpirvReflectNative.SpvReflectNumericTraits numeric)
		{
			Scalar = new SpirvScalar(numeric.scalar);
			Matrix = new SpirvMatrix(numeric.matrix);
			Vector = new SpirvVector(numeric.vector);
		}
	}

	public struct ReflectArrayTraits
	{
		public uint[] Dims;
		public uint Stride;

		internal unsafe ReflectArrayTraits(SpirvReflectNative.SpvReflectArrayTraits array)
		{
			Dims = new uint[array.dims_count];
			Stride = array.stride;

			// Populate Dims
			for (int i = 0; i < array.dims_count; i++)
			{
				Dims[i] = array.dims[i];
			}
		}
	}

	public struct ReflectBindingArrayTraits
	{
		public uint[] Dims;

		internal unsafe ReflectBindingArrayTraits(SpirvReflectNative.SpvReflectBindingArrayTraits array)
		{
			Dims = new uint[array.dims_count];

			// Populate Dims
			for (int i = 0; i < array.dims_count; i++)
			{
				Dims[i] = array.dims[i];
			}
		}
	}
	
	public struct ReflectImageTraits
	{
		public Dim Dim;
		public uint Depth;
		public uint Arrayed;
		public SamplingMode MultiSampled;
		public uint Sampled;
		public ImageFormat ImageFormat;

		internal ReflectImageTraits(SpirvReflectNative.SpvReflectImageTraits image)
		{
			Arrayed = image.arrayed;
			Depth = image.depth;
			Sampled = image.sampled;
			MultiSampled = (SamplingMode)image.ms;
			Dim = (Dim)image.dim;
			ImageFormat = (ImageFormat)image.image_format;
		}
	}

	public struct Traits
	{
		public ReflectNumericTraits Numeric;
		public ReflectImageTraits Image;
		public ReflectArrayTraits Array;

		internal Traits(SpirvReflectNative.Traits traits)
		{
			Array = new ReflectArrayTraits(traits.array);
			Image = new ReflectImageTraits(traits.image);
			Numeric = new ReflectNumericTraits(traits.numeric);
		}
	}

	public struct ReflectDescriptorSet
	{
		public uint Set;
		public ReflectDescriptorBinding[] Bindings;
	}
}
