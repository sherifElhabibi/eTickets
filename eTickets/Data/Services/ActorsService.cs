﻿using eTickets.Data.Base;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services
{
    public class ActorsService : EntityBaseRepository<Actor>,IActorsService
    {
        public ActorsService(eTicketContext context):base(context) { }
    }
}
