using Playground.JobFinder.Supports.Repositories;

namespace Playground.JobFinder.Modules.JobDomain;

public interface IJobRepository: ISupportRepository<Job>, IAdvancedSupportRepository<Job>
{

}
