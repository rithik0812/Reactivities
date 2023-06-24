using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
        private DataContext context;
        // ##########################################################################################
        // THIS IS BAD PRACTICE TO DIRECTLY INJECT IT, USE AN INTERFACE SO THE CONTEXT IS REPLACEABLE
        // ##########################################################################################
        public ActivitiesController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet] // api/activities (endpoint url)
        public async Task<ActionResult<List<Domain.Activity>>> GetActivities()
        {
        // ################################################################################################################################
        // THIS IS BAD PRACTICE TO DIRECTLY CALL THE DB HERE USE THE REPOSTORY PATTERN AND REMEMBER WE DONT WANT TO EXPOSE THE DOMAIN MODEL
        // ################################################################################################################################

            return await context.Activities.ToListAsync();
        }


        [HttpGet("{ID}")] // api/activities/ID (endpoint url)
        public async Task<ActionResult<Domain.Activity>> GetActivity(Guid ID)
        {
        // ################################################################################################################################
        // THIS IS BAD PRACTICE TO DIRECTLY CALL THE DB HERE USE THE REPOSTORY PATTERN AND REMEMBER WE DONT WANT TO EXPOSE THE DOMAIN MODEL
        // ################################################################################################################################

            return await context.Activities.FindAsync(ID);
        }

    }
}