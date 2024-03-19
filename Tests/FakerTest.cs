using DtoGenerator.Config;
using DtoGenerator.Generator;
using DtoGenerator.MainClasses.Impl;
using FluentAssertions;
using Tests.TestClasses;

namespace Tests;

[TestClass]
public class FakerTest
{
    private static GeneratorsConfiguration config;
    private static Faker faker;

    [AssemblyInitialize]
    public static void AssemblyInit(TestContext context)
    {
        config = new GeneratorsConfiguration();
        faker = new Faker(config.generatorsDictionary);
        config.AddListGeneratorToFaker(faker);
    }

    [TestMethod]
    public void TestDtoWithPrimitives()
    {
        DtoWithPrimitives dto = faker.Create<DtoWithPrimitives>();

        dto.x.Should().BeInRange(0,Int32.MaxValue);
    }

    [TestMethod]
    public void TestDtoWithList()
    {
        DtoWithList dto = faker.Create<DtoWithList>();
        
        dto.list.Should().NotBeNull();
    }

    [TestMethod]
    public void TestDtoWithInternalDto()
    {
        DtoWithInternalDto dto = faker.Create<DtoWithInternalDto>();

        dto.dto.Should().NotBeNull();
    }

    [TestMethod]
    public void TestDtoContainingItself()
    {
        DtoContainingItself dto = faker.Create<DtoContainingItself>();

        dto.dto.Should().BeNull();
        dto.x.Should().BeInRange(0,Int32.MaxValue);
    }

}