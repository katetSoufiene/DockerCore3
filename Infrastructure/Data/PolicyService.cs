using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplicationCore.Entities;
using WebApplicationCore.Interfaces;

namespace Infrastructure.Services
{
    public class PoliciesService : IPolicyService
    {
        private IPolicyRepository _repository;

        public PoliciesService(IPolicyRepository repository)
        {
            _repository = repository;
        }

        public async Task<Policy> Add(Policy policy)
        {
            if (string.IsNullOrEmpty(policy.PolicyHolder.Name)) return null;

            return await _repository.Add(policy);
        }

        public async Task<IEnumerable<Policy>> Get()
        {
            return await _repository.Get();
        }

        public async Task<Policy> Get(int id)
        {
            if (id < 0) return null;

            return await _repository.Get(id);
        }

        public async Task<Policy> Remove(int id)
        {
            if (id < 0) return null;

            return await _repository.Remove(id);
        }

        public async Task<Policy> Update(int id, Policy policy)
        {
            if (id != policy.Id) return null;

            return await _repository.Update(id, policy);
        }
    }
}
