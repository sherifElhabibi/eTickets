using eTickets.Data;
using eTickets.Data.Base;
using eTickets.Data.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Models
{
    public class NewMovieVM
    {
        public int Id { get; set; }


        [Display(Name="Movie Name")]
        [Required(ErrorMessage ="Name Required")]
        public string Name { get; set; }

        [Display(Name = "Movie Description")]
        [Required(ErrorMessage = "Name Required")]
        public string Description { get; set; }

        [Display(Name = "Movie Price $")]
        [Required(ErrorMessage = "Price Required")]
        public double Price { get; set; }

        [Display(Name = "Movie Poster URL")]
        [Required(ErrorMessage = "Poster URL Required")]
        public string ImageURL { get; set; }

        [Display(Name = "Movie Start Date")]
        [Required(ErrorMessage = " Start Date Required")]
        public DateTime StartDate { get; set;}

        [Display(Name = "Movie End Date")]
        [Required(ErrorMessage = " End Date Required")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Movie Category")]
        [Required(ErrorMessage = " Category Required")]
        public MovieCategory MovieCategory { get; set; }

        //Relationships

        [Display(Name = "Movie Producer")]
        [Required(ErrorMessage = " Producer Required")]
        public int ProducerId { get; set; }

        [Display(Name = "Movie Cinema")]
        [Required(ErrorMessage = " Cinema Required")]
        public int CinemaId { get; set; }

        [Display(Name = "Movie Actors")]
        [Required(ErrorMessage = " Actors Required")]
        public ICollection<int> ActorsIds { get; set; }
    }
}
