using Gestion.DTO.Goals;
using Gestion.Server.Data;
using Gestion.Server.Data.GestionModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Server.Services;

public class GoalService
{
    private readonly GestionContext _context;
    public GoalService(GestionContext context)
    {
        _context = context;
    }
    public List<GoalDTO>  GetGoals()
    {
        var goals= _context.Goals.ToList();
        return goals.Select(goal => new GoalDTO
        {
            GoalId = goal.GoalId,
            Name = goal.GoalName,
            CreatedDate = goal.CreatedDate,
            CompletedTasks = goal.CompletedTasks,
            TotalTasks = goal.TotalTasks,
            Percentage= goal.Percentage,
        })
        .ToList(); 
    }
    public async Task<GoalDTO?> GetById(int id)
    { 
        var goal = await _context.Goals.FirstOrDefaultAsync(g=>g.GoalId == id);
        if (goal != null) {
            return new GoalDTO
            {
                GoalId = goal.GoalId,
                Name = goal.GoalName,
                CreatedDate = goal.CreatedDate,
                CompletedTasks = goal.CompletedTasks,
                TotalTasks = goal.TotalTasks,
                Percentage = goal.Percentage,
            };
        }
        return null; 
    }
    public async Task<GoalDTO> GetByName(string name) 
    {
        var goal= await _context.Goals.FirstOrDefaultAsync(g => g.GoalName == name);
        if (goal == null) return new();
        return new GoalDTO
        {
            GoalId = goal.GoalId,
            Name = goal.GoalName,
            CreatedDate = goal.CreatedDate,
            CompletedTasks = goal.CompletedTasks,
            TotalTasks = goal.TotalTasks,
            Percentage= goal.Percentage,
        };
    }
    public async Task Save(GoalUpdateDTO goal)
    {
        var search = await _context.Goals
                                 .FirstOrDefaultAsync(g => g.GoalName == goal.Name.Trim());
        if(search != null)
        {
            throw new Exception("El nombre no esta disponible.");
        }
        Goal _goal = new()
        {

            GoalName = goal.Name,
            CreatedDate = DateTime.Now,
            CompletedTasks = 0,
            TotalTasks = 0,
            Percentage = 0
        };
        _context.Goals.Add(_goal );
        await _context.SaveChangesAsync();
    }
    public async Task Update(int goalId, GoalUpdateDTO updatedGoal)
    { 
        var goal = await _context.Goals.FirstOrDefaultAsync(g => g.GoalId == goalId);

        if (goal == null)
        {
            throw new Exception("La meta no existe.");
        } 
        var search = await _context.Goals
                                   .FirstOrDefaultAsync(g => g.GoalName == updatedGoal.Name.Trim() && g.GoalId != goalId);

        if (search != null)
        {
            throw new Exception("El nombre no está disponible.");
        }  
        goal.GoalName = updatedGoal.Name;  
        _context.Goals.Update(goal);
        await _context.SaveChangesAsync();
    }
    public async Task Delete(int goalId)
    { 
        var goal = await _context.Goals.FirstOrDefaultAsync(g => g.GoalId == goalId);
        if (goal == null)
        {
            throw new Exception("El objetivo no existe.");
        }
        var _assigments = _context.Assignments.Where(x => x.GoalId == goalId).ToList();
        _context.RemoveRange(_assigments);
        //var hasAssignments = await _context.Assignments.AnyAsync(a => a.GoalId == goalId);
        //if (hasAssignments)
        //{
        //    throw new Exception("No se puede eliminar la meta porque tiene tareas asignadas.");
        //} 
        _context.Goals.Remove(goal);
        await _context.SaveChangesAsync();
    }
}
