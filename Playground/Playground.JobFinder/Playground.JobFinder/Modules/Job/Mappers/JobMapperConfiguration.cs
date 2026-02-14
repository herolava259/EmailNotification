using AutoMapper;
using Playground.JobFinder.Modules.Job.Domain;
using Playground.JobFinder.Modules.Job.DTOs;

namespace Playground.JobFinder.Modules.Job.Mappers
{
    public class JobMapperConfiguration: Profile
    {
        public JobMapperConfiguration()
        {
            CreateMap<Job, JobDto>().ReverseMap();
        }
    }
}
