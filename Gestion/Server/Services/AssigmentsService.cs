using Gestion.Server.Data.GestionModels;
using Gestion.Server.Data;
using Gestion.DTO.Assigments;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

public class AssignmentService
{
    private readonly GestionContext _context;

    public AssignmentService(GestionContext context)
    {
        _context = context;
    }

    public async Task<AssigmentResponse> GetAssignments(int id, int page, int take, string task = "", DateTime? date = null, string status = "")
    {
        int actualPage = page;
        var query = _context.Assignments.Where(x => x.GoalId == id);
        if (!string.IsNullOrEmpty(task))
        {
            query = query.Where(x => x.AssigmentName.Contains(task));
        }

        if (date.HasValue)
        {
            query = query.Where(x => x.CreatedDate.Date == date.Value.Date); 
        }

        if (!string.IsNullOrEmpty(status))
        {
            query = query.Where(x => x.AssignmentStatus == status); 
        }
        int count = await query.CountAsync();
        int skip = (page - 1) * take;
        if (count < skip)
        {
            skip = 0;
            actualPage = 1;
        }
        var assignments = await query.Skip(skip)
                                     .Take(take)
                                     .ToListAsync(); 
        List<AssignmentDTO> assignmentsDTO = assignments.Select(assignment => new AssignmentDTO
        {
            AssignmentId = assignment.AssignmentId,
            Name = assignment.AssigmentName,
            CreatedDate = assignment.CreatedDate,
            AssignmentStatus = assignment.AssignmentStatus,
            IsImportant = assignment.IsImportant,
            GoalId = assignment.GoalId
        })
        .ToList();

        // Cálculo de las páginas
        int pages = (int)Math.Ceiling((double)count / take); 
        return new AssigmentResponse(assignmentsDTO, count, pages,actualPage);
    }
    public async Task ChangeImportant( AssignmentDTO assignment)
    {
        var assignmentResponse =await _context.Assignments.FirstOrDefaultAsync(x=>x.AssignmentId== assignment.AssignmentId);
        if(assignmentResponse == null)
        {
            throw new Exception("No se encontro la tarea");
        }
        assignmentResponse.IsImportant = assignment.IsImportant;
        _context.Assignments.Update(assignmentResponse);
        _context.SaveChanges();
    }
    public async Task<AssignmentDTO> GetByName(string name)
    {
        var assignment = await _context.Assignments.FirstOrDefaultAsync(a => a.AssigmentName == name);
        if (assignment == null) return new();
        return new AssignmentDTO
        {
            AssignmentId = assignment.AssignmentId,
            Name = assignment.AssigmentName,
            CreatedDate = assignment.CreatedDate,
            AssignmentStatus = assignment.AssignmentStatus,
            IsImportant = assignment.IsImportant,
            GoalId = assignment.GoalId
        };
    }
    public async Task<List<AssignmentDTO>> GetByGoalId(int goalId)
    {
        var assignments = await _context.Assignments
            .Where(a => a.GoalId == goalId)
            .ToListAsync();

        if (!assignments.Any()) return new List<AssignmentDTO>();

        return assignments.Select(assignment => new AssignmentDTO
        {
            AssignmentId = assignment.AssignmentId,
            Name = assignment.AssigmentName,
            CreatedDate = assignment.CreatedDate,
            AssignmentStatus = assignment.AssignmentStatus,
            IsImportant = assignment.IsImportant,
            GoalId = assignment.GoalId
        }).ToList();
    }
    public async Task Save(AssigmentUpdateDTO assignment)
    {
        var search = await _context.Assignments
                                   .FirstOrDefaultAsync(a => a.AssigmentName == assignment.Name.Trim());
        if (search != null)
        {
            throw new Exception("El nombre de la tarea no está disponible.");
        }

        Assignment _assignment = new()
        {
            AssigmentName = assignment.Name, 
            GoalId=assignment.GoalId,
            AssignmentStatus="Abierta"
        };

        _context.Assignments.Add(_assignment);
        await _context.SaveChangesAsync(); 
        await UpdateGoal(assignment.GoalId);
    }

    public async Task Update(int assignmentId, AssigmentUpdateDTO updatedAssignment)
    { 
        var assignment = await _context.Assignments.FirstOrDefaultAsync(a => a.AssignmentId == assignmentId);

        if (assignment == null)
        {
            throw new Exception("La tarea no existe.");
        }
         var search = await _context.Assignments
                                   .FirstOrDefaultAsync(a => a.AssigmentName == updatedAssignment.Name.Trim() && a.AssignmentId != assignmentId);

        if (search != null)
        {
            throw new Exception("El nombre de la tarea no está disponible.");
        }
         
        assignment.AssigmentName = updatedAssignment.Name; 
         
        _context.Assignments.Update(assignment);
        await _context.SaveChangesAsync(); 
    }
    public async Task CompleteTasks(List<int> assignmentsId)
    {
        int goalId = 0;
        var assignments = await _context.Assignments
            .Where(a => assignmentsId.Contains(a.AssignmentId))
            .ToListAsync(); 
        foreach (var assignment in assignments)
        {
            assignment.AssignmentStatus = "Completada";
        }
        if (assignments.Any())
        { 
            goalId = assignments.First().GoalId.GetValueOrDefault(0); 
        }
        else
        { 
            throw new Exception("No se encontraron asignaciones.");
        }
        await _context.SaveChangesAsync();
        await UpdateGoal(goalId);

    }
    public async Task Delete(List<int> assignmentsId)
    {
        int goalId = 0;
        // Busca las asignaciones en la base de datos en una sola consulta
        var assignmentsToDelete = await _context.Assignments
            .Where(a => assignmentsId.Contains(a.AssignmentId))
            .ToListAsync();

        //if (assignmentsToDelete.Count != assignmentsId.Count())
        //{
        //    throw new Exception("Algunas de las asignaciones no existen.");
        //}
        if (assignmentsToDelete.Any())
        {
            goalId = assignmentsToDelete.First().GoalId.GetValueOrDefault(0);
        }
        else
        {
            throw new Exception("No se encontraron asignaciones.");
        } 
        _context.Assignments.RemoveRange(assignmentsToDelete); 
        await _context.SaveChangesAsync(); 
        await UpdateGoal(goalId);
    }
    private async Task UpdateGoal(int goalId)
    { 
        // Obtener la meta con sus asignaciones
        var goal = await _context.Goals
            .Include(g => g.Assignments)
            .FirstOrDefaultAsync(g => g.GoalId == goalId);

        if (goal == null)
        {
            throw new Exception("Meta no encontrada");
        } 

        // Calcular el número total de tareas
        goal.TotalTasks = goal.Assignments.Count;

        // Calcular el número de tareas completadas
        goal.CompletedTasks = goal.Assignments.Count(a => a.AssignmentStatus == "Completada");

        // Calcular el porcentaje de tareas completadas
        goal.Percentage = goal.TotalTasks > 0
            ? (decimal)goal.CompletedTasks / goal.TotalTasks * 100
            : 0;
        _context.Goals.Update(goal);
        // Guardar cambios en la base de datos
        await _context.SaveChangesAsync();
    }
}
