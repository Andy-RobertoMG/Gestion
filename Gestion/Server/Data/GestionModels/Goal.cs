using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gestion.Server.Data.GestionModels;

public partial class Goal
{
    public int GoalId { get; set; }
    [MaxLength(100)]
    public string GoalName { get; set; } = null!;

    public DateTime CreatedDate { get; set; }
    public int CompletedTasks { get; set; }

    public int TotalTasks { get; set; } = 0;
    public decimal Percentage { get; set; } = 0;
    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
}
