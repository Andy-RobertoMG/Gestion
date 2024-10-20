using Gestion.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Gestion.DTO.Goals;
using Gestion.Server.Data.GestionModels;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gestion.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoalsController : ControllerBase
    {
        private readonly GoalService _goalService;
        public GoalsController(GoalService goalService)
        {
            _goalService = goalService;
        }

        // GET: api/<GoalsController>
        [HttpGet]
        public List<GoalDTO> Get()
        {
            return _goalService.GetGoals();
        }

        // GET api/<GoalsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var goal =await _goalService.GetById(id);
            if (goal == null)
            {
                return NotFound();
            }
            return Ok(goal);
        }
        [HttpGet("name/{name}")]
        public IActionResult Get(string name)
        {
            return  Ok(_goalService.GetByName(name));
        }

        // POST api/<GoalsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GoalUpdateDTO goal)
        {
            try
            {
                await _goalService.Save(goal);
                return Ok();
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<GoalsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] GoalUpdateDTO goal)
        {
            try
            {
                await _goalService.Update(id, goal);

                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<GoalsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _goalService.Delete(id);  
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
