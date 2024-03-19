using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DtoGenerator.Generator;
using DtoGenerator.Generator.Implementations;
using DtoGenerator.Generator.Interfaces;
using DtoGenerator.Generator.Loader;
using DtoGenerator.MainClasses.Impl;

namespace DtoGenerator.Config;

public class GeneratorsConfiguration
{
    private const string pluginPath = "D:\\spp\\myfaker\\Faker\\GeneratorsPlugin\\bin\\Debug\\net8.0\\GeneratorsPlugin.dll";
    public Dictionary<Type, IGenerator> generatorsDictionary { get; set; }

    public GeneratorsConfiguration()
    {
        generatorsDictionary = CreateGenerators();
    }

    public void AddListGeneratorToFaker(Faker faker)
    {
        faker.AddGenerator(typeof(List<>),new ListGenerator(faker));
    }

    private Dictionary<Type, IGenerator> CreateGenerators()
    {
        Dictionary<Type, IGenerator> generators = new();
        generators.Add(typeof(int), new IntGenerator());
        generators.Add(typeof(long), new LongGenerator());
        generators.Add(typeof(string), new StringGenerator());
        GeneratorsLoader loader = new GeneratorsLoader();
        foreach(KeyValuePair<Type,IGenerator> generator in loader.LoadGenerators(pluginPath))
        {
            generators[generator.Key] =  generator.Value;
        }
        return generators;
    }
}
