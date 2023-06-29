using AmidoTechBddfyTests.Tests.Fixtures;
using AmidoTechBddfyTests.Tests.Steps;
using TestStack.BDDfy;
using Xunit;

namespace AmidoTechBddfyTests.Tests.Stories
{
    [Story(
        AsA = "Tester at Ensono Digital",
        IWant = "to be able to write some automated tests",
        SoThat = "I can apply to be Senior Consultant and get more money")]


    public class AutomatedTests : IClassFixture<AuthFixture>
    {
        private readonly AuthFixture _fixture;
        private readonly CreateUserSteps _createUserSteps;
        private readonly GetUserSteps _getUserSteps;
        private readonly UpdateUserSteps _updateUserSteps;
        private readonly DeleteUserSteps _deleteUserSteps;

        public AutomatedTests(AuthFixture fixture)
        {
            this._fixture = fixture;
            _createUserSteps = new CreateUserSteps();
            _getUserSteps = new GetUserSteps();
            _updateUserSteps = new UpdateUserSteps();
            _deleteUserSteps = new DeleteUserSteps();
        }

        [Fact]
        public void CreateUserTest()
        {
            this.Given(CreateUserSteps => _createUserSteps.GivenAUniqueUser())
                .When(CreateUserSteps => _createUserSteps.WhenIAddAUser())
                .Then(CreateUserSteps => _createUserSteps.ThenTheCorrectResponseCodeIsReturned())
                .BDDfy();
        }

        [Fact]
        public void GetUserTest()
        {
            this.Given(GetUserSteps => _getUserSteps.GivenAUserExists())
                .When(GetUserSteps => _getUserSteps.WhenIGetAUser())
                .Then(GetUserSteps => _getUserSteps.ThenTheCorrectResponseCodeIsReturned())
                .Then(GetUserSteps => _getUserSteps.ThenTheUserDetailsAreCorrect())
                .BDDfy();
        }

        [Fact]
        public void UpdateUserPasswordTest()
        {
            this.Given(UpdateUserSteps => _updateUserSteps.GivenAUserExists())
                .When(UpdateUserSteps => _updateUserSteps.WhenNewUserValuesAre())
                .When(UpdateUserSteps => _updateUserSteps.WhenIUpdateUserWithNewPassword())
                .Then(UpdateUserSteps => _updateUserSteps.ThenTheCorrectResponseCodeIsReturned())
                .Then(UpdateUserSteps => _updateUserSteps.ThenTheNewUserDetailsAreCorrect())
                .BDDfy();
        }


        [Fact]
        public void DeleteUserTest()
        {
            this.Given(D => _deleteUserSteps.GivenAUserExists())
                .When(DeleteUserSteps => _deleteUserSteps.WhenIDeleteUser())
                .Then(DeleteUserSteps => _deleteUserSteps.ThenTheCorrectResponseCodeIsReturned())
                .BDDfy();
        }
    }
}
