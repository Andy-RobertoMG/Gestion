using System;
using System.Collections.Generic;

namespace Gestion.Server.Data.GestionModels;

public partial class Goal
{
    public int GoalId { get; set; }

    public string GoalName { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public int CompletedTasks { get; set; }

    public int TotalTasks { get; set; }

    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
}
