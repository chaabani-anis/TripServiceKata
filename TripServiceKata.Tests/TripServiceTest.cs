using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripServiceKata.Exception;
using TripServiceKata.Trip;

namespace TripServiceKata.Tests
{
    [TestClass]
    public class TripServiceTest
    {
        private TripService _tripService;

        [TestInitialize]
        public void SetUp()
        {
            _tripService = new TripService();
        }


        [TestMethod]
        [ExpectedException(typeof(UserNotLoggedInException))]
        public void Should_Return_Exception_When_User_Not_Logged()
        {
            //Arrange
            User.User user = null;
            
            //Act
            _tripService.GetTripsByUser(user, null);
        }

        [TestMethod]
        public void Should_Return_No_Trips_When_User_Has_No_Friends()
        {
            //Arrange
            var user1 = new User.User();
            var loggedUser = new User.User();

            //Act
            var listTrips = _tripService.GetTripsByUser(user1, loggedUser);

            //Assert
            Assert.IsFalse(listTrips.Any());
        }

        [TestMethod]
        public void Should_Return_No_Trip_When_Users_Are_Not_Friends()
        {
            //Arrange
            var user = new User.User();
            user.AddFriend(new User.User());
            var loggedUser = new User.User();
            loggedUser.AddFriend(new User.User());

            //Act
            var listTrips = _tripService.GetTripsByUser(user, loggedUser);

            //Assert
            Assert.IsFalse(listTrips.Any());
        }

        [TestMethod]
        public void Should_Return_Trips_When_Users_Are_Friends()
        {
            //Arrange
            _tripService = new TestableTripService();
            var user = new User.User();
            
            var friend = new User.User();
            user.AddFriend(friend);

            var brazil = new Trip.Trip();
            user.AddTrip(brazil);

            //Act
            var listTrips = _tripService.GetTripsByUser(user, friend);

            //Assert
            Assert.IsTrue(listTrips.Any());
        }

        private class TestableTripService : TripService
        {
            protected override User.User LoggedUsers()
            {
                return null;
            }

            protected override List<Trip.Trip> GetTripsBy(User.User user)
            {
                return user.Trips();
            }
        }
    }
}
