using eTickets.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Models
{
    public class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public DateTime StartDate { get; set;}
        public DateTime EndDate { get; set; }
        public double Price { get; set; }
        public MovieCategory MovieCategory { get; set; }

        //Producer
        public int ProducerId { get; set; }
        [ForeignKey("ProducerId")]
        public Producer MovieProducer { get; set; }   
        
        //Actor
        public ICollection<Actor> Actors { get; set; }

        //Cinema
        public int CinemaId { get; set; }
        [ForeignKey("CinemaId")]
        public Cinema Cinema { get; set; }
        public ICollection<Actor_Movie> Actors_Movies { get; set; }
    }
}
