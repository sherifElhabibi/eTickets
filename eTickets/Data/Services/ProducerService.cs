using eTickets.Data.Base;
using eTickets.Models;

namespace eTickets.Data.Services
{
    public class ProducerService : EntityBaseRepository<Producer>,IProducerService
    {
        public ProducerService(eTicketContext context):base(context){ }
    }
}
