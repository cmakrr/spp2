using DtoGenerator.Generator.Interfaces;
using DtoGenerator.MainClasses.Impl;

namespace DtoGenerator.Generator.Implementations;

public class LongGenerator : IGenerator
{
    private readonly Random random;

    public LongGenerator()
    {
        random = new Random();
    }

    public object Generate(Type t)
    {
        return random.NextInt64();
    }
}