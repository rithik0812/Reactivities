using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid ID { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private DataContext _context;
            private IMapper _mapper;

            public Handler(DataContext context)
            {
                this._context = context;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                // find the activity in the Db by id
                var activity = await _context.Activities.FindAsync(request.ID);

                // remove activity
                _context.Remove(activity);

                // now we save the changes to the DB so its an aync method
                await _context.SaveChangesAsync();

                // this is essentially returning nothing (just a notification the controller saying the task is done)
                return Unit.Value;
            }
        }
    }
}