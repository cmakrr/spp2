using System.Reflection;
using DtoGenerator.Generator.Interfaces;

namespace DtoGenerator.Generator.Loader;

public class GeneratorsLoader
{
    internal bool IsGenerator(Type t)
    {
        string? generatorInterfacePath = typeof(IGenerator).FullName;
        if (generatorInterfacePath == null)
        {
            return false;
        }

        Type? type = t.GetInterface(generatorInterfacePath);
        if (type == null)
        {
            return false;
        }

        Attribute? attribute = t.GetCustomAttribute(typeof(GeneratorAttrAttribute));
        return attribute != null;
    }

    public Dictionary<Type, IGenerator> LoadGenerators(string pluginAssemblyPath)
    {
        Dictionary<Type, IGenerator> loadedGenerators = new();

        Assembly pluginAssembly = Assembly.LoadFile(pluginAssemblyPath);
        Type[] expectedPlugins = pluginAssembly.GetTypes();
        foreach (Type expectedPlugin in expectedPlugins)
        {
            if (IsGenerator(expectedPlugin))
            {
                GeneratorAttrAttribute generatorAttribute = (GeneratorAttrAttribute)expectedPlugin
                    .GetCustomAttribute(typeof(GeneratorAttrAttribute))!;
                Type generatedValueType = generatorAttribute.generatorType;
                IGenerator? generator = Activator.CreateInstance(expectedPlugin) as IGenerator;
                loadedGenerators[generatedValueType] = generator!;
            }
        }

        return loadedGenerators;
    }
}
