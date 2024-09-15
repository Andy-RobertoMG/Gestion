using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion.DTO.Assigments;

public record struct AssigmentResponse(List<AssignmentDTO> Assignments,int Count,int Pages,int ActualPage ); 
