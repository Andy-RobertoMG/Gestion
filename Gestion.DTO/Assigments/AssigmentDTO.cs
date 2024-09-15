 
namespace Gestion.DTO.Assigments;

public class AssignmentDTO
{
    public int AssignmentId { get; set; } = 0;
    public string Name { get; set; } =string.Empty;
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public string AssignmentStatus { get; set; } = string.Empty;
    public bool IsImportant { get; set; } = true;
    public int? GoalId { get; set; } = 0;
}
