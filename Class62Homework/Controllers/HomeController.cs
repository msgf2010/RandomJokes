using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Class62Homework.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Class62Homework.Web.Models;

//Create an application that displays a list of random programming jokes to the user.
//Use this api to get the random jokes: https://official-joke-api.appspot.com/jokes/programming/random

//This application will also have a login system where users can login/signup to the site.

//When a logged in user visits the site, they will see a like/dislike option next to the joke.
//If they've previously liked the joke, then the like button should be disabled, and the dislike should be enabled.
//If they've disliked it in the past, then the opposite should happen.
//A user should only be able to change their mind within 10 minutes (or whatever time interval you choose).

//Finally, the application should have a page where it displays a list of all jokes ever displayed to any user,
//with a count of how many likes and dislikes each joke has.

//Use Entity Framework for all the db code. As a guide for how to do the many to many relationship, see here:
//https://github.com/LITW06/QASite/blob/master/QASite.Data/QASiteContext.cs#L28

//There will be three tables in this application, Jokes, Users and UserLikedJokes.
//The UserLikedJokes will have a UserId, JokeId, DateTime and a boolean Liked.

namespace Class62Homework.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString;

        public HomeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        public IActionResult GetJoke()
        {
            var api = new JokesApi();

            return Json(api.GetRandomJoke().First());
        }

        public IActionResult GetLikesDislikes(int websiteId)
        {
            var repo = new JokesRepository(_connectionString);
            var authDb = new Authentication(_connectionString);

            var likes = 0;
            var dislikes = 0;
            var user = authDb.GetByEmail(User.Identity.Name);
            var didUserLikeOrDislike = false;
            var userLiked = false;
            DateTime date = new DateTime();

            if (repo.JokeInDb(websiteId))
            {
                IEnumerable<UserLikedJokes> ul = repo.GetUserLikedJokes(repo.GetJokeId(websiteId));
                likes = ul.Where(j => j.Liked).Count();
                dislikes = ul.Where(j => !j.Liked).Count();
                if (user != null)
                {
                    didUserLikeOrDislike = ul.Any(u => u.UserId == user.Id);
                    if (didUserLikeOrDislike)
                    {
                        date = ul.FirstOrDefault(u => u.UserId == user.Id).Date;
                        userLiked = ul.Any(u => u.Liked && u.UserId == user.Id);
                    }
                }
            }

            return Json(new { likes, dislikes, didUserLikeOrDislike, date, userLiked });
        }

        public IActionResult Index()
        {
            var repo = new JokesRepository(_connectionString);
            var authDb = new Authentication(_connectionString);

            JokesModelView mv = new JokesModelView();
            mv.User = authDb.GetByEmail(User.Identity.Name);
            if (mv.User != null)
            {
                mv.UserJokes = repo.GetJokesForUser(mv.User.Id);
            }

            return View(mv);
        }

        public IActionResult RandomJoke()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LikeJoke(Joke joke)
        {
            var repo = new JokesRepository(_connectionString);
            var authDb = new Authentication(_connectionString);
            var user = authDb.GetByEmail(User.Identity.Name);

            if (repo.JokeInDb(joke.WebsiteId))
            {
                joke.Id = repo.GetJokeId(joke.WebsiteId);
                repo.LikeDislikeJoke(joke.Id, user.Id, true);
            }
            else
            {
                repo.AddJoke(joke);
                joke.Id = repo.GetJokeId(joke.WebsiteId);
                repo.LikeDislikeJoke(joke.Id, user.Id, true);
            }

            return Json("");
        }

        [HttpPost]
        public IActionResult DislikeJoke(Joke joke)
        {
            var repo = new JokesRepository(_connectionString);
            var authDb = new Authentication(_connectionString);
            var user = authDb.GetByEmail(User.Identity.Name);

            if (repo.JokeInDb(joke.WebsiteId))
            {
                joke.Id = repo.GetJokeId(joke.WebsiteId);
                repo.LikeDislikeJoke(joke.Id, user.Id, false);
            }
            else
            {
                repo.AddJoke(joke);
                joke.Id = repo.GetJokeId(joke.WebsiteId);
                repo.LikeDislikeJoke(joke.Id, user.Id, false);
            }

            return Json("");
        }
    }
}
