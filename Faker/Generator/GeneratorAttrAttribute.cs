namespace DtoGenerator.Generator;

public class GeneratorAttrAttribute : Attribute
{
    public Type generatorType
    { get; set; }

    public GeneratorAttrAttribute(Type genereatorType)
    {
        this.generatorType = genereatorType;
    }
}
