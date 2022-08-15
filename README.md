# Grateful
Grateful is a library that takes the pain and headache away from integration testing an ASP.NET Web API project. In other words it is a framework for integration testing an ASP.NET Web API project.

This is a Nuget project, and can be found <a href="https://www.nuget.org/packages/Grateful.xUnit#readme-body-tab">here</a>

# Table of Contents
1) [HowToUse](#howto)
2) [Contributing](#contributing)
3) [Grateful?](#grateful?)

## How to Use

For detailed examples, see Grateful.xUnit.Tests under the <a href="https://github.com/seadavis/Grateful.xUnit/tree/main/test">Test</a> folder. 

The basic workflow for a test is this:
- Start up an ASP.NET project.
- Hit a specific route with an HttpClient
- Assert on the data returned form an HttpClient.

If a series of tests is run only one ASP.NET project is started.
To enable the shared context across tests in xUnit, Grateful uses collections.

Every test in Grateful must be specified in a collection.

The most basic testing class in the test project is the <a href="https://github.com/seadavis/Grateful.xUnit/blob/main/test/Grateful.xUnit.Tests/HttpCollections.cs">HttpCollections.cs</a> class. It uses the collection called, "ASP.NET Working collection" which is specified like so,

```csharp
[CollectionDefinition("ASP.NET Working Collection")]
public class WorkingProjectCollection : ICollectionFixture<WorkingProjectFixture>
{
}
```

And the corresponding fixture is,

```csharp
public class WorkingProjectFixture : HttpClientFixture
{
    public WorkingProjectFixture() : base(@"..\..\..\..\Grateful.xUnit.Test.SampleProject")
    {

    }

}
```

As you can see, from the above example all you need to do is put a relative directory path in the base class constructor for HttpClientFixture. This path is relative to the output path of the xUnit project where the Fixutre resides.

The specifies the root directory of the ASP.NET WebAPI project you wish to test.

Grateful relies on the existence of launchSettings.json under the Properties folder and will launch the first http project specified under the "profiles" section. For an example see <a href="https://github.com/seadavis/Grateful.xUnit/blob/main/test/Grateful.xUnit.Test.SampleProject/Properties/launchSettings.json">The launch settings under the sample project</a>.

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

### Post

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

### Delete

```csharp
[Fact]
public async Task Delete()
{
    var statusCode = await _fixture.Client.Delete("helloworld");
    Assert.Equal(HttpStatusCode.OK, statusCode);
}
```

### Authorization

Authorization is a little tricky and relies on MSAL to work properly. There are two components to the authorization. 

1) Specifying authorization on the ASP.NET project.
2) Specifying a daemon client for the MSAL portion.

To specify the authorization on an ASP.NET project, you'll need to create an app registration in Azure, for example Grateful.Sample.API. Then on the actual ASP.NET project you'll need to add a section to appsettings.Development.json

The overview section of the Appregistration will then provide all of the info you'll need for 

```json
 "AzureAd": {
      "Instance": "https://login.microsoftonline.com/",
      "TenantId": "<Directory (tenant) Id>",
      "ClientId": "The application id of Grateful.Sample.API"
   }
```

Then to allow for daemon applications you'll need to add an API permission for your API project. 

First create an appregisitration for the API for example Grateful.Sample.API, then add an Application ID URI. Then create an App role for the API app reigstration.

Simply select "Add a Permission" from the API permissions section of Grateful.Sample.API, and then select the API, under APIs my organization uses. And select Application permissions. Then give the newly created permission admin consent.

For the xUnit project, you'll need another App registration, such as Grateful.Sample.App. You'll need to create a new client secret. And add the permission we previously created for the API app registration. Then give the permission admin consent.

For Grateful to find the necessary secrets to authorize against the Web API you'll need to put a folder called test.settings.json in the root directory of the Grateful xUnit project.

Here is what the Json file should look like;

```json
{
   "AppClientId": "<the application id of Grateful.Sample.App>",
   "APIClientId": "<The application id of Grateful.Sample.API>",
   "ClientSecret": "<A client secret created on Grateful.Sample.App>",
   "TenantId": "<The same tenant id used in the creation of the ASP.NET application>"
}
```

Then to have the HttpClient authorize using the secrets given above you simply do;

```csharp
 [Fact]
public async Task PostAuthorized()
{

    var data = await _fixture.Client.PostAuthorized<HelloWorldData, HelloWorldData>("helloworld/api/auth", new HelloWorldData()
    {
    Name = "Sean!",
    Greeting = "Hello, man!"
    });
    data.Should().BeOkWithData(new HelloWorldData()
    {
    Greeting = "Salutations!",
    Name = "Post Authorized User!"
    });
}
```

For the test.settings.json file I choose to set it to gitignore, since it contains sensitive information. Then to run automated tests in an Azure pipeline I add it to the <a href="https://docs.microsoft.com/en-us/azure/devops/pipelines/library/?view=azure-devops">Azure pipeline library</a> and then copy it.

Here is sample Yaml for downloading the file from the Azure library

```yaml
  - task: DownloadSecureFile@1
    displayName: 'DownloadTestSettings'
    inputs:
      secureFile: 'test.settings.json'
```

And sample yaml for copying from the Azure library into the xUnit project.

```yaml
  - task: CopyFiles@2
    name: 'CopyTestSettings'
    inputs:
     SourceFolder: '$(Agent.TempDirectory)'
     Contents: 'test.settings.json'
     TargetFolder: '$(Agent.WorkFolder)\1\s\test\Grateful.xUnit.Tests\'
```

## Contributing

This project is open for contributions. Simply open a pull request, and I will look at it, and I will integrate if appropriate.

To run the unit tests locally you must setup authorization as described in the preceeding section. the test.settings.json file, must be ignored for the PR to be accepted.

And you should use appSettings.Development.json in Grateful.xUnit.Test.SampleProject, for the ASP.NET Web API secrets, and it should be json ignored1 ).

## Grateful?
The name is because it is an Inte<b>grat</b>ion testing framework. And because I am grateful to have such a framework.

This project was designed because I was on a project that failed, in part because I spent too long unit testing, what should have been an integration test. At that moment I was finally fed up with mocks, and stubs, but I still wanted to test. So I invented Grateful, the framework that makes it easy to integration test ASP.NET WebAPI projects.