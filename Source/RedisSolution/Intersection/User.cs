using System.Collections.Generic;

namespace Intersection
{
    public class User
    {
        public User()
        {
            Friends = new List<Friend>();
        }
        public string UserId { get; set; }

        public List<Friend> Friends
        {
            get; private set; 
        }
        public void AddFriend(Friend friend)
        {
            Friends.Add(friend);
        }
    }
}