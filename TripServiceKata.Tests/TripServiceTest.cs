using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripServiceKata.Exception;
using TripServiceKata.Trip;

namespace TripServiceKata.Tests
{
    [TestClass]
    public class TripServiceTest
    {
        public static User.User LoggedInUser = new User.User();

        [TestMethod]
        [ExpectedException(typeof(UserNotLoggedInException))]
        public void Should_Throw_Exception_When_No_Logged_User()
        {
            //Arrange
            LoggedInUser = null;
            TripService tripService = new TestableTripService();

            //Act
            tripService.GetTripsByUser(LoggedInUser);

            //Assert exception
        }

        [TestMethod]
        public void Should_Return_No_Trips_When_Users_Are_Not_Friends()
        {
            //Arrange
            User.User friend = new User.User();
            friend.AddTrip(new Trip.Trip());
            friend.AddFriend(new User.User());

            LoggedInUser = new User.User();
            TripService tripService = new TestableTripService();

            //Act
            List<Trip.Trip> trips = tripService.GetTripsByUser(friend);

            //Assert
            Assert.IsTrue((trips.Count == 0));
        }

        [TestMethod]
        public void Should_Return_Friend_Trips_When_Users_Are_Friends()
        {
            //Arrange
            User.User friend = new User.User();
            friend.AddTrip(new Trip.Trip());
            friend.AddFriend(LoggedInUser);

            TripService tripService = new TestableTripService();

            //Act
            List<Trip.Trip> trips = tripService.GetTripsByUser(friend);

            //Assert
            Assert.IsTrue((trips.Count == 1));
        }

        public class TestableTripService : TripService
        {
            public override User.User GetLoggedUser()
            {
                return LoggedInUser;
            }

            public override List<Trip.Trip> TripsByUser(User.User user)
            {
                return user.Trips();
            }
        }
    }

    
}
