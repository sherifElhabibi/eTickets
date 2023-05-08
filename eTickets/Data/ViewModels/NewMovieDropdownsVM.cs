using eTickets.Models;

namespace eTickets.Data.ViewModels
{
    public class NewMovieDropdownsVM
    {
        public NewMovieDropdownsVM()
        {
            Producers = new HashSet<Producer>();
            Cinemas = new HashSet<Cinema>();
            Actors = new HashSet<Actor>();
        }
        public ICollection<Producer> Producers { get; set; }
        public ICollection<Cinema> Cinemas { get; set; }
        public ICollection<Actor> Actors { get; set; }


    }
}
