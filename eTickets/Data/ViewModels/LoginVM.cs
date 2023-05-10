using eTickets.Data;
using eTickets.Data.Base;
using eTickets.Data.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace eTickets.Data.ViewModels
{
    public class LoginVM
    {
        [Display(Name="Email Address")]
        [Required(ErrorMessage="Email Required")]
        public string EmailAddress { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
