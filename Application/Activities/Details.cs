using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Details
    // Details if essentially a find the activity by ID
    {
        // make a query request part of cqrs (separate query and commands (eg. get vs edit/delete/add))
        // this Query class is the request for a get all and expects a single instence of domain object activity based on given ID
        public class Query : IRequest<Activity> 
        {
            public Guid ID { get; set; } 
        }

        // the handler takes in the request and dependency injection injects the dbcontext object from EF
        // then it finds a single instance of the domain object activity by the id given in the Query 
        public class Handler : IRequestHandler<Query, Activity>
        {
            private DataContext _context;

            public  Handler(DataContext context)
            {
                this._context = context;                
            }

            public async Task<Activity> Handle(Query request, CancellationToken cancellationToken)
            {
                // find the activity by ID
                return await _context.Activities.FindAsync(request.ID);
            }
        }
    }
}