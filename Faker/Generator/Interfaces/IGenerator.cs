using DtoGenerator.MainClasses.Impl;

namespace DtoGenerator.Generator.Interfaces;

public interface IGenerator
{
    object Generate(Type t);
}
