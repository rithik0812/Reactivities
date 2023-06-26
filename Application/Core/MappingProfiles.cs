using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain;

namespace Application.Core
{
    // An instance to extend the automapper base class Profiles, will be injected using dependancy injection from Service container
    public class MappingProfiles : Profile
    {
        // shows the mapping from one object type to another object type
        // In our case since we are editing it both objects are the same type
        public MappingProfiles()
        {
            CreateMap<Activity, Activity>();
        }
    }
}