using AutoMapper;
using Playground.JobFinder.Models.DTOs;
using Playground.JobFinder.Modules.JobDomain;

namespace Playground.JobFinder.Models.Mappers
{
    public class JobMapperConfiguration: Profile
    {
        public JobMapperConfiguration()
        {
            CreateMap<Job, JobDto>().ReverseMap();
        }
    }
}
