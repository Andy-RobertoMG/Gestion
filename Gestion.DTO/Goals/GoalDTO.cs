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

        public string Name { get; set; } = null!;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public int CompletedTasks { get; set; }= 0;

        public decimal Percentage { get; set; } = 0;
        public int TotalTasks { get; set; } = 0; 
    }
}
