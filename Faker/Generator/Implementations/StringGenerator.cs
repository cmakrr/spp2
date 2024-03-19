using DtoGenerator.Generator.Interfaces;
using DtoGenerator.MainClasses.Impl;
using System;

namespace DtoGenerator.Generator.Implementations;

public class StringGenerator : IGenerator
{
    private Random random;
    private const int minSize = 0;
    private const int maxSize = 1000;
    private const string customChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

    public StringGenerator()
    {
        random = new Random();
    }

    public object Generate(Type t)
    {
        int length = random.Next(minSize, maxSize);
        return new string(Enumerable.Repeat(customChars, length)
        .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}