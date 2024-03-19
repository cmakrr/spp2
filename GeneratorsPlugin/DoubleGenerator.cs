using DtoGenerator.Generator;
using DtoGenerator.Generator.Interfaces;
using DtoGenerator.MainClasses.Impl;

namespace GeneratorsPlugin;

[GeneratorAttr(typeof(double))]
public class DoubleGenerator : IGenerator
{
    private Random random;

    public DoubleGenerator()
    {
        random = new Random();
    }

    public object Generate(Type t)
    {
        return random.NextDouble();
    }
}
