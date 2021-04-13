# SPIRV-Reflect Sharp

SPIRV-Reflect Sharp provides .NET bindings for [SPIRV-Reflect](https://www.github.com) to allow generating reflection data for shaders entirely using C#

# Usage

```cs
byte[] shaderBytes = File.ReadAllBytes(@"shader.frag.spv");
using (ShaderModule module = SpirvReflect.ReflectCreateShaderModule(shaderBytes))
{
	var in_vars = module.EnumerateInputVariables();
	var intf_vars = module.EnumerateInterfaceVariables();
	var push_constants = module.EnumeratePushConstants();
}
```

# License

This project is licensed under the MIT License

SPIRV-Reflect is licensed under the Apache License

# Credits

KhronosGroup for SPIRV-Reflect