using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Class62Homework.Data
{
    public class JokesRepository
    {
        private string _connectionString;

        public JokesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Joke> GetJokes()
        {
            using (var context = new JokesContext(_connectionString))
            {
                return context.Jokes.Include(u => u.UserLikedJokes).ToList();
            }
        }

        public IEnumerable<Joke> GetJokesForUser(int userId)
        {
            using (var context = new JokesContext(_connectionString))
            {
                return context.Jokes.Include(ul => ul.UserLikedJokes).Where(u => u.UserLikedJokes.Any(ui => ui.UserId == userId)).ToList();
            }
        }

        public void AddJoke(Joke joke)
        {
            using (var context = new JokesContext(_connectionString))
            {
                context.Jokes.Add(joke);
                context.SaveChanges();
            }
        }

        public IEnumerable<UserLikedJokes> GetUserLikedJokes(int jokeId)
        {
            using (var context = new JokesContext(_connectionString))
            {
                return context.UserLikedJokes.Where(j => j.JokeId == jokeId).ToList();
            }
        }

        public bool JokeInDb(int websiteId)
        {
            using (var context = new JokesContext(_connectionString))
            {
                return context.Jokes.Any(i => i.WebsiteId == websiteId);
            }
        }

        public int GetJokeId(int websiteId)
        {
            using (var context = new JokesContext(_connectionString))
            {
                return context.Jokes.FirstOrDefault(p => p.WebsiteId == websiteId).Id;
            }
        }

        public Joke GetByWebsiteId(int id)
        {
            using (var context = new JokesContext(_connectionString))
            {
                return context.Jokes.FirstOrDefault(p => p.Id == id);
            }
        }

        public void LikeDislikeJoke(int jokeId, int userId, bool status)
        {
            using (var context = new JokesContext(_connectionString))
            {
                var like = new UserLikedJokes
                {
                    UserId = userId,
                    JokeId = jokeId,
                    Liked = status,
                    Date = DateTime.Now
                };
                if (DidUserLikeOrDislike(userId, jokeId))
                {
                    context.Database.ExecuteSqlCommand(
                        "UPDATE UserLikedJokes SET Liked = @status WHERE UserId = @userId AND JokeId = @jokeId",
                        new SqlParameter("@status", status),
                        new SqlParameter("@userId", userId),
                        new SqlParameter("@jokeId", jokeId));
                }
                else
                {
                    context.UserLikedJokes.Add(like);
                }

                context.SaveChanges();
            }
        }

        public bool DidUserLikeOrDislike(int userId, int jokeId)
        {
            using (var context = new JokesContext(_connectionString))
            {
                return context.UserLikedJokes.Any(u => u.UserId == userId && u.JokeId == jokeId);
            }
        }
    }
}
