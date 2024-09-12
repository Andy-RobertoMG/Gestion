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
            GoalName = goal.GoalName,
            CreatedDate = goal.CreatedDate,
            CompletedTasks = goal.CompletedTasks,
            TotalTasks = goal.TotalTasks
        })
        .ToList(); 
    }

    public async Task<GoalDTO> GetByName(string name) 
    {
        var goal= await _context.Goals.FirstOrDefaultAsync(g => g.GoalName == name);
        if (goal == null) return new();
        return new GoalDTO
        {
            GoalId = goal.GoalId,
            GoalName = goal.GoalName,
            CreatedDate = goal.CreatedDate,
            CompletedTasks = goal.CompletedTasks,
            TotalTasks = goal.TotalTasks
        };
    }
    public async Task Save(GoalUpdateDTO goal)
    {
        var search = await _context.Goals
                                 .FirstOrDefaultAsync(g => g.GoalName == goal.GoalName.Trim());
        if(search != null)
        {
            throw new Exception("El nombre no esta disponible.");
        }
        Goal _goal = new()
        {
            
            GoalName = goal.GoalName,
            CreatedDate = DateTime.Now,
            CompletedTasks = 0,
            TotalTasks = 0
        };
        _context.Goals.Add(_goal );
        await _context.SaveChangesAsync();
    }
    public async Task Update(int goalId, GoalUpdateDTO updatedGoal)
    {
        // Busca el objetivo por su ID
        var goal = await _context.Goals.FirstOrDefaultAsync(g => g.GoalId == goalId);

        if (goal == null)
        {
            throw new Exception("El objetivo no existe.");
        }

        // Verifica si el nombre del objetivo ya existe (excepto para el objetivo actual)
        var search = await _context.Goals
                                   .FirstOrDefaultAsync(g => g.GoalName == updatedGoal.GoalName.Trim() && g.GoalId != goalId);

        if (search != null)
        {
            throw new Exception("El nombre no está disponible.");
        } 
        // Actualiza los campos necesarios
        goal.GoalName = updatedGoal.GoalName; 

        // Guarda los cambios en la base de datos
        _context.Goals.Update(goal);
        await _context.SaveChangesAsync();
    }
    public async Task Delete(int goalId)
    {
        // Busca el objetivo por su ID
        var goal = await _context.Goals.FirstOrDefaultAsync(g => g.GoalId == goalId);

        if (goal == null)
        {
            throw new Exception("El objetivo no existe.");
        }

        // Elimina el objetivo
        _context.Goals.Remove(goal);
        await _context.SaveChangesAsync();
    }
}
