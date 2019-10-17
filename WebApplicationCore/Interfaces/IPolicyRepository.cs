using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplicationCore.Entities;

namespace WebApplicationCore.Interfaces
{
    public interface IPolicyRepository
    {
        Task<IEnumerable<Policy>> Get();
        Task<Policy> Add(Policy policy);
        Task<Policy> Update(int id, Policy policy);
        Task<Policy> Remove(int id);
        Task<Policy> Get(int id);
    }
}