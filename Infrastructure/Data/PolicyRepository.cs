using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationCore.Entities;
using WebApplicationCore.Interfaces;

namespace Infrastructure.Repositories
{
    public class PolicyRepository : IPolicyRepository
    {
        private IList<Policy> _policies;
        public PolicyRepository()
        {
            _policies = new List<Policy>();           
        }

        public Task<IEnumerable<Policy>> Get()
        {
            return Task.FromResult<IEnumerable<Policy>>(_policies);
        }

        public Task<Policy> Get(int id)
        {
            var policy = _policies.SingleOrDefault(p => p.Id == id);
            return Task.FromResult<Policy>(policy);
        }

        public Task<Policy> Add(Policy policy)
        {
            if (_policies.Any())
            {
                var id = _policies.Select(p => p.Id).Max() + 1;
                policy.Id = id;
                _policies.Add(policy);
            }
            else
            {
                policy.Id = 1;
                _policies.Add(policy);
            }
            return Task.FromResult(policy);
        }

        public Task<Policy> Update(int id, Policy policy)
        {
            _policies.Remove(_policies.SingleOrDefault(p => p.Id == id));
            _policies.Add(policy);
            return Task.FromResult(policy);
        }

        public Task<Policy> Remove(int id)
        {
            var policy = _policies.SingleOrDefault(p => p.Id == id);

            if (policy != null)
                _policies.Remove(policy);
            return Task.FromResult(policy);
        }     

    }
}