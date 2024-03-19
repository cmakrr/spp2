using System.Reflection;
using DtoGenerator.Generator.Interfaces;
using DtoGenerator.MainClasses.Impl;
using DtoGenerator.MainClasses.Interfaces;

namespace DtoGenerator.Generator.Implementations;

public class ListGenerator : IGenerator
{
    private const int minSize = 0;
    private const int maxSize = 1000;
    private Random random;
    private IFaker faker { get; set; }

    public ListGenerator(IFaker faker)
    {
        this.faker = faker;
        random = new Random();
    }

    public List<T> CreateList<T>(int size)
    {
        List<T> l = new List<T>();
        for (int i = 0; i < size; i++)
        {
            l.Add((T)faker.Create(typeof(T)));
        }
        return l;
    }

    public object Generate(Type t)
    {

        MethodInfo methodInfo = GetType().GetMethod("CreateList")!;

        MethodInfo genericMethodInfo = methodInfo.MakeGenericMethod(t);

        return genericMethodInfo.Invoke(this, [random.Next(minSize,maxSize)]);
    }
}
