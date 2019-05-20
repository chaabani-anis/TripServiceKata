using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripServiceKata.Exception;
using TripServiceKata.Trip;

namespace TripServiceKata.Tests
{
    [TestClass]
    public class TripServiceTest
    {
        [TestMethod]
        [ExpectedException(typeof(UserNotLoggedInException))]
        public void Should_Throw_Exception_When_No_Logged_User()
        {
            //Arrange
            User.User unloggedUser = null;
            TripService tripService = new TestTripService();

            //Act
            tripService.GetTripsByUser(unloggedUser);

            //Assert exception
        }
    }

    public class TestTripService : TripService
    {
        public override User.User GetLoggedUser()
        {
            return null;
        }
    }
}
