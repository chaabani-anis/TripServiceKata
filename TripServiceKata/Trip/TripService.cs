using System.Collections.Generic;
using TripServiceKata.Exception;
using TripServiceKata.User;

namespace TripServiceKata.Trip
{
    public class TripService
    {
        public List<Trip> GetTripsByUser(User.User user, User.User loggedUser)
        {
            List<Trip> tripList = new List<Trip>();
            bool isFriend = false;
            if (loggedUser == null)
            {
                throw new UserNotLoggedInException();
            }
            else
            {
                isFriend = AreUserFriends(user, loggedUser);

                if (isFriend)
                {
                    tripList = GetTripsBy(user);
                }

                return tripList;
            }
        }

        private bool AreUserFriends(User.User user, User.User loggedUser)
        {
            bool isFriend = false;
            foreach (User.User friend in user.GetFriends())
            {
                if (friend.Equals(loggedUser))
                {
                    isFriend = true;
                    break;
                }
            }
            return isFriend;
        }

        protected virtual List<Trip> GetTripsBy(User.User user)
        {
            List<Trip> tripList;
            tripList = TripDAO.FindTripsByUser(user);
            return tripList;
        }

        protected virtual User.User LoggedUsers()
        {
            User.User loggedUser = UserSession.GetInstance().GetLoggedUser();
            return loggedUser;
        }
    }
}
