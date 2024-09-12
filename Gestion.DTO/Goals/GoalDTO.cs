using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion.DTO.Goals
{
    public class GoalDTO
    {
        public int GoalId { get; set; }

        public string GoalName { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public int CompletedTasks { get; set; }

        public int TotalTasks { get; set; }
    }
}
