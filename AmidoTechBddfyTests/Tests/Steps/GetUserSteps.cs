using AmidoTechBddfyTests.Builders;
using AmidoTechBddfyTests.Configuration;
using AmidoTechBddfyTests.Models;
using Newtonsoft.Json;
using Shouldly;
using System.Net;

namespace AmidoTechBddfyTests.Tests.Steps
{
    public class GetUserSteps : StepsBase
    {
        private string _existingUserId;
        private string _currentPassword;
        private const string UserPath = "/users/";

        public GetUserSteps()
        {
            var config = ConfigAccessor.GetApplicationConfiguration();
            BaseUrl = config.BaseUrl;
            _currentPassword = config.CurrentPassword;
        }

        #region Step Definitions

        #region Given

        public void GivenAUserExists()
        {
            var config = ConfigAccessor.GetApplicationConfiguration();
            _existingUserId = config.ExisitngUserId;

            //_existingUserId = "59374fh3rhfjsjjjakskw8w";
        }

        #endregion Given

        #region When

        public async Task WhenIGetAUser()
        {
            LastResponse = await HttpRequestFactory.Get(BaseUrl, UserPath + _existingUserId);
        }

        #endregion When

        #region Then

        public Task ThenTheCorrectResponseCodeIsReturned()
        {
            if (LastResponse.RequestMessage != null)
                LastResponse.StatusCode.ShouldBe(HttpStatusCode.OK,
                    $"Response from {LastResponse.RequestMessage.Method} {LastResponse.RequestMessage.RequestUri} was not as expected");
            return Task.CompletedTask;
        }

        public async Task ThenTheUserDetailsAreCorrect()
        {
            var responseUser =
                JsonConvert.DeserializeObject<UserResponse>(await LastResponse.Content.ReadAsStringAsync());
            responseUser.name.ShouldBe("Joe");
            responseUser.password.ShouldBe(_currentPassword);
            //responseUser.password.ShouldBe("MyCurrentPassword");
            responseUser.contact[0].mob.ShouldBe("07890123456");
            responseUser.contact[0].tel.ShouldBe("01234567890");
        }

        #endregion Then

        #endregion Step Definitions
    }
}
