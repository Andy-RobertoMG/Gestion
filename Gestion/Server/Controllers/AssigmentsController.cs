using Gestion.DTO.Assigments;
using Gestion.Server.Data.GestionModels;
using Microsoft.AspNetCore.Mvc;  
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gestion.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentsController : ControllerBase
    {
        private readonly AssignmentService _assignmentService;

        public AssignmentsController(AssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }

        // GET: api/<AssignmentsController>
        [HttpGet]
        public List<AssignmentDTO> Get()
        {
            return _assignmentService.GetAssignments();
        }

        // GET api/<AssignmentsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        [HttpGet("byGoal/{id}")]
        public async Task<IActionResult> GetByGoalId(int id)
        {
            try
            {
                var result =await _assignmentService.GetByGoalId(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // GET api/<AssignmentsController>/name/{name}
        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var result = await _assignmentService.GetByName(name);
            return Ok(result);
        }

        // POST api/<AssignmentsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AssigmentUpdateDTO assignment)
        {
            try
            {
                await _assignmentService.Save(assignment);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<AssignmentsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AssigmentUpdateDTO assignment)
        {
            try
            {
                await _assignmentService.Update(id, assignment);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<AssignmentsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _assignmentService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
