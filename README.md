# Grateful
Grateful is a library that takes the pain and headache away from integration testing an ASP.NET Web API project. In other words it is a framework for integration testing an ASP.NET Web API project.

# Table of Contents
[HowToUse](#howto)
[Contributing](#contributing)
[Grateful?](#whyname)

## How to Use

For detailed examples, see Grateful.xUnit.Tests under the <a href="https://github.com/seadavis/Grateful.xUnit/tree/main/test">Test</a> folder. 

The basic workflow for a test is this:
- Start up an ASP.NET project.
- Hit a specific route with an HttpClient
- Assert on the data returned form an HttpClient.

If a series of tests is run only one ASP.NET project is started.
To enable the shared context across tests in xUnit, Grateful uses collections.

Every test in Grateful must be specified in a collection.

The most basic testing class in the Unit Test is the <a href="https://github.com/seadavis/Grateful.xUnit/blob/main/test/Grateful.xUnit.Tests/HttpCollections.cs">HttpCollections.cs</a> class. It uses the collection called, "ASP.NET Working collection" which is specified like so,

```csharp
[CollectionDefinition("ASP.NET Working Collection")]
public class WorkingProjectCollection : ICollectionFixture<WorkingProjectFixture>
{
}
```

And the corresponding fixture

```csharp
public class WorkingProjectFixture : HttpClientFixture
{
    public WorkingProjectFixture() : base(@"..\..\..\..\Grateful.xUnit.Test.SampleProject")
    {

    }

}
```

As you can see, from the above example all you need to do is put a relative directory path in the base class constructor for HttpClientFixture. This path is relative to the output path of the xUnit project where the Fixutre resides.

For more information on collections and fixture see <a href="https://xunit.net/docs/shared-context">xUnit Docs.</a>

### Get

Testing a "Get" Http method can be done like so,
```csharp
[Fact]
public async Task Get()
{
    var data = await _fixture.Client.Get<HelloWorldData>("helloworld");
    data.Should().BeOkWithData(new HelloWorldData()
    {
    Name = "Sean!",
    Greeting = "Hello, man!"
    });
}
```

And a post,

```csharp
[Fact]
public async Task Post()
{
    var data = await _fixture.Client.Post<HelloWorldData, HelloWorldData>("helloworld", new HelloWorldData()
    {
    Name = "Sean!",
    Greeting = "Hello, man!"
    });
    data.Should().BeOkWithData(new HelloWorldData()
    {
    Name = "Sean!",
    Greeting = "Hello, man!"
    });
}
```

And a delete, 

```csharp
[Fact]
public async Task Delete()
{
    var statusCode = await _fixture.Client.Delete("helloworld");
    Assert.Equal(HttpStatusCode.OK, statusCode);
}
```

## whyname
The name is because it is an Inte<b>grat</b>ion testing framework. And because I am grateful to test an ASP.NEt project.

This project was designed because I was on a project that failed, in part because I spent too long unit testing, what should have been an integration test. At that moment I was finally fed up with mocks, and stubs, but I still wanted to test. So I invented Grateful, the framework that makes it easy to integration test ASP.NET WebAPI projects.