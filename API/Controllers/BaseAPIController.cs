using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseAPIController : ControllerBase
    {
        // we make a private field of mediator in the base class
        private IMediator _mediator; 

        // we check if the _mediator field is null
        // if its null we retrive an instance of it from the services container 
        // if its not null then we return the same instance of it 
        // since this property is protected it can be accessed by all children classes of the base class        
        protected IMediator Mediator
        {
            get
            {
                if (_mediator == null)
                {
                    _mediator = HttpContext.RequestServices.GetService<IMediator>();
                }
                return _mediator;
            }
        }
    }
}