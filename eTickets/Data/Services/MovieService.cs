using eTickets.Data.Base;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services
{
    public class MovieService: EntityBaseRepository<Movie>, IMovieService
    {
        private readonly eTicketContext _context;
        public MovieService(eTicketContext context) : base(context) { _context = context; }

        public async Task AddNewMovieAsync(NewMovieVM data)
        {
            var newMovie = new Movie()
            {
                Id = data.Id,
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,
                CinemaId = data.CinemaId,
                ProducerId = data.ProducerId,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                MovieCategory = data.MovieCategory
            };
            await _context.Movie.AddAsync(newMovie);
            await _context.SaveChangesAsync();

            //Add Movie Actors
            foreach(var actorId in data.ActorsIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = newMovie.Id,
                    ActorId = actorId,
                };
                await _context.Actors_Movies.AddAsync(newActorMovie);
            }
                await _context.SaveChangesAsync();
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var movie = await _context.Movie
                .Include(c => c.Cinema)
                .Include(p => p.MovieProducer)
                .Include(am => am.Actors_Movies).ThenInclude(a => a.Actor)
                .FirstOrDefaultAsync(n => n.Id == id);
            return movie;
        }

        public async Task<NewMovieDropdownsVM> GetNewMovieDropdownsValues()
        {
            var response = new NewMovieDropdownsVM()
            {
                Cinemas = await _context.Cinema.OrderBy(n => n.Name).ToListAsync(),
                Actors = await _context.Actor.OrderBy(n => n.FullName).ToListAsync(),
                Producers = await _context.Producer.OrderBy(n => n.FullName).ToListAsync()
            };
            return response;
        }

        public async Task UpdateMovieAsync(NewMovieVM data)
        {
            var movieDetails = await _context.Movie.FirstOrDefaultAsync(n => n.Id == data.Id);
            if (movieDetails != null)
            {
                movieDetails.Name = data.Name;
                movieDetails.Description = data.Description;
                movieDetails.Price = data.Price;
                movieDetails.ImageURL = data.ImageURL;
                movieDetails.CinemaId = data.CinemaId;
                movieDetails.ProducerId = data.ProducerId;
                movieDetails.StartDate = data.StartDate;
                movieDetails.EndDate = data.EndDate;
                movieDetails.MovieCategory = data.MovieCategory;
                await _context.SaveChangesAsync();

                //Remove Existing Actors
                var existingActors = _context.Actors_Movies.Where(n=>n.MovieId==data.Id).ToList();
                _context.Actors_Movies.RemoveRange(existingActors);
                await _context.SaveChangesAsync();

                //Add Movie Actors
                foreach (var actorId in data.ActorsIds)
                {
                    var newActorMovie = new Actor_Movie()
                    {
                        MovieId = data.Id,
                        ActorId = actorId,
                    };
                    await _context.Actors_Movies.AddAsync(newActorMovie);
                }
                await _context.SaveChangesAsync();
            }
        }
    }
}
