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

        //[HttpGet("{skip}/{take}")]
        //public async Task<ActionResult<AssigmentResponse>> Get(int skip,int take)
        //{
        //    try
        //    {
        //        return Ok(_assignmentService.GetAssignments(skip, take));
        //    }
        //    catch(Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

        // GET api/<AssignmentsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        [HttpGet("byGoal/{id}/{page}/{take}")]
        public async Task<IActionResult> GetByGoalId(int id,int page, int take, string task = "",DateTime? date = null,string status = "")
        {
            try
            {
                return Ok(await _assignmentService.GetAssignments(id,page, take,task,date,status));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPut("important")]
        public async Task<ActionResult> ChangeImportant( [FromBody] AssignmentDTO assignment)
        {
            try
            {
                await _assignmentService.ChangeImportant(assignment);
                return Ok();
            }catch(Exception ex)
            {
                return BadRequest();
            }
        } 
        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var result = await _assignmentService.GetByName(name);
            return Ok(result);
        }
         
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
        [HttpPut("completeTasks")]
        public async Task<IActionResult> CompleteTasks(List<int> assignmentsId)
        {
            try
            {
                await _assignmentService.CompleteTasks(assignmentsId);
                return Ok("Tareas completadas exitosamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // DELETE api/<AssignmentsController>/5
        [HttpPost("deletemultiple")]
        public async Task<IActionResult> Delete(List<int> assignmentsId)
        {
            try
            {
                await _assignmentService.Delete(assignmentsId);
                return Ok("");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
