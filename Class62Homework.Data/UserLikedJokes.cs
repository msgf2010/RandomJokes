using System;
using System.Collections.Generic;
using System.Text;

namespace Class62Homework.Data
{
    public class UserLikedJokes
    {
        public int UserId { get; set; }
        public int JokeId { get; set; }
        public DateTime Date { get; set; }
        public bool Liked { get; set; }

        public User User { get; set; }
        public Joke Joke { get; set; }
    }
}
