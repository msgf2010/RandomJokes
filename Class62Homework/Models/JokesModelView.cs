using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Class62Homework.Data;

namespace Class62Homework.Web.Models
{
    public class JokesModelView
    {
        public IEnumerable<Joke> UserJokes { get; set; }
        public User User { get; set; }
    }
}
