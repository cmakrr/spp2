using System.Reflection;
using DtoGenerator.Config;
using DtoGenerator.Exceptions;
using DtoGenerator.Generator.Interfaces;
using DtoGenerator.MainClasses.Interfaces;

namespace DtoGenerator.MainClasses.Impl;

public class Faker : IFaker
{
    private readonly Dictionary<Type, IGenerator> generatorsDictionary;

    private readonly ISet<Type> currentlyGeneratingTypes = new HashSet<Type>();


    public Faker(Dictionary<Type, IGenerator> generators)
    {
        generatorsDictionary = generators;
    }

    public void AddGenerator(Type type, IGenerator generator)
    {
        generatorsDictionary[type] = generator;
    }

    private ConstructorInfo? GetEmptyConstructor(Type t)
    {
        return t.GetConstructor(Type.EmptyTypes);
    }

    private bool IsDto(Type type)
    {
        return typeof(IDto).IsAssignableFrom(type); 
    }

    private object? CreateDto(Type t)
    {
        if (!currentlyGeneratingTypes.Add(t))
        {
            return null;
        }

        ConstructorInfo? emptyConstructor = GetEmptyConstructor(t);
        if (emptyConstructor == null)
        {
            throw new NoEmptyConstructorException();
        }
        object result = emptyConstructor.Invoke(null);

        foreach (PropertyInfo propertyInfo in t.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
        {
            if (propertyInfo.CanWrite)
            {
                Type type;
                if (propertyInfo.PropertyType.IsGenericType)
                {
                    type = propertyInfo.PropertyType.GetGenericTypeDefinition();
                }
                else
                {
                    type = propertyInfo.PropertyType;
                }
                if (generatorsDictionary.ContainsKey(type))
                {
                    Type argumentType = propertyInfo.PropertyType.IsGenericType ? propertyInfo.PropertyType.GetGenericArguments()[0] : type;
                    propertyInfo.SetValue(result, generatorsDictionary[type].Generate(argumentType));
                }
                else if (IsDto(type))
                {
                    propertyInfo.SetValue(result, Create(type));
                }
            }
        }
        foreach (FieldInfo fieldInfo in t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
        {
            if (!fieldInfo.Attributes.HasFlag(FieldAttributes.InitOnly))
            {
                Type type;
                if (fieldInfo.FieldType.IsGenericType)
                {
                    type = fieldInfo.FieldType.GetGenericTypeDefinition();
                }
                else
                {
                    type = fieldInfo.FieldType;
                }
                if (generatorsDictionary.ContainsKey(type))
                {
                    Type argumentType = fieldInfo.FieldType.IsGenericType ? fieldInfo.FieldType.GetGenericArguments()[0] : type;
                    fieldInfo.SetValue(result, generatorsDictionary[type].Generate(argumentType));
                }
                else if (IsDto(type))
                {
                    fieldInfo.SetValue(result, Create(type));
                }
            }
        }
        currentlyGeneratingTypes.Remove(t);
        return result;
    }

    public object? Create(Type t)
    {
        if (IsDto(t))
        {
            return CreateDto(t);
        } else if (generatorsDictionary.ContainsKey(t))
        {
            return generatorsDictionary[t].Generate(t);
        }
        return null;
    }


    public T Create<T>() where T : IDto
    {
        return (T)Create(typeof(T));
    }
}