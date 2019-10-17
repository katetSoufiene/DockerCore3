using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplicationCore.Entities;
using WebApplicationCore.Interfaces;

namespace WebApiCore3.Controllers
{
    [Route("api/[controller]")]
    public class PoliciesController : Controller
    {
        private readonly IPolicyService _service;

        public PoliciesController(IPolicyService service)
        {
            _service = service;
        }

        [Produces("application/json")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var policies = await _service.Get();
            return Ok(policies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var policy = await _service.Get(id);
            if (policy == null)
            {
                return NotFound();
            }

            return Ok(policy);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Policy policy)
        {
            if (policy == null)
            {
                return BadRequest();
            }

            await _service.Add(policy);
            return Ok();

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Policy policy)
        {

            if ((await _service.Get()).All(a => a.Id != id))
            {
                return NotFound(id);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _service.Update(id, policy);
            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            if ((await _service.Get()).All(a => a.Id != id))
            {
                return NotFound(id);
            }

            await _service.Remove(id);
            return Ok();
        }
    }
}
