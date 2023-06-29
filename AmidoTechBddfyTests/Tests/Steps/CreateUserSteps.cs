using AmidoTechBddfyTests.Builders;
using AmidoTechBddfyTests.Configuration;
using AmidoTechBddfyTests.Models;
using Shouldly;
using System.Net;

namespace AmidoTechBddfyTests.Tests.Steps
{
    public class CreateUserSteps : StepsBase
    {
        private User _user;
        private const string UserPath = "/users";

        public CreateUserSteps()
        {
            var config = ConfigAccessor.GetApplicationConfiguration();
            BaseUrl = config.BaseUrl; //gets from appsetting.json

            //BaseUrl = Environment.GetEnvironmentVariable("BaseUrl"); //gets from my system environment variables.
        }

        #region Step Definitions

        #region Given

        public void GivenAUniqueUser()
        {
            _user = new User
            {
                name = "Joe",
                password = "MyCurrentPassword"
            };
        }

        #endregion Given

        #region When

        public async Task WhenIAddAUser()
        {
            LastResponse = await HttpRequestFactory.Post(BaseUrl, UserPath, _user);
        }

        #endregion When

        #region Then

        public Task ThenTheCorrectResponseCodeIsReturned()
        {
            if (LastResponse.RequestMessage != null)
                LastResponse.StatusCode.ShouldBe(HttpStatusCode.Created,
                    $"Response from {LastResponse.RequestMessage.Method} {LastResponse.RequestMessage.RequestUri} was not as expected");
            return Task.CompletedTask;
        }

        #endregion Then

        #endregion Step Definitions
    }
}
