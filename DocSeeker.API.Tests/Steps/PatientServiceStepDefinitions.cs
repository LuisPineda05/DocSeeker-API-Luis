using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
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
public sealed class PatientServiceStepDefinitions: WebApplicationFactory<Program>
{
    // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

    private readonly WebApplicationFactory<Program> _factory;

    public PatientServiceStepDefinitions(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }
    
    private HttpClient Client { get; set; }
    
    private Uri BaseUri { get; set; }
    
    private Task<HttpResponseMessage> Response { get; set; }
    
    [Given(@"the Endpoint https://localhost:(.*)/api/v(.*)/patient")]
    public void GivenTheEndpointHttpsLocalhostApiVPatient(int port, int version)
    {
        BaseUri = new Uri($"https://localhost:{port}/api/v{version}/patient");
        Client = _factory.CreateClient(new WebApplicationFactoryClientOptions { BaseAddress = BaseUri });
    }

    [When(@"a Post Request is sent")]
    public void WhenAPostRequestIsSent(Table savePatientResource)
    {
        // remember to check SavePatientResource, you may have to use SaveUserResource instead
        var resource = savePatientResource.CreateSet<SavePatientResource>().First();
        var content = new StringContent(resource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
        Response = Client.PostAsync(BaseUri, content);
    }

    [Then(@"the response code returned is (.*)")]
    public void ThenTheResponseCodeReturnedIs(int expectedStatus)
    {
        var expectedStatusCode = ((HttpStatusCode)expectedStatus).ToString();
        var actualStatusCode = Response.Result.StatusCode.ToString();
        
        Assert.Equal(expectedStatusCode, actualStatusCode);
    }

    [Then(@"a Patient Resource is included in the Response Body")]
    public async Task ThenAPatientResourceIsIncludedInTheResponseBody(Table expectedPatientResource)
    {
        var expectedResource = expectedPatientResource.CreateSet<SavePatientResource>().First();
        var responseData = await Response.Result.Content.ReadAsStringAsync();
        var resource = JsonConvert.DeserializeObject<PatientResource>(responseData);
        Assert.Equal(expectedResource.FirstName, resource.FirstName);
    }
    
    [When(@"a Request is sent to delete Patient with id (.*)")]
    public void WhenARequestIsSentToDeletePatientWithId(int id)
    {
        Response = Client.DeleteAsync(BaseUri.ToString() + $"/{id}");
        Response = Client.GetAsync(BaseUri);
    }
}