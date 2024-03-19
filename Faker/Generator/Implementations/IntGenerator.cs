using DtoGenerator.Generator.Interfaces;
using DtoGenerator.MainClasses.Impl;

namespace DtoGenerator.Generator.Implementations;

public class IntGenerator : IGenerator
{
    private readonly Random random;

    public IntGenerator()
    {
        random = new Random();
    }

    public object Generate(Type t)
    {
        return random.Next();
    }
}