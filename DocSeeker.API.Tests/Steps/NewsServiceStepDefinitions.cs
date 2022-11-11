using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using System.Net;
using System.Net.Mime;
using System.Text;
using DocSeeker.API.Docseeker.Resources;
using DocSeeker.API.DocSeeker.Resources;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using SpecFlow.Internal.Json;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Xunit;

namespace DocSeeker.API.Tests.Steps;

[Binding]
public sealed class NewsServiceStepDefinitions: WebApplicationFactory<Program>
{
    // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

    private readonly WebApplicationFactory<Program> _factory;

    public NewsServiceStepDefinitions(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    private HttpClient Client { get; set; }
    
    private Uri BaseUri { get; set; }
    
    private Task<HttpResponseMessage> Response { get; set; }

    [Given(@"the Endpoint https://localhost:(.*)/api/v(.*)/new")]
    public void GivenTheEndpointHttpsLocalhostApiVNew(int port, int version)
    {
        BaseUri = new Uri($"https://localhost:{port}/api/v{version}/new");
        Client = _factory.CreateClient(new WebApplicationFactoryClientOptions { BaseAddress = BaseUri });
    }

    [When(@"a news article is posted")]
    public void WhenANewsArticleIsPosted(Table saveNewsResource)
    {
        var resource = saveNewsResource.CreateSet<SaveNewResource>().First();
        var content = new StringContent(resource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
        Response = Client.PostAsync(BaseUri, content);
    }

    [Then(@"the response code returned for the news article is (.*)")]
    public void ThenTheResponseCodeReturnedForTheNewsArticleIs(int expectedStatus)
    {
        var expectedStatusCode = ((HttpStatusCode)expectedStatus).ToString();
        var actualStatusCode = Response.Result.StatusCode.ToString();
        
        Assert.Equal(expectedStatusCode, actualStatusCode);
    }

    [Then(@"a News Resource is included in the Response Body")]
    public async Task ThenANewsResourceIsIncludedInTheResponseBody(Table expectedNewsResource)
    {
        var expectedResource = expectedNewsResource.CreateSet<SaveNewResource>().First();
        var responseData = await Response.Result.Content.ReadAsStringAsync();
        var resource = JsonConvert.DeserializeObject<NewResource>(responseData);
        Assert.Equal(expectedResource.Title, resource.Title);
    }
}