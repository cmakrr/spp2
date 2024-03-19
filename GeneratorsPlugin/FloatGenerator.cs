using DtoGenerator.Generator;
using DtoGenerator.Generator.Interfaces;
using DtoGenerator.MainClasses.Impl;

namespace GeneratorsPlugin;

[GeneratorAttr(typeof(float))]
public class FloatGenerator : IGenerator
{
    private Random random;

    public FloatGenerator()
    {
        random = new Random();
    }

    public object Generate(Type t)
    {
        return random.NextSingle();
    }
}
