using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

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
public sealed class DoctorServiceStepDefinitions: WebApplicationFactory<Program>
{
    // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

    private readonly WebApplicationFactory<Program> _factory;

    public DoctorServiceStepDefinitions(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }
    
    private HttpClient Client { get; set; }
    
    private Uri BaseUri { get; set; }
    
    private Task<HttpResponseMessage> Response { get; set; }
    
    [Given(@"the Endpoint https://localhost:(.*)/api/v(.*)/doctor")]
    public void GivenTheEndpointHttpsLocalhostApiVDoctor(int port, int version)
    {
        BaseUri = new Uri($"https://localhost:{port}/api/v{version}/doctor");
        Client = _factory.CreateClient(new WebApplicationFactoryClientOptions { BaseAddress = BaseUri });
    }
    
    [When(@"a new Doctor is registered")]
    public void WhenANewDoctorIsRegistered(Table saveDoctorResource)
    {
        var resource = saveDoctorResource.CreateSet<SaveDoctorResource>().First();
        var content = new StringContent(resource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
        
        Response = Client.PostAsync(BaseUri, content);
    }

    [Then(@"a Doctor Resource is included in the Response Body")]
    public async Task ThenADoctorResourceIsIncludedInTheResponseBody(Table expectedDoctorResource)
    {
        var expectedResource = expectedDoctorResource.CreateSet<SavePatientResource>().First();
        var responseData = await Response.Result.Content.ReadAsStringAsync();
        var resource = JsonConvert.DeserializeObject<PatientResource>(responseData);
        Assert.Equal(expectedResource.FirstName, resource.FirstName);
    }

    [Then(@"the response code is (.*)")]
    public void ThenTheResponseCodeIs(int expectedStatus)
    {
        var expectedStatusCode = ((HttpStatusCode)expectedStatus).ToString();
        var actualStatusCode = Response.Result.StatusCode.ToString();
        
        Assert.Equal(expectedStatusCode, actualStatusCode);
    }
}