using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gestion.Server.Data.GestionModels;

public partial class Assignment
{
    public int AssignmentId { get; set; }

    public int? GoalId { get; set; }
    
    public string AssigmentName { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string AssignmentStatus { get; set; } = null!;

    public bool IsImportant { get; set; } 

    public virtual Goal? Goal { get; set; }
}
