using Gestion.Server.Data.GestionModels;
using Gestion.Server.Data;
using Gestion.DTO.Assigments;
using Microsoft.EntityFrameworkCore;

public class AssignmentService
{
    private readonly GestionContext _context;

    public AssignmentService(GestionContext context)
    {
        _context = context;
    }

    public List<AssignmentDTO> GetAssignments()
    {
        var assignments = _context.Assignments.ToList();
        return assignments.Select(assignment => new AssignmentDTO
        {
            AssignmentId = assignment.AssignmentId,
            AssignmentName = assignment.Assignment1,
            CreatedDate = assignment.CreatedDate,
            AssignmentStatus = assignment.AssignmentStatus,
            IsImportant = assignment.IsImportant,
            GoalId = assignment.GoalId
        })
        .ToList();
    }

    public async Task<AssignmentDTO> GetByName(string name)
    {
        var assignment = await _context.Assignments.FirstOrDefaultAsync(a => a.Assignment1 == name);
        if (assignment == null) return new();
        return new AssignmentDTO
        {
            AssignmentId = assignment.AssignmentId,
            AssignmentName = assignment.Assignment1,
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
            AssignmentName = assignment.Assignment1,
            CreatedDate = assignment.CreatedDate,
            AssignmentStatus = assignment.AssignmentStatus,
            IsImportant = assignment.IsImportant,
            GoalId = assignment.GoalId
        }).ToList();
    }
    public async Task Save(AssigmentUpdateDTO assignment)
    {
        var search = await _context.Assignments
                                   .FirstOrDefaultAsync(a => a.Assignment1 == assignment.AssignmentName.Trim());
        if (search != null)
        {
            throw new Exception("El nombre de la asignación no está disponible.");
        }

        Assignment _assignment = new()
        {
            Assignment1 = assignment.AssignmentName, 
        };

        _context.Assignments.Add(_assignment);
        await _context.SaveChangesAsync();
    }

    public async Task Update(int assignmentId, AssigmentUpdateDTO updatedAssignment)
    { 
        var assignment = await _context.Assignments.FirstOrDefaultAsync(a => a.AssignmentId == assignmentId);

        if (assignment == null)
        {
            throw new Exception("La asignación no existe.");
        }

        // Verifica si el nombre ya existe (excepto para la asignación actual)
        var search = await _context.Assignments
                                   .FirstOrDefaultAsync(a => a.Assignment1 == updatedAssignment.AssignmentName.Trim() && a.AssignmentId != assignmentId);

        if (search != null)
        {
            throw new Exception("El nombre no está disponible.");
        }

        // Actualiza los campos necesarios
        assignment.Assignment1 = updatedAssignment.AssignmentName; 
         
        _context.Assignments.Update(assignment);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int assignmentId)
    {
        // Busca la asignación por su ID
        var assignment = await _context.Assignments.FirstOrDefaultAsync(a => a.AssignmentId == assignmentId);

        if (assignment == null)
        {
            throw new Exception("La asignación no existe.");
        }

        // Elimina la asignación
        _context.Assignments.Remove(assignment);
        await _context.SaveChangesAsync();
    }
}
