using AmidoTechBddfyTests.Builders;
using AmidoTechBddfyTests.Configuration;
using AmidoTechBddfyTests.Models;
using Shouldly;
using System.Net;

namespace AmidoTechBddfyTests.Tests.Steps
{
    public class DeleteUserSteps : StepsBase
    {
        private string _existingUserId;
        private const string UserPath = "/users/";

        public DeleteUserSteps()
        {
            var config = ConfigAccessor.GetApplicationConfiguration();
            BaseUrl = config.BaseUrl;
        }

        #region Step Definitions

        #region Given

        public void GivenAUserExists()
        {
            var config = ConfigAccessor.GetApplicationConfiguration();
            //_existingUserId = "59374fh3rhfjsjjjakskw8w";
            _existingUserId = config.ExisitngUserId;

        }

        #endregion Given

        #region When

        public async Task WhenIDeleteUser()
        {
            LastResponse = await HttpRequestFactory.Delete(BaseUrl, UserPath + _existingUserId);
        }

        #endregion When

        #region Then

        public Task ThenTheCorrectResponseCodeIsReturned()
        {
            if (LastResponse.RequestMessage != null)
                LastResponse.StatusCode.ShouldBe(HttpStatusCode.NoContent,
                    $"Response from {LastResponse.RequestMessage.Method} {LastResponse.RequestMessage.RequestUri} was not as expected");
            return Task.CompletedTask;
        }

        #endregion Then

        #endregion Step Definitions
    }
}
