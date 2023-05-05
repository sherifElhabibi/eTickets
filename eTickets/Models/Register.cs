using eTickets.Data.Enums;
using MessagePack;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Models
{
    public class Register
    {
        public string Name { get; set; }
        public int Phone { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; }

    }
}
