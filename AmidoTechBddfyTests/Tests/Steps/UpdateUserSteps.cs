using AmidoTechBddfyTests.Builders;
using AmidoTechBddfyTests.Configuration;
using AmidoTechBddfyTests.Models;
using Newtonsoft.Json;
using Shouldly;
using System.Net;

namespace AmidoTechBddfyTests.Tests.Steps
{
    public class UpdateUserSteps : StepsBase
    {
        private string _existingUserId;
        private const string UserPath = "/users/";
        private User _newUserDetails;

        public UpdateUserSteps()
        {
            var config = ConfigAccessor.GetApplicationConfiguration();
            BaseUrl = config.BaseUrl;
        }

        #region Step Definitions

        #region Given
        public void GivenAUserExists()
        {
            _existingUserId = "59374fh3rhfjsjjjakskw8w";

        }

        #endregion Given

        #region When

        public void WhenNewUserValuesAre()
        {
            _newUserDetails = new User
            {
                name = "Joe",
                password = "MyNewPassword"
            };
        }

        public async Task WhenIUpdateUserWithNewPassword()
        {
            LastResponse = await HttpRequestFactory.Put(BaseUrl, UserPath + _existingUserId, _newUserDetails);
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

        public async Task ThenTheNewUserDetailsAreCorrect()
        {
            var responseUser =
                JsonConvert.DeserializeObject<UserResponse>(await LastResponse.Content.ReadAsStringAsync());
            responseUser.name.ShouldBe("Joe");
            responseUser.password.ShouldBe("MyNewPassword");
            responseUser.contact[0].mob.ShouldBe("07890123456");
            responseUser.contact[0].tel.ShouldBe("01234567890");
        }

        #endregion Then

        #endregion Step Definitions
    }
}
