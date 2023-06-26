using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application
{
    public class Create
    {
        public class Command : IRequest
        {
            public Activity Activity { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private DataContext _context;

            public Handler(DataContext context)
            {
                this._context = context;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                // add a new activity (note we dont access DB here so no async)
                _context.Activities.Add(request.Activity);

                

                // now we save the changes to the DB so its an aync method
                await _context.SaveChangesAsync();

                // this is essentially returning nothing (just a notification the controller saying the task is done)
                return Unit.Value;
            }
        }
    }
}