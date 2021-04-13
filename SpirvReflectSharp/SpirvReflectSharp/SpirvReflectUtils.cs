using System;

namespace SpirvReflectSharp
{
	public static class SpirvReflectUtils
	{
		public static void Assert(bool condition)
		{
			if (!condition)
			{
				throw new Exception("Failed assert!");
			}
		}
	}
}
