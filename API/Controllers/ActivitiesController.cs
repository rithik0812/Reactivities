using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Application.Activities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActivitiesController : BaseAPIController
    {

        [HttpGet] // api/activities (endpoint url)
        // Get All endpoint 
        public async Task<ActionResult<List<Domain.Activity>>> GetActivities()
        {
            // Mediator prop comes from the base class
            return await Mediator.Send(new List.Query());
        }


        [HttpGet("{ID}")] // api/activities/ID (endpoint url)
        // Get object by ID 
        public async Task<ActionResult<Domain.Activity>> GetActivity(Guid ID)
        {
            // send the ID parameter from the HTTP request into Mediator 
            return await Mediator.Send(new Details.Query{ID = ID});
        }

        [HttpPost] // api/activities (needs an Activity object in body to create one)
        // Create a new activity object
        public async Task<IActionResult> CreateActivity( [FromBody] Domain.Activity activity) 
        {
            return Ok(await Mediator.Send(new Create.Command{Activity = activity}));
        }

        [HttpPut("{id}")] // api/activities/ID (needs an Activity object in the body (without id) and a id in the route)
        // Edit an existing activity 
        public async Task<IActionResult> EditActivity( [FromRoute] Guid ID, [FromBody] Domain.Activity activity)
        {
            activity.ID = ID;
            return Ok(await Mediator.Send(new Edit.Command{Activity = activity}));
        }

        [HttpDelete("{id}")] // api/activities/ID 
        // Delete an activity by id 

        public async Task<IActionResult> DeleteActivity(Guid ID)
        {
            return Ok(await Mediator.Send(new Delete.Command{ID = ID})); 
        }
    }
}