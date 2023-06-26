using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application
{
    public class Edit
    {
        public class Command : IRequest
        {
            public Activity Activity { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private DataContext _context;
            private IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                this._context = context;
                this._mapper = mapper; 
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                // find the activity in the Db by id
                var activity = await _context.Activities.FindAsync(request.Activity.ID);

                // we will use automapper to map the edited activity to the activity in the Db
                // maps the command request object to the entity object from the DB
                _mapper.Map(request.Activity, activity); 
                // now we save the changes to the DB so its an aync method
                await _context.SaveChangesAsync();

                // this is essentially returning nothing (just a notification the controller saying the task is done)
                return Unit.Value;
            }
        }
    }
}