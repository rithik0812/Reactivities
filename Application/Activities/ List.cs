using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application
{
    public class  List
    // list is essentially GetAll for activities
    {
        // make a query request part of cqrs (separate query and commands (eg. get vs edit/delete/add))
        // this Query class is the request for a get all and expects a list of the domain object activity
        public class Query : IRequest<List<Activity>> {}

        // the handler takes in the request and dependency injection injects the dbcontext object from EF
        // then it returns a list of list of the domain object activity, as per the expectations of the query
        public class Handler : IRequestHandler<Query, List<Activity>>
        {
            private DataContext _context;

            public  Handler(DataContext context)
            {
                this._context = context;                
            }

            public async Task<List<Activity>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Activities.ToListAsync();
            }
        }
    }
}